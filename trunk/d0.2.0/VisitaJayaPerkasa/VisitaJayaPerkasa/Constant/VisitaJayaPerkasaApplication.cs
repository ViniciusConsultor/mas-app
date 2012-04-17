using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Form;
using Telerik.WinControls.UI;

namespace VisitaJayaPerkasa.Constant
{
    public static class VisitaJayaPerkasaApplication
    {
        public static LoginForm loginForm;
        public static MainForm mainForm;
        public static PBarDialog pBarForm;

        public static object objGetOtherView;
        public static RadRibbonBarGroup RBGroup = null;

        public static string cboDefaultText = "-- Choose --";
        public static string strGeneralCustomer = "General Customer";
        public static string roleAdmin = "administrator";
        public static string connectionString = "Initial Catalog=ShippingMain;";
        public static string nameFile = "Connection.txt";
        public static bool anyConnection = true;
    }
}