using System;
using System.Collections.Generic;
using System.Text;

namespace iMISClubs.Models
{
    public enum MenuItemType
    {
        Browse,
        Scan
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
