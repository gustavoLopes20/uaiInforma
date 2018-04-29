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
using App.Models;
using Newtonsoft.Json;
using App.Data;
using System.Threading.Tasks;

namespace App.Activitys
{
    [Activity(Label = "Especialidades")]
    public class EspecialidadesActivity : Activity
    {
        //private List<Especialidades> lstEspec;
        private List<Unidades> lstUnidades;
        private ListView listView;
        private List<string> items;
        private long unidadeAtual;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Fragment2_Data);

            //components

            var textViewEspec = FindViewById<TextView>(Resource.Id.textViewTitle);
            listView = FindViewById<ListView>(Resource.Id.List);


            //model json

            var MyJsonString = Intent.GetStringExtra("EspecialidadeUnidade");
            var especObj = JsonConvert.DeserializeObject<EspecialidadesUnidades>(MyJsonString);

            textViewEspec.Text = especObj.Especialidade.Descricao;

            //adionando itens na listView
            items = new List<string>();

            Task.Run(async () =>
            {
                var rest = new RestService();

                lstUnidades = await rest.ListUnidades();


            }).Wait();

            //buscando os medicos 
            foreach(var u in lstUnidades)
            {
                if(u.Id == especObj.UnidadeId)
                {
                    unidadeAtual = u.Id;

                    foreach(var m in u.medicos)
                    {
                        if(m.EspecialidadeUnidadeId == especObj.Id)
                        {
                            items.Add(m.Medico.Descricao);//lista dos nomes dos medicos
                        }
                    }
                }
            }

            //adcionando na lista

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
            listView.Adapter = adapter;

            listView.ItemClick += ItemClick;

        }

        //click medico event

        private void ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var i = e.Position;
            var med = items[i];

            foreach (var u in lstUnidades)
            {
                if (u.Id == unidadeAtual)
                {
                    foreach (var m in u.medicos)
                    {
                        if (m.Medico.Descricao == med)
                        {
                            var medicosIntent = new Intent(this, typeof(MedicosActivity));

                            // passando dados para outra tela no formato json
                            medicosIntent.PutExtra("Medico", JsonConvert.SerializeObject(m));

                            StartActivity(medicosIntent);
                        }
                    }
                }

            }
        }
    }
}