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

namespace MachineConnectOEM.SAC
{
    /// <summary>
    /// Interaction logic for NewDateTime.xaml
    /// </summary>
    public partial class NewDateTime : Window
    {
        private string oldDate;
        public NewDateTime()
        {
            InitializeComponent();
        }

        public NewDateTime(string oldd)
        {
            oldDate = oldd;
            InitializeComponent();
        }

        public string NewDate
        {
            get { return dtpNewDate.Text; }
            set { dtpNewDate.Text = value; }
        }

        private void btnOK_Clicked(object sender, RoutedEventArgs e)
        {
            
            if (!dtpNewDate.Text.Equals(""))
            {
                DialogResult = true;
            }
            this.Close();
        }

        private void btnCancel_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void NewDateTime_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNewDate.Text = oldDate;
            //this.Left = Mouse.GetPosition()
            //Point p = Mouse.GetPosition(this);
            //this.Left = p.X;
            //this.Top = p.Y;
        }
    }
}
