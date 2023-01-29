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
    /// Interaction logic for frmLoadScreen.xaml
    /// </summary>
    public partial class frmLoadScreen : Window {
        public frmLoadScreen() {
            InitializeComponent();
        }

        #region Click Events
        /// <summary>
        /// Click event for btnNewGame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewGame_Click(object sender, RoutedEventArgs e) {
            //Start a new game
            if (chkChangeMapDimensions.IsChecked == true) {
                frmChangeMapDimensions chgMap = new frmChangeMapDimensions();
                chgMap.ShowDialog();
            } else {
                Game.ResetGame();
            }
            //Show welcome tutorial before game
            frmWelcomeTutorial welcomeTutorial = new frmWelcomeTutorial();
            welcomeTutorial.ShowDialog();
            MainWindow mw = new MainWindow(false);
            mw.Show();
            this.Close();
        }

        /// <summary>
        /// Click event for btnLoadGame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadGame_Click(object sender, RoutedEventArgs e) {
            //Deserialize an old game and skip welcome tutorial
            MainWindow mw = new MainWindow(true);
            mw.Show();
            this.Close();
        }

        /// <summary>
        /// Click event for btnViewCredits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewCredits_Click(object sender, RoutedEventArgs e) {
            //Instantiate and load frmCredits as a modal
            frmCredits credits = new frmCredits();
            credits.ShowDialog();
            //Does not close frmLoadScreen
        }
        #endregion
    }
}
