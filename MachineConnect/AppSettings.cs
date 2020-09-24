using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MachineConnectOEM.SAC
{
    public static class AppSettings
    {
        private static double fontFamilyProcessParameter =  Convert.ToInt32(ConfigurationManager.AppSettings["FontSizeLable"].ToString());
        public static double FontSizeLable
        {
            get { return fontFamilyProcessParameter; }
            set { fontFamilyProcessParameter = value; }
        }

        private static double fontSizeProcessParameter = Convert.ToInt32(ConfigurationManager.AppSettings["FontSizeText"]);
        public static double FontSizeText
        {
            get { return fontSizeProcessParameter; }
            set { fontSizeProcessParameter = value; }
        }

        private static double fontSizeParamValue = Convert.ToInt32(ConfigurationManager.AppSettings["FontSizeParamValue"]);
        public static double FontSizeParamValue
        {
            get { return fontSizeParamValue; }
            set { fontSizeParamValue = value; }
        }
    }
}
