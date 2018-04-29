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
    class Plantoes : AppDataObject
    {
        public long MedicosUnidadeId { get; set; }
        public MedicosUnidades MedicoUnidade { get; set; }

        public long SetorUnidadeId { get; set; }
        public SetoresUnidades SetorUnidade { get; set; }

        public long EspecialidadeUnidadeId { get; set; }
        public EspecialidadesUnidades EspecialidadeUnidade { get; set; }

        public long UnidadeId { get; set; }
        public Unidades Unidade { get; set; }

        public DateTime Data { get; set; }

        public DateTime Horario_Inicial { get; set; }
        public DateTime Horario_Final { get; set; }
        public string DataFMT { get; set; }

        public string Hora_Inicial { get; set; }

        public string Hora_Final { get; set; }
    }
}