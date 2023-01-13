using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncHuduSyncro.Syncro.Models
{
    public class SyncroContactQueryResult
    {
        public List<SyncroContactModel> contacts { get; set; }
        public SyncroAssetQueryMeta meta { get; set; }
    }

    public class SyncroContactQueryMeta
    {
        public int total_pages { get; set; }
        public int total_entries { get; set; }
        public int per_page { get; set; }
        public int page { get; set; }
    }

    public class SyncroContactModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public int customer_id { get; set; }
        public int account_id { get; set; }
        public string notes { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object vendor_id { get; set; }
        public bool opt_out { get; set; }
        public string extension { get; set; }
        public string processed_phone { get; set; }
        public string processed_mobile { get; set; }
        public string ticket_matching_emails { get; set; }
    }
}
