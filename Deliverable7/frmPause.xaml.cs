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
    /// Interaction logic for frmPause.xaml
    /// </summary>
    public partial class frmPause : Window {
        public frmPause() {
            InitializeComponent();
        }

        #region Click Events
        /// <summary>
        /// Click event for btnSave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            //Instantiate a MainWindow in order to access Serialize method
            MainWindow mw = new MainWindow(null);
            mw.Serialize(Game.Map);
        }

        /// <summary>
        /// Click event for btnLoad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoad_Click(object sender, RoutedEventArgs e) {
            //Instantiate a MainWindow to load an old game and then show it; close this window
            MainWindow mw = new MainWindow(true);
            mw.Show();
            this.Close();
        }

        /// <summary>
        /// Click event for btnClose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
        #endregion
    }
}
