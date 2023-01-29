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
    /// Interaction logic for frmWelcomeTutorial.xaml
    /// </summary>
    public partial class frmWelcomeTutorial : Window {
        public frmWelcomeTutorial() {
            InitializeComponent();
            tbTutorial.Text = TutorialText();
        }

        /// <summary>
        /// Click event for btnClose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCloseTutorial_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Private method to write the text for tbTutorial
        /// </summary>
        /// <returns> String tutorial </returns>
        private string TutorialText() {
            string tutorialText = "Welcome to the dungeon.\r\n" +
                "As you navigate, you'll find weapons to help you and potions to heal you.\r\n" +
                "Monsters are there to challenge and kill you. You'll need to kill them first.\r\n" +
                "Somewhere in the dungeon, there is a door. Find it to escape. But you'll need a key.\r\n" +
                "A strong monster roams the dungeon - Grawgnog the Indifferent.\r\n" +
                "On his belt, is the key to success... literally and metephorically.\r\n" +
                "But he won't just give it to you! You'll have to take it, by force, if you want to win.\r\n" +
                "You can use the arrow keys to move your Hero and press ESC to access the pause menu\r\n\r\n" +
                "Are you ready?";
            return tutorialText;
        }
    }
}
