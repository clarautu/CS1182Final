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
    /// Interaction logic for frmItem.xaml
    /// </summary>
    public partial class frmItem : Window {
        /// <summary>
        /// Overloaded constructor for frmItem
        /// </summary>
        /// <param name="xCoordinate"> Hero's X Coordinate </param>
        /// <param name="yCoordinate"> Hero's Y Coordinate </param>
        public frmItem(int xCoordinate, int yCoordinate) {
            InitializeComponent();
            tbFound.FontSize = 40;
            tbFound.HorizontalAlignment = HorizontalAlignment.Center;
            tbFound.VerticalAlignment = VerticalAlignment.Center;

            //If Item is a Weapon, display it's stats and current Weapon's stats
            if(Game.Map.GameBoard[xCoordinate, yCoordinate].Item.GetType() == typeof(Weapon)) {
            DisplayWeapon(xCoordinate, yCoordinate);
            }
        }

        /// <summary>
        /// Click event for btnLeaveIt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLeaveIt_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Click event for btnTakeIt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTakeIt_Click(object sender, RoutedEventArgs e) {
            //Get Hero's position
            int xCoordinate = Game.Map.Hero.MapPosition[0];
            int yCoordinate = Game.Map.Hero.MapPosition[1];
            //Take Item and leave currently equipped Item if applicable
            Game.Map.GameBoard[xCoordinate, yCoordinate].Item =
                Game.Map.Hero.ApplyItem(Game.Map.GameBoard[xCoordinate, yCoordinate].Item);
            //Close form
            this.Close();
        }

        /// <summary>
        /// Private method to display found and current Weapon stats
        /// </summary>
        private void DisplayWeapon( int xCoordinate, int yCoordinate) {
            //Center textblock text
            tbFoundStats.TextAlignment = TextAlignment.Center;
            tbCurrentStats.TextAlignment = TextAlignment.Center;

            //Cast to a Weapon to access its attributes
            Weapon tempWeapon = (Weapon)Game.Map.GameBoard[xCoordinate, yCoordinate].Item;

            tbFound.Text = "You found a " + tempWeapon.Name;
            tbFoundStats.Text = "Found Weapon Stats\r\nDamage: " + tempWeapon.AffectValue + "\r\nLowers Speed By: -" + tempWeapon.AttackSpeedMod;

            //Check if Hero has a Weapon, then display stats
            if (Game.Map.Hero.HasWeapon) {
            tbCurrentStats.Text = "Current Weapon Stats\r\nDamage: " + Game.Map.Hero.EquippedWeapon.AffectValue + "\r\nLowers Speed By: -"
                + Game.Map.Hero.EquippedWeapon.AttackSpeedMod;
            } else {
                //Display N/A
                tbCurrentStats.Text = "Current Weapon Stats\r\nDamage: N/A\r\nLowers Speed By: N/A";
            }
        }
    }
}
