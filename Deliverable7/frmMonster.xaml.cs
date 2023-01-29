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
    /// Interaction logic for frmMonster.xaml
    /// </summary>
    public partial class frmMonster : Window {
        /// <summary>
        /// Overloaded constructor for frmMonster
        /// </summary>
        /// <param name="xCoordinate"> Hero's X Coordinate </param>
        /// <param name="yCoordinate"> Hero's Y Coordinate </param>
        public frmMonster(int xCoordinate, int yCoordinate) {
            //Load frmCombatTutorial if this is the first time combat is entered
            if (Game.IsFirstCombat) {
                frmCombatTutorial ct = new frmCombatTutorial();
                ct.ShowDialog();
            }
            InitializeComponent();
            tbMonsterEncounter.FontSize = 40;
            tbMonsterEncounter.TextWrapping = TextWrapping.Wrap;
            tbMonsterEncounter.Text = "You encountered " + Game.Map.GameBoard[xCoordinate, yCoordinate].Monster.Name;
            UpdateStats();
        }

        #region Click Events
        /// <summary>
        /// Click event for btnRunAway
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRunAway_Click(object sender, RoutedEventArgs e) {
            //Get Hero's coordinates and set Hero.IsRunningAway to true
            int xCoordinate = Game.Map.Hero.MapPosition[0];
            int yCoordinate = Game.Map.Hero.MapPosition[1];
            Game.Map.Hero.IsRunningAway = true;
            //Hero tries to run, gets attacked if Monster is faster
            bool isHeroAlive = Game.Map.Hero + Game.Map.GameBoard[xCoordinate, yCoordinate].Monster;
            //If Hero died, change Game.GameState to Lost and close form
            if (isHeroAlive == false) {
                Game.GameState = Game.State.Lost;
                this.Close();
            }
            //If Hero lives, put Hero.IsRunningAway back to false and close form
            Game.Map.Hero.IsRunningAway = false;
            this.Close();
        }

        /// <summary>
        /// Click event for btnAttack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAttack_Click(object sender, RoutedEventArgs e) {
            //Get Hero's coordinates
            int xCoordinate = Game.Map.Hero.MapPosition[0];
            int yCoordinate = Game.Map.Hero.MapPosition[1];
            //Hero and Monster fight
            bool isHeroAlive = Game.Map.Hero + Game.Map.GameBoard[xCoordinate, yCoordinate].Monster;
            //If Hero died, change Game.GameState to Lost and close form
            if(isHeroAlive == false) {
                Game.GameState = Game.State.Lost;
                this.Close();
            }
            //If Monster died, inform user and close form
            if (Game.Map.GameBoard[xCoordinate, yCoordinate].Monster.IsAlive == false) {
                //Check if it was the Boss
                if (Game.Map.GameBoard[xCoordinate, yCoordinate].Monster.GetType() == typeof(Boss)) {
                    Boss deadBoss = (Boss)Game.Map.GameBoard[xCoordinate, yCoordinate].Monster;
                    //Give DoorKey to Hero
                    Game.Map.Hero.ApplyItem(deadBoss.Key);
                    MessageBox.Show("You defeated Grawgnog and got the key.");
                    Game.Map.GameBoard[xCoordinate, yCoordinate].Monster = null;
                    this.Close();
                } else {
                    MessageBox.Show("You killed " + Game.Map.GameBoard[xCoordinate, yCoordinate].Monster.Name);
                    Game.Map.GameBoard[xCoordinate, yCoordinate].Monster = null;
                    this.Close();
                }
            } else {
                UpdateStats();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Private method to update Hero and Monster stats
        /// </summary>
        private void UpdateStats() {
            //Update Hero's stats
            tbHeroHealth.Text = Game.Map.Hero.HP + "/" + Game.Map.Hero.MaxHP;
            tbHeroAttack.Text = Game.Map.Hero.AttackDamage.ToString();
            tbHeroSpeed.Text = Game.Map.Hero.AttackSpeed.ToString();
            //Get Hero's coordinates
            int xCoordinate = Game.Map.Hero.MapPosition[0];
            int yCoordinate = Game.Map.Hero.MapPosition[1];
            //Update Monster's stats
            tbMonsterHealth.Text = Game.Map.GameBoard[xCoordinate, yCoordinate].Monster.HP + "/" + Game.Map.GameBoard[xCoordinate, yCoordinate].Monster.MaxHP;
            tbMonsterAttack.Text = Game.Map.GameBoard[xCoordinate, yCoordinate].Monster.AttackValue.ToString();
            tbMonsterSpeed.Text = Game.Map.GameBoard[xCoordinate, yCoordinate].Monster.AttackSpeed.ToString();
        }
        #endregion
    }
}
