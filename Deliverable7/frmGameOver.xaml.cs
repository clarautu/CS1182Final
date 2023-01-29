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
using System.Windows.Shapes;
using CustomClasses;

namespace Deliverable7 {
    /// <summary>
    /// Form that loads when game is won, lost, and when the door is found before the key
    /// </summary>
    public partial class frmGameOver : Window {
        //Private enum to determine which version of frmGameOver to load
        private enum EndType { WonGame, LostGame, NoKey}
        public frmGameOver() {
            InitializeComponent();
            //Determine what kind of end it is
            EndType endType = WhatEndIsIt();

            //Load version for when the game is won
            if(endType == EndType.WonGame) {
                lblWonMessage.Visibility =
                lblWonWhatNow.Visibility =
                btnLoadGame.Visibility =
                btnNewGame.Visibility =
                btnQuit.Visibility = Visibility.Visible;
                //Load version for when game is lost
            } else if(endType == EndType.LostGame) {
                lblLostnMessage.Visibility =
                    lblLostWhatNow.Visibility =
                    btnLoadGame.Visibility =
                    btnNewGame.Visibility =
                    btnQuit.Visibility = Visibility.Visible;
                //Load version for when the door is found without the key
            } else {
                lblNoKeyMessage.Visibility =
                    btnNoKeyContinue.Visibility = Visibility.Visible;
            }
        }

        #region Click Events
        /// <summary>
        /// Click event for btnWonNewGame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewGame_Click(object sender, RoutedEventArgs e) {
            //Start a new game and close this window
            frmLoadScreen ls = new frmLoadScreen();
            ls.Show();
            this.Close();
        }

        /// <summary>
        /// Click event for btnWonLoadGame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadGame_Click(object sender, RoutedEventArgs e) {
            //Instantiate a new MainWindow to load a game, then close this window
            MainWindow mw = new MainWindow(true);
            mw.Show();
            this.Close();
        }

        /// <summary>
        /// Click event for btnWonQuit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuit_Click(object sender, RoutedEventArgs e) {
            //Shutdown the whole application
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Click event for btnNoKeyContinue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNoKeyContinue_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Private method to determine what kind of end it is and what to display
        /// </summary>
        /// <returns> EndType enum </returns>
        private EndType WhatEndIsIt() {
            //Get Hero's position
            int xCoordinate = Game.Map.Hero.MapPosition[0];
            int yCoodinate = Game.Map.Hero.MapPosition[1];

            //Check if Hero is alive
            if (Game.Map.Hero.IsAlive) {
                //See if Hero is at the Door
                if (Game.Map.GameBoard[xCoordinate, yCoodinate].Item.GetType() == typeof(Door)) {
                    //Check if Hero has a DoorKey
                    if (Game.Map.Hero.DoorKey != null) {
                        //Check if DoorKey and Door codes matches
                        Door foundDoor = (Door)(Game.Map.GameBoard[xCoordinate, yCoodinate].Item);
                        bool doesMatch = foundDoor.DoesMatchKey(Game.Map.Hero.DoorKey);
                        if (doesMatch) {
                            return EndType.WonGame;
                        } else {
                            //Code doesn't match
                            MessageBox.Show("You have a key, but it doesn't match the Door... hmmm...");
                            return EndType.NoKey;
                        }
                    } else {
                        //Hero doesn't have a DoorKey
                        return EndType.NoKey;
                    }
                }
                return EndType.WonGame;
            } else {
                //Hero is dead
                return EndType.LostGame;
            }
        }
        #endregion

    }
}
