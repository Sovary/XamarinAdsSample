using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinAdsSample
{
    public class MenuItem
    {
        public string Name{get;set;}
        public string Description { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Photo { get; set; }
    }
}