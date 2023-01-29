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

namespace Deliverable7 {
    /// <summary>
    /// Interaction logic for frmCombatTutorial.xaml
    /// </summary>
    public partial class frmCombatTutorial : Window {
        public frmCombatTutorial() {
            InitializeComponent();
            //Change Game.IsFirstCombat to false
            Game.IsFirstCombat = false;
            tbTutorial.Text = TutorialText();
        }

        /// <summary>
        /// Click event for btnClose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Private method to write the text for tbTutorial
        /// </summary>
        /// <returns> String tutorial </returns>
        private string TutorialText() {
            string tutorialText = "You have just entered combat for the first time.\r\n" +
                "Succeeding in combat is necessary to win Dungeon Crawler, so pay attention to your and the Monster's stats.\r\n" +
                "The bottom left of the screen displays your stats, while the bottom right displays the Monster's.\r\n\r\n" +
                "Health: Displays current hit points / max amount of hit points\r\n" +
                "Attack Strength: displays how much damage is done to the the other character, if a hit lands\r\n" +
                "Attack Speed: This is an important one. It determines who attacks first and your speed at running away\r\n" +
                "   *If your Attack Speed is greater than the Monster's and you run away, you get away without taking damage\r\n" +
                "   *If your Attack Speed is less than the Monster's and you run away, you'll take damage from the Monster, before you get away\r\n" +
                "   *If your Attack Speed is greater than the Monster's and you attack, the Monster will damage before being able to attack\r\n" +
                "       *Note: If this kills the Monster, you will take no damage from combat\r\n" +
                "   *If your Attack Speed is less than the Monster's and you attack, you'll take damage before being able to attack\r\n" +
                "       *Note: If this kills you, the Monster will take no damage from combat\r\n" +
                "If your health reaches zero at any point, combat will end and the game will be lost.";
            return tutorialText;
        }
    }
}
