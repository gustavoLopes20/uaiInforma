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
    public class Fragment2 : Fragment
    {
        private List<Unidades> listUnidades;
        private ListView listView;
        private Spinner spinner;
        private List<string> itens;
        private long unidadeAtual;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
                
            Task.Run(async () =>
            {

                var rest = new RestService();

                listUnidades = await rest.ListUnidades();

            }).Wait();

            unidadeAtual = -1;
        }

        public static Fragment2 NewInstance()
        {
            var frag2 = new Fragment2 { Arguments = new Bundle() };
            return frag2;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment2, null);


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
            var espec = itens[i];

            foreach (var u in listUnidades)
            {
               if(u.Id == unidadeAtual)
                {
                    foreach (var es in u.especialidades)
                    {
                        if (es.Especialidade.Descricao == espec)
                        {
                            var especialiadesIntent = new Intent(Activity, typeof(EspecialidadesActivity));

                            especialiadesIntent.PutExtra("EspecialidadeUnidade", JsonConvert.SerializeObject(es));

                            StartActivity(especialiadesIntent);
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
                if (u.Descricao == und.ToString())
                {
                    unidadeAtual = u.Id;

                    foreach (var es in u.especialidades)
                    {
                        items.Add(es.Especialidade.Descricao);
                    }
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