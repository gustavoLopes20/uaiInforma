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

namespace App.CommunicationsModels
{
    class PlantoesResquetModel
    {

       public long Unidade { get; set; }
       public string DataInicioApp { get; set; }
       public long Medico { get; set; }
       public long Setor { get; set; } 
    }
}