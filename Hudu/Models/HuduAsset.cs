using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncHuduSyncro.Hudu.Models
{
    public class HuduAssetQueryResult
    {
        public List<HuduAsset> assets { get; set; }
    }

    public class HuduAsset
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public int asset_layout_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string company_name { get; set; }
        public string object_type { get; set; }
        public string asset_type { get; set; }
        public bool archived { get; set; }
        public string url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<HuduAssetCard> cards { get; set; }
        public List<HuduAssetField> fields { get; set; }

        public class HuduAssetCard
        {
            public int id { get; set; }
            public int integrator_id { get; set; }
            public string integrator_name { get; set; }
            public string link { get; set; }
            public string primary_field { get; set; }
            public string sync_type { get; set; }
            public int? sync_id { get; set; }
            public string sync_identifier { get; set; }
        }
    }
}
