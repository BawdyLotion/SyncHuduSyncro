using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncHuduSyncro.Syncro.Models
{
    public class SyncroAssetQueryResult
    {
        public List<SyncroAssetModel> assets { get; set; }
        public SyncroAssetQueryMeta meta { get; set; }
    }

    public class SyncroAssetQueryMeta
    {
        public int total_pages { get; set; }
        public int total_entries { get; set; }
        public int per_page { get; set; }
        public int page { get; set; }
    }
    public class SyncroAssetModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int customer_id { get; set; }
        public int? contact_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string asset_type { get; set; }
        public string asset_serial { get; set; }
        public string external_rmm_link { get; set; }
        public bool has_live_chat { get; set; }
        public bool? snmp_enabled { get; set; }
        public object address { get; set; }

        public SyncroAssetPropertiesModel properties { get; set; }


        public class SyncroAssetPropertiesModel
        {
            public string? Hudu { get; set; }
        }
    }
}
