using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using SyncHuduSyncro.Syncro.Models;

namespace SyncHuduSyncro.Syncro
{
    public class SyncroAPI
    {
        private List<SyncroAssetModel> assets = new List<SyncroAssetModel>();
        public List<SyncroAssetModel> AllAssets { get { return assets.ToList(); } }


        private List<SyncroContactModel> contacts = new List<SyncroContactModel>();
        public List<SyncroContactModel> AllContacts { get { return contacts.ToList(); } }

        public void UpdateAssetList()
        {
            assets = new List<SyncroAssetModel>();


            var baseUrl = ConfigurationManager.AppSettings.Get("Syncro_URL");
            var authToken = ConfigurationManager.AppSettings.Get("Syncro_Token");

            if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(authToken))
                throw new Exception("Invalid Syncro Config Values.");

            var max_pages = 200;
            for (var pageIndex = 1; pageIndex <= max_pages; pageIndex++)
            {
                var client = new RestClient(baseUrl);
                client.Authenticator = new JwtAuthenticator(authToken);
                var request = new RestRequest("customer_assets");
                request.AddParameter("page", pageIndex);

                var result = client.ExecuteAsync<SyncroAssetQueryResult>(request).GetAwaiter().GetResult();

                if (result.StatusCode != System.Net.HttpStatusCode.OK || result.Data == null)
                {
                    Console.WriteLine("Failed to query assets.");
                    break;
                }
                else
                {
                    if (result.Data.assets.Count == 0)
                        break;
                    else
                    {
                        max_pages = result.Data.meta.total_pages;

                        assets.AddRange(result.Data.assets);
                    }
                }
            }


        }

        public void UpdateContactsList()
        {
            contacts = new List<SyncroContactModel>();


            var baseUrl = ConfigurationManager.AppSettings.Get("Syncro_URL");
            var authToken = ConfigurationManager.AppSettings.Get("Syncro_Token");

            if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(authToken))
                throw new Exception("Invalid Syncro Config Values.");

            var max_pages = 200;
            for (var pageIndex = 1; pageIndex <= max_pages; pageIndex++)
            {
                var client = new RestClient(baseUrl);
                client.Authenticator = new JwtAuthenticator(authToken);
                var request = new RestRequest("contacts");
                request.AddParameter("page", pageIndex);

                var result = client.ExecuteAsync<SyncroContactQueryResult>(request).GetAwaiter().GetResult();

                if (result.StatusCode != System.Net.HttpStatusCode.OK || result.Data == null)
                {
                    Console.WriteLine("Failed to query assets.");
                    break;
                }
                else
                {
                    if (result.Data.contacts.Count == 0)
                        break;
                    else
                    {
                        max_pages = result.Data.meta.total_pages;

                        contacts.AddRange(result.Data.contacts);
                    }
                }
            }
        }


        public bool SetHuduAssetLink(int assetId, string huduUrl)
        {
            var baseUrl = ConfigurationManager.AppSettings.Get("Syncro_URL");
            var authToken = ConfigurationManager.AppSettings.Get("Syncro_Token");

            if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(authToken))
                throw new Exception("Invalid Syncro Config Values.");

            var client = new RestClient(baseUrl);
            client.Authenticator = new JwtAuthenticator(authToken);
            var request = new RestRequest($"customer_assets/{assetId}", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Content-Type", "application/json");
            var body = @"{""asset"": {""properties"": {""Hudu"": """+ huduUrl+@"""}}}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var result = client.Execute(request);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }
    }
}
