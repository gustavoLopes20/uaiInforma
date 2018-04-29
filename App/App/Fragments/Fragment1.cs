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
using System.Linq;

namespace App.Fragments
{
    public class Fragment1 : Fragment
    {
        private List<Unidades> listUnidades;
        private ListView listView;
        private Spinner spinner;
        private List<string> itens;
        private long unidadeIdAtual;
        private List<string> medicos;
        private AutoCompleteTextView autocompleteTextView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            unidadeIdAtual = 0;

            Task.Run(async () =>
            {

                var rest = new RestService();

                listUnidades = await rest.ListUnidades();


            }).Wait();

        }

        public static Fragment1 NewInstance()
        {
            var frag1 = new Fragment1 { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment1, null);


            spinner = view.FindViewById<Spinner>(Resource.Id.spinnerUnidades);

            listView = view.FindViewById<ListView>(Resource.Id.List);


            List<string> unidades = new List<string>();
            medicos = new List<string>();

            unidades.Add("-- Buscar por unidade --");

            foreach (var u in listUnidades)
            {
                unidades.Add(u.Descricao);
                foreach(var m in u.medicos)
                {
                    medicos.Add(m.Medico.Descricao);
                }
            }


            //spinner

            var adapterS = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, unidades);
            spinner.Adapter = adapterS;

            spinner.ItemSelected += Spinner_ItemSelected;


            //busca autocomplet

            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleDropDownItem1Line, medicos);
            autocompleteTextView = view.FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);
            autocompleteTextView.Adapter = autoCompleteAdapter;

            autocompleteTextView.ItemClick += ItemClick;



            //listview

            listView.ItemClick += ListView_ItemClick;


            return view;
        }



        //event busca auto complete

        private void ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            var med = autocompleteTextView.Text;

            foreach (var u in listUnidades)
            {
                foreach (var medico in u.medicos)
                {
                    if (medico.Medico.Descricao == med)
                    {
                        
                        var medicosIntent = new Intent(Activity, typeof(MedicosActivity));

                        // passando dados para outra tela no formato json
                        medicosIntent.PutExtra("Medico", JsonConvert.SerializeObject(medico));

                        StartActivity(medicosIntent);

                        autocompleteTextView.Text = "";
                    }
                }
            }
        }



        //listview event click - redirecionando para outra tela

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var i = e.Position;
            var med = itens[i];

            foreach (var u in listUnidades)
            {
                if(u.Id == unidadeIdAtual)
                {
                    foreach (var m in u.medicos)
                    {
                        if (m.Medico.Descricao == med)
                        {
                            var medicosIntent = new Intent(Activity, typeof(MedicosActivity));

                            // passando dados para outra tela no formato json
                            medicosIntent.PutExtra("Medico", JsonConvert.SerializeObject(m));

                            StartActivity(medicosIntent);
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
                    foreach(var m in u.medicos)
                    {
                        items.Add(m.Medico.Descricao);
                    }
                    unidadeIdAtual = u.Id;
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
