using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using App.Activitys;
using App.Data;
using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Fragments
{
    public class Fragment3 : Fragment
    {
        private List<Unidades> listUnidades;
        private ListView listView;
        private Spinner spinner;
        private List<string> itens;
        private long unidadaIdAtual;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            unidadaIdAtual = 0;

            Task.Run(async () =>
            {

                var rest = new RestService();

                listUnidades = await rest.ListUnidades();

            }).Wait();
        }

        public static Fragment3 NewInstance()
        {
            var frag3 = new Fragment3 { Arguments = new Bundle() };
            return frag3;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment3, null);


            spinner = view.FindViewById<Spinner>(Resource.Id.spinnerUnidades);

            listView = view.FindViewById<ListView>(Resource.Id.List);


            List<string> unidades = new List<string>();

            unidades.Add("-- Selecione uma unidade --");

            foreach (var u in listUnidades)
            {
                unidades.Add(u.Descricao);
            }


            //spinner

            var adapterS = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, unidades);
            spinner.Adapter = adapterS;

            spinner.ItemSelected += Spinner_ItemSelected;

            //listview

            listView.ItemClick += ListView_ItemClick;


            return view;
        }

         //listview event click

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var i = e.Position;
            var setor = itens[i];

            foreach (var u in listUnidades)
            {
                if(u.Id == unidadaIdAtual)
                {
                    foreach (var s in u.setores)
                    {
                        if (s.Setor.Descricao == setor)
                        {
                            var plantoesIntent = new Intent(Activity, typeof(PlantoesActivity));

                            plantoesIntent.PutExtra("SetorUnidade", JsonConvert.SerializeObject(s));

                            StartActivity(plantoesIntent);
                        }
                    }
                }

            }

        }

        //spiner event selected

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            var und = spinner.GetItemAtPosition(e.Position);

            List<string> items = new List<string>();

            foreach (var u in listUnidades)
            {
                if(u.Descricao == und.ToString())
                {
                    foreach(var s in u.setores)
                    {
                        items.Add(s.Setor.Descricao);
                    }
                    unidadaIdAtual = u.Id;
                }
            }
            itens = new List<string>();
            itens = AtualizaItems(items);
            

        }


        //update listview

        public List<string> AtualizaItems(List<string> items)
        {
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, items);
            listView.Adapter = adapter;

            return items;
        }

    }
}