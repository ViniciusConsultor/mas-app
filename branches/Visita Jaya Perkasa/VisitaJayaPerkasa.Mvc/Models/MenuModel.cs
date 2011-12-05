using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class Menu
    {
        public List<MenuItem> Items = new List<MenuItem>();
    }

    public class MenuItem
    {
        public string Url;
        public string Text;
        public string ImageUrl;
        public string Alt;
        public string Section;
        public List<MenuItem> Items = new List<MenuItem>();
    }

    public class MenuModel
    {
        public bool IsAdmin = false;
        public bool IsUser = false;

        public string Portal50Url;

        public Menu Menu;
    }
}