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
    [Activity(Label = "Médicos", LaunchMode = Android.Content.PM.LaunchMode.SingleTop, Icon = "@drawable/icon")]
    public class MedicosActivity : Activity
    {
        private List<Plantoes> lstPlantoes;
        private ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Fragment1_Data);


            //components

            var textViewMedico = FindViewById<TextView>(Resource.Id.textViewTitle);
            var calenderView = FindViewById<CalendarView>(Resource.Id.calenderView);
            listView = FindViewById<ListView>(Resource.Id.List);



            //model json

            var MyJsonString = Intent.GetStringExtra("Medico");
            var medicoObj = JsonConvert.DeserializeObject<MedicosUnidades>(MyJsonString);

            textViewMedico.Text = medicoObj.Medico.Descricao;


            //buscar na data atual
            var hoje = DateTime.UtcNow;
            var hojeFMT = hoje.ToString("dd/MM/yyyy");
            if(!BuscarPlantoes(medicoObj, hojeFMT))
            {
                Android.Widget.Toast.MakeText(this, "Não possui escalas", Android.Widget.ToastLength.Short).Show();
            }

            //event calenderView

            calenderView.DateChange += (s, e) =>
            {
                string dataStr = e.DayOfMonth + "/" + (e.Month + 1) + "/" + e.Year;

                //buscar por plantoes
                if(!BuscarPlantoes(medicoObj, dataStr))
                {
                    //Não possui itens
                    Android.Widget.Toast.MakeText(this, "Não possui escalas", Android.Widget.ToastLength.Short).Show();
                }

            };

        }
        
        //buscar por plantoes

        private bool BuscarPlantoes(MedicosUnidades medico, string dataStr)
        {
            //model resquest

            var model = new PlantoesResquetModel
            {
                Medico = medico.Id,
                Setor = 0,
                Unidade = medico.UnidadeId,
                DataInicioApp = dataStr
            };


            Task.Run(async () =>
            {
                var rest = new RestService();

                lstPlantoes = await rest.ListPlantoes("/api/Plantoes/Medico", model);

            }).Wait();

            if(lstPlantoes.Count != 0)
            {
                //adcionando item na lista personalizada
                listView.Adapter = new PlantoesAdapter(this, lstPlantoes);

                return true;
            }
            else
            {
                listView.Adapter = new PlantoesAdapter(this, lstPlantoes);
                return false;
            }
                     
        }

    }
}