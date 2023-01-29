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
    /// Interaction logic for frmChangeMapDimensions.xaml
    /// </summary>
    public partial class frmChangeMapDimensions : Window {
        public frmChangeMapDimensions() {
            InitializeComponent();
        }

        #region Click Events
        /// <summary>
        /// Click event for btnSetDimensions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSetDimensions_Click(object sender, RoutedEventArgs e) {
            //Try parse height and width
            bool heightParsedCorrectly = int.TryParse(txtHeight.Text, out int height);
            bool widthParsedCorrectly = int.TryParse(txtWidth.Text, out int width);
            //Check if parsed correctly and height and width are greater than 5
            if(heightParsedCorrectly && widthParsedCorrectly && height > 5 && width > 5) {
                Game.ResetGame(height, width);
                this.Close();
            } else {
                //Else show appropiate error message(s)
                if(height <= 5 || width <= 5) {
                lblErrorGreaterThan5.Visibility = Visibility.Visible;
                }
                if(heightParsedCorrectly != true || widthParsedCorrectly != true)
                lblErrorWholeIntegers.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Click event for btnCancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            Game.ResetGame();
            this.Close();
        }

        /// <summary>
        /// Click event for btnDefault
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefault_Click(object sender, RoutedEventArgs e) {
            txtHeight.Text = "10";
            txtWidth.Text = "10";
        }
        #endregion
    }
}
