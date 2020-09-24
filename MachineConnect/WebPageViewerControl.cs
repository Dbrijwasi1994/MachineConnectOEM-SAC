using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MachineConnectApplication
{
    public partial class WebPageViewerControl : UserControl
    {
        public WebPageViewerControl()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.Navigate(Settings.APP_PATH + @"\HelpFiles\help.htm");
            
           
        }
    }
}
