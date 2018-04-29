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
using Newtonsoft.Json;
using App.Models;
using Android.Support.Design.Widget;
using System.Threading.Tasks;
using App.Data;
using App.CommunicationsModels;
using System.Globalization;
using App.Adapeters;

namespace App.Activitys
{
    [Activity(Label = "PlantoesActivity")]
    public class PlantoesActivity : Activity
    {
        private List<Plantoes> lstPlantoes;
        private ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Fragment3_Data);

            //components

            var textViewMedico = FindViewById<TextView>(Resource.Id.textViewTitle);
            var calenderView = FindViewById<CalendarView>(Resource.Id.calenderView);
            listView = FindViewById<ListView>(Resource.Id.List);



            //model json

            var MyJsonString = Intent.GetStringExtra("SetorUnidade");
            var setorObj = JsonConvert.DeserializeObject<SetoresUnidades>(MyJsonString);

            textViewMedico.Text = setorObj.Setor.Descricao;


            //buscar na data atual
            var hoje = DateTime.UtcNow;
            var hojeFMT = hoje.ToString("dd/MM/yyyy");
            if(!BuscarPlantoes(setorObj, hojeFMT))
            {
                Android.Widget.Toast.MakeText(this, "Não possui plantões", Android.Widget.ToastLength.Short).Show();
            }

            //event calenderView

            calenderView.DateChange += (s, e) =>
            {
                string dataStr = e.DayOfMonth + "/" + (e.Month + 1) + "/" + e.Year;

                //buscar por plantoes
                if(!BuscarPlantoes(setorObj, dataStr))
                {
                    Android.Widget.Toast.MakeText(this, "Não possui plantões", Android.Widget.ToastLength.Short).Show();
                }

            };

        }

        //buscar por plantoes de uma determinado setor

        private bool BuscarPlantoes(SetoresUnidades setor, string dataStr)
        {
            //model resquest

            var model = new PlantoesResquetModel
            {
                Unidade = setor.UnidadeId,
                Setor = setor.Id,
                DataInicioApp = dataStr
            };


            Task.Run(async () =>
            {
                var rest = new RestService();

                lstPlantoes = await rest.ListPlantoes("/api/Plantoes/Medico", model);

            }).Wait();

            //adcionando item na lista personalizada
            if(lstPlantoes != null)
            {
                listView.Adapter = new PlantoesAdapter(this, lstPlantoes);

                if (lstPlantoes.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                        
            }
            else
            {
                return false;
            }
           
        }
    }
}