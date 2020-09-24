using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MachineConnectApplication;

namespace FocasGUI
{
    public partial class UpgradeOEM : UserControl
    {
        int imageIndex = 0;
        List<string> allImages = new List<string>();
        public UpgradeOEM()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {          
            imageIndex++;
            if (imageIndex >= allImages.Count)
            {
                imageIndex = 0;
                pictureBox1.BackgroundImage = Image.FromFile(allImages[imageIndex]);
                return;
            }
            pictureBox1.BackgroundImage = Image.FromFile(allImages[imageIndex]);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {  
            if (imageIndex <= 0)
            {
                imageIndex = allImages.Count-1;
                pictureBox1.BackgroundImage = Image.FromFile(allImages[imageIndex]);
                return;
            }
            imageIndex--;
            pictureBox1.BackgroundImage = Image.FromFile(allImages[imageIndex]);
        }

        private void UpgradeOEM_Load(object sender, EventArgs e)
        {
            string imagePath = Path.Combine(Settings.APP_PATH , "UpgradeImages");
            if (!Directory.Exists(imagePath)) return;
            foreach (string file in Directory.EnumerateFiles(imagePath))
            {
                allImages.Add(file);
            }

            pictureBox1.BackgroundImage = Image.FromFile(allImages[imageIndex]);

        }
    }
}
