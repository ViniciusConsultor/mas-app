using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Form;

namespace VisitaJayaPerkasa.Constant
{
    public static class VisitaJayaPerkasaApplication
    {
        public static LoginForm loginForm;
        public static MainForm mainForm;

        public static string cboDefaultText = "-- Choose --";
        public static string roleAdmin = "administrator";
        public static string connectionString = "Data Source=localhost;Initial Catalog=ShippingMain;Integrated Security=True";

    }
}