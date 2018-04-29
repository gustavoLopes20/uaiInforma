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
using App.Data;

namespace App.Models
{
    class SetoresUnidades : AppDataObject
    {
        public long UnidadeId { get; set; }

        public Unidades Unidade { get; set; }

        public long SetorId { get; set; }

        public Setores Setor { get; set; }
    }
}