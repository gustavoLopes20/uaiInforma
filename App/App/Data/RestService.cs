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
using System.Net.Http;
using System.Net.Http.Headers;
using App.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using App.CommunicationsModels;

namespace App.Data
{

    class RestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://192.168.43.120:54402")
            };
        }

        // Retornar todas as unidades de atendimento

        public async Task<List<Unidades>> ListUnidades()
        {

            try
            {
                var unidades = await client.GetAsync("/api/Unidades");

                if (unidades.IsSuccessStatusCode)
                {
                    var json = await unidades.Content.ReadAsStringAsync();

                    var res = JsonConvert.DeserializeObject<List<Unidades>>(json);

                    return res;
                }
            }
            catch(InvalidCastException e)
            {
                throw e;
            }

            return null;

        }


        // Retornar todas as escalas de um determinado medico/setor em uma determinada data

        public async Task<List<Plantoes>> ListPlantoes(string url,PlantoesResquetModel Model)
        {

            try
            {

                var content = new StringContent(JsonConvert.SerializeObject(Model), Encoding.UTF8, "application/json");

                var plantoes = client.PostAsync(url, content).Result;

                if (plantoes.IsSuccessStatusCode)
                {
                    var json = await plantoes.Content.ReadAsStringAsync();

                    var res = JsonConvert.DeserializeObject<List<Plantoes>>(json);

                    return res;
                }
            }
            catch (InvalidCastException e)
            {
                throw e;
            }

            return null;

        }

    }
}