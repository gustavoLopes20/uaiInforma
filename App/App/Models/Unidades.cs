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
    class Unidades : AppDataObject
    {

        public string Descricao { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Telefone { get; set; }

        public List<SetoresUnidades> setores { get; set; }
        public List<EspecialidadesUnidades> especialidades { get; set; }
        public List<MedicosUnidades> medicos { get; set; }

    }
}