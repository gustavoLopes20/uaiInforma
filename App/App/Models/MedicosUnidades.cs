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
    class MedicosUnidades : AppDataObject
    {
        public long UnidadeId { get; set; }
        public Unidades Unidade { get; set; }

        public long MedicoId { get; set; }
        public Medicos Medico { get; set; }

        public long SetorUnidadeId { get; set; }
        public SetoresUnidades SetorUnidade { get; set; }

        public long EspecialidadeUnidadeId { get; set; }
        public EspecialidadesUnidades EspecialidadeUnidade { get; set; }
    }
}