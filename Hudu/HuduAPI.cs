using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RestSharp;
using SyncHuduSyncro.Hudu.Models;

namespace SyncHuduSyncro.Hudu
{

    /// <summary>
    /// Used to interact with the Hudu API. 
    ///     Pull all asset entries
    ///     Update specific fields
    ///     Update associations
    /// </summary>
    public class HuduAPI
    {
        private List<HuduAsset> assets = new List<HuduAsset>();
        public List<HuduAsset> SyncroAssets { get { return assets.Where(I => I.cards.Any(C => C.integrator_name == "syncro")).ToList(); } }
        public List<HuduAsset> AllAssets { get { return assets.ToList(); } }

        public void UpdateAssetList()
        {
            assets = new List<HuduAsset>();


            var baseUrl = ConfigurationManager.AppSettings.Get("Hudu_URL");
            var authToken = ConfigurationManager.AppSettings.Get("Hudu_Token");

            if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(authToken))
                throw new Exception("Invalid Hudu Config Values.");

            for (var pageIndex = 1; pageIndex < 200; pageIndex++)
            {
                var client = new RestClient(baseUrl);
                var request = new RestRequest("assets");
                request.AddHeader("x-api-key", authToken);
                request.AddParameter("page_size", 250);
                request.AddParameter("page", pageIndex);

                var result = client.ExecuteAsync<HuduAssetQueryResult>(request).GetAwaiter().GetResult();

                if (result.StatusCode != System.Net.HttpStatusCode.OK || result.Data == null)
                {
                    Console.WriteLine("Failed to query assets.");
                    break;
                }
                else if (result.Data.assets.Count == 0)
                    break;

                else
                    assets.AddRange(result.Data.assets);
            }

        }


        public bool UpdateHuduAsset(HuduUpdateAssetRequest asset)
        {
            var success = false;

            var baseUrl = ConfigurationManager.AppSettings.Get("Hudu_URL");
            var authToken = ConfigurationManager.AppSettings.Get("Hudu_Token");

            if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(authToken))
                throw new Exception("Invalid Hudu Config Values.");

            var client = new RestClient(baseUrl);
            var request = new RestRequest($"companies/{asset.asset.company_id}/assets/{asset.asset.id}", Method.Put);
            request.AddHeader("x-api-key", authToken);
            request.AddJsonBody(asset);
            var result = client.ExecuteAsync(request).GetAwaiter().GetResult();
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                success = true;
            
            return success;
        }
    }
}
