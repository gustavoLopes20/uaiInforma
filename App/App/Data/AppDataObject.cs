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

namespace App.Data
{
    class AppDataObject
    {
        public long Id { get; set; }
        public DateTime Registro {get;set;}
        public DateTime DataUpdate { get; set; }
        public string RID { get; set; }
        public bool Ativo { get; set; }
    }
}