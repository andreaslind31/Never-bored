using Never_bored.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Never_bored.API
{
    public class DataProcessor
    {
        public static async Task<ActivityModel> LoadData()
        {

            string url = ApiHelper.ApiClient.BaseAddress.ToString();


            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ActivityModel data = await response.Content.ReadAsAsync<ActivityModel>();

                    return data;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }

        }
    }
}
