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
    /// Interaction logic for frmCredits.xaml
    /// </summary>
    public partial class frmCredits : Window {
        public frmCredits() {
            InitializeComponent();
        }

        /// <summary>
        /// Click event for btnClose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
