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

namespace App.Adapeters
{
    class PlantoesAdapter : BaseAdapter<Plantoes>
    {
        private List<Plantoes> items;
        private Activity context;

        public PlantoesAdapter(Activity context, List<Plantoes> items)
        :base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Plantoes this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.Fragment1_List, null);

            view.FindViewById<TextView>(Resource.Id.unidade).Text = item.Unidade.Descricao;
            view.FindViewById<TextView>(Resource.Id.setor).Text = item.SetorUnidade.Setor.Descricao;
            view.FindViewById<TextView>(Resource.Id.espec).Text = item.EspecialidadeUnidade.Especialidade.Descricao;
            view.FindViewById<TextView>(Resource.Id.horario).Text = item.Hora_Inicial + " - " + item.Hora_Final;
            view.FindViewById<TextView>(Resource.Id.data).Text = item.DataFMT;
            view.FindViewById<TextView>(Resource.Id.medico).Text = item.MedicoUnidade.Medico.Descricao;


            return view;
        }
    }
}