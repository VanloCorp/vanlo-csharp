using RestSharp;

using System;
using System.Collections.Generic;

namespace Vanlo {
    public class Rate : Resource {
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string service { get; set; }
        public string rate { get; set; }
        public string list_rate { get; set; }
        public string retail_rate { get; set; }
        public string currency { get; set; }
        public string list_currency { get; set; }
        public string retail_currency { get; set; }
        public int? est_delivery_days { get; set; }
        public DateTime? delivery_date { get; set; }
        public bool delivery_date_guaranteed { get; set;}
        public int? delivery_days { get; set;}
        public string carrier { get; set; }
        public string shipment_id { get; set; }
        public string carrier_account_id { get; set; }

        /// <summary>
        /// Create standalone rates for the given parameters.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to generate rates with. Valid pairs:
        ///   * {"from_address", Dictionary&lt;string, object&gt;} Origin address.
        ///   * {"to_address", Dictionary&lt;string, object&gt;} Destination address.
        ///   * {"parcel", Dictionary&lt;string, object&gt;} Parcel details.
        /// </param>
        /// <returns>List of Vanlo.Rate instances.</returns>
        public static List<Rate> Create(Dictionary<string, object> parameters) {
            Request request = new Request("/rate", Method.POST);
            request.AddBody(parameters);

            RateListResponse response = request.Execute<RateListResponse>();
            return response.rates;
        }

        private class RateListResponse : Resource {
            public List<Rate> rates { get; set; }
        }
    }
}
