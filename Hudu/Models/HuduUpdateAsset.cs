using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Serializers;
using RestSharp.Serializers.Json;

namespace SyncHuduSyncro.Hudu.Models
{

    public class HuduUpdateAssetRequest
    {
        public HuduUpdateAssetRequest(HuduAsset huduAsset, HuduAsset huduContact)
        {
            asset = new HuduUpdateAsset(huduAsset, huduContact);
        }

        public HuduUpdateAsset asset { get; set; }
    }

    public class HuduUpdateAsset
    {
        public HuduUpdateAsset(HuduAsset asset, HuduAsset contact)
        {
            id = asset.id;
            company_id = asset.company_id;

            asset_layout_id = asset.asset_layout_id;
            name = asset.name;

            if (contact != null)
            {
                var dataString = System.Text.Json.JsonSerializer.Serialize(new HuduAssetParent()
                {
                    id = contact.id,
                    url = $"/a/{contact.slug}",
                    name = contact.name
                });
                custom_fields = new List<HuduCustomField>()
                {
                    new HuduCustomField(){Contact = $"[{dataString}]" }
                };
            }
        }

        public int id { get; set; }
        public int asset_layout_id { get; set; }

        public int company_id { get; set; }
        public string name { get; set; }
        public List<HuduCustomField> custom_fields { get; set; } = new List<HuduCustomField>();

        public class HuduCustomField
        {
            public string Contact { get; set; }
        }
        public class HuduAssetParent
        {
            public int id { get; set; }
            public string url { get; set; }
            public string name { get; set; }
        }
    }
}
