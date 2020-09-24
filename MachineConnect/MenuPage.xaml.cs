using MachineConnectApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MachineConnectOEM
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void btnManuals_Click(object sender, RoutedEventArgs e)
        {
            MachineManual ctrl = new MachineManual();
            ctrl.Show();
        }
    }
}
