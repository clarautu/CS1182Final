using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CustomClasses;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;

namespace Deliverable7 {
    /// <summary>
    /// Deliverable7
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// </summary>
    /// <ImAwesome>
    /// Has a seperate load form that loads first and asks player what they'd like to do: load game, new game, or new game with custom map dimensions
    /// Has a credit form that displays the game's credits
    /// Has a seperate pause form for loading and saving your game
    /// Has a welcome tutorial that only loads on new games
    /// frmMonster displays all relavant Hero and Monster stats
    /// Has a combat tutorial that loads during the first combat
    /// Has a Boss, which is a bigger, stronger Monster
    /// Boss carries the key. Must be defeated to win.
    /// frmItem displays found and current Weapon stats to user
    /// </ImAwesome>
    public partial class MainWindow : Window {
        public MainWindow(bool? loadGame) {
            InitializeComponent();
            //If loaded game, deserialize before populating grdGameBoard
            if (loadGame == true) {
                Deserialize();
                PopulateGrdGameBoard(Game.Height, Game.Width);
                ShowMapContents();
                UpdateHeroStats();
            } else if (loadGame == null) {
                //Null if only instantiating to access Serialize() and Deserialize() methods
            } else {
                //False if new game
                PopulateGrdGameBoard(Game.Height, Game.Width);
                ShowMapContents();
                UpdateHeroStats();
            }
        }

        #region Key Listener
        /// <summary>
        /// Key up event listening for arrow keys and escape key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyUp(object sender, KeyEventArgs e) {
            //Determine which key was pressed and outsource to the appropiate click event
            if (e.Key == Key.Up) {
                BtnMoveUp_Click(null, null);
            } else if (e.Key == Key.Down) {
                BtnMoveDown_Click(null, null);
            } else if (e.Key == Key.Right) {
                BtnMoveRight_Click(null, null);
            } else if (e.Key == Key.Left) {
                BtnMoveLeft_Click(null, null);
            } else if (e.Key == Key.Escape) {
                BtnPause_Click(null, null);
            }
        }
        #endregion

        #region Click Events
        /// <summary>
        /// Click event for btnMoveUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMoveUp_Click(object sender, RoutedEventArgs e) {
            Game.Map.MoveHero(Actor.Directions.Up);
            ShowMapContents();
            CheckForEncounters();
            UpdateHeroStats();
            CheckGameState();
        }

        /// <summary>
        /// Click event for btnMoveDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMoveDown_Click(object sender, RoutedEventArgs e) {
            Game.Map.MoveHero(Actor.Directions.Down);
            ShowMapContents();
            CheckForEncounters();
            UpdateHeroStats();
            CheckGameState();
        }

        /// <summary>
        /// Click event for btnMoveRight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMoveRight_Click(object sender, RoutedEventArgs e) {
            Game.Map.MoveHero(Actor.Directions.Right);
            ShowMapContents();
            CheckForEncounters();
            UpdateHeroStats();
            CheckGameState();
        }

        /// <summary>
        /// Click event for btnMoveLeft
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMoveLeft_Click(object sender, RoutedEventArgs e) {
            Game.Map.MoveHero(Actor.Directions.Left);
            ShowMapContents();
            CheckForEncounters();
            UpdateHeroStats();
            CheckGameState();
        }

        /// <summary>
        /// Click event for btnPause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPause_Click(object sender, RoutedEventArgs e) {
            //Open pause menu
            frmPause pause = new frmPause();
            pause.ShowDialog();
        }

        /// <summary>
        /// Click event for btnQuit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuit_Click(object sender, RoutedEventArgs e) {
            //Exits game
            Application.Current.Shutdown();
        }
        #endregion

        #region Other Private Methods
        /// <summary>
        /// Private method that calls appropiate methods to populate grdGameBoard
        /// </summary>
        /// <param name="numOfRows"> Number of rows </param>
        /// <param name="NumOfColumns"> Number of columns </param>
        private void PopulateGrdGameBoard(int numOfRows, int numOfColumns) {
            SetGridControls(numOfRows, numOfColumns);
            AddButtons(numOfRows, numOfColumns);
        }

        /// <summary>
        /// Private method that adds the appropiate number of grid controls to grdGameBoard
        /// </summary>
        /// <param name="numOfRows"> Number of rows </param>
        /// <param name="NumOfColumns"> Number of columns </param>
        private void SetGridControls(int numOfRows, int NumOfColumns) {
            //Clear any existing row and column definitions
            grdGameBoard.RowDefinitions.Clear();
            grdGameBoard.ColumnDefinitions.Clear();

            //Set row definitions
            for (int i = 0; i < numOfRows; i++) {
                grdGameBoard.RowDefinitions.Add(new RowDefinition());
            }
            //Set column definitions
            for (int j = 0; j < NumOfColumns; j++) {
                grdGameBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        /// <summary>
        /// Private method that adds buttons to grdGameBoard
        /// </summary>
        /// <param name="numOfRows"> Number of rows </param>
        /// <param name="numOfColumns"> Number of columns </param>
        private void AddButtons(int numOfRows, int numOfColumns) {
            for (int i = 0; i < grdGameBoard.RowDefinitions.Count; i++) {
                for (int j = 0; j < grdGameBoard.ColumnDefinitions.Count; j++) {
                    Button btn = new Button() { Name = "btn_" + i + "_" + j };
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    grdGameBoard.Children.Add(btn);
                }
            }
        }

        /// <summary>
        /// Method to display the location and type of everything within class Map
        /// </summary>
        public void ShowMapContents() {
            //Get the Hero's current coordinates
            int xCoordinate = Game.Map.Hero.MapPosition[0];
            int yCoordinate = Game.Map.Hero.MapPosition[1];

            //For each Button in the grid...
            foreach (Button btn in grdGameBoard.Children) {
                //Get it's location from its name to match the corresponding MapCell
                string[] name = btn.Name.Split('_');
                int x = int.Parse(name[1]);
                int y = int.Parse(name[2]);
                //Check if the MapCell has been discovered and then mark with 'X'
                if (Game.Map.GameBoard[x, y].IsDiscovered) {
                    btn.Content = "";
                    btn.Background = null;

                    //Check if it contains an Item and then display the Item's name
                    if (Game.Map.GameBoard[x, y].ContainsItem) {
                        btn.Content = Game.Map.GameBoard[x, y].Item.Name;
                        btn.Background = new SolidColorBrush(Colors.LightBlue);
                    }
                    //Check if it contains a Monster and then display the Monster's name
                    if (Game.Map.GameBoard[x, y].ContainsMonster) {
                        //Check if Monster is alive and display name if true
                        if (Game.Map.GameBoard[x, y].Monster.IsAlive == true) {
                            btn.Content = Game.Map.GameBoard[x, y].Monster.Name;
                            btn.Background = new SolidColorBrush(Colors.Red);
                        } else {
                            //Do nothing and leave it as a regular discovered MapCell
                        }
                    }
                    //Check if Hero is at MapCell and then mark with "!Hero!"
                    if (x == xCoordinate && y == yCoordinate) {
                        btn.Content = "!Hero!";
                        btn.Background = new SolidColorBrush(Colors.ForestGreen);
                    }
                } else {
                    btn.Background = new SolidColorBrush(Colors.Black);
                }
            }
        }

        /// <summary>
        /// Private method to update all Hero stats
        /// </summary>
        private void UpdateHeroStats() {
            tbHeroNameAndTitle.Text = Game.Map.Hero.NameAndTitle;
            tbHeroHP.Text = Game.Map.Hero.HP + "/" + Game.Map.Hero.MaxHP;
            if (Game.Map.Hero.HasWeapon == true) {
                tbHeroWeapon.Text = Game.Map.Hero.EquippedWeapon.Name;
            } else {
                tbHeroWeapon.Text = "None";
            }
            if (Game.Map.Hero.DoorKey != null) {
                tbHeroHasKey.Text = "Yes";
            } else {
                tbHeroHasKey.Text = "No";
            }
        }

        /// <summary>
        /// Private method to check if Hero's position contains an Item or a Monster
        /// </summary>
        private void CheckForEncounters() {
            //Get Hero's position
            int xCoordinate = Game.Map.Hero.MapPosition[0];
            int yCoordinate = Game.Map.Hero.MapPosition[1];
            //Check if Hero's position contains an Item
            if (Game.Map.GameBoard[xCoordinate, yCoordinate].ContainsItem == true) {

                //Check if Item is the Door
                if (Game.Map.GameBoard[xCoordinate, yCoordinate].Item.GetType() == typeof(Door)) {
                    frmGameOver go = new frmGameOver();
                    go.ShowDialog();
                    //All other Items
                } else {
                    frmItem item = new frmItem(xCoordinate, yCoordinate);
                    item.ShowDialog();
                }
                //Check if Hero's position conatins a Monster
            } else if (Game.Map.GameBoard[xCoordinate, yCoordinate].ContainsMonster == true) {
                    frmMonster monster = new frmMonster(xCoordinate, yCoordinate);
                    monster.ShowDialog();
            }
        }

        /// <summary>
        /// Private method to check the current game state and display frmGameOver when appropiate
        /// </summary>
        private void CheckGameState() {
            //Check if Hero has died
            if (Game.GameState == Game.State.Lost) {
                frmGameOver go = new frmGameOver();
                go.Show();
                this.Close();
            }
        }
        #endregion

        #region Serialization and Deserialization Methods
        /// <summary>
        /// Private method for binary serilization to save game
        /// </summary>
        /// <param name="map"> Map object to be serialized </param>
        public void Serialize(Map map) {
            FileStream fs = null;
            try {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "My Files(*.map)|*.map|All Files(*.*)|*.*";
                if (sfd.ShowDialog() == true) {
                    fs = new FileStream(sfd.FileName, FileMode.Create);
                } else {
                    //fs = new FileStream("../../Data/test.bin", FileMode.Create);
                }
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, map);
            } catch (Exception ex) {
                MessageBox.Show("Notice: Game was not saved.");
            } finally {
                if (fs != null) {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// Private method for deserialization to load game
        /// </summary>
        public void Deserialize() {
            FileStream fs = null;
            try {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "My Files(*.map)|*.map|All Files(*.*)|*.*";
                if (ofd.ShowDialog() == true) {
                    //We got our file
                    fs = File.Open(ofd.FileName, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    Map map = (Map)bf.Deserialize(fs);
                    Game.Map = map;
                    Game.Height = map.GameBoard.GetLength(0);
                    Game.Width = map.GameBoard.GetLength(1);
                } else {
                    //No file was selected so load new game
                    Game.ResetGame();
                }
            } catch (Exception ex) {
                //The code broke
            } finally {
                if (fs != null) {
                    fs.Close();
                }
            }
        }
        #endregion

    }
}
