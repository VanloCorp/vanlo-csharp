using RestSharp;

using System;
using System.Collections.Generic;

namespace Vanlo {
    public class Report : Resource {
        public string id { get; set; }
        public string @object { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string status { get; set; }
        public string url { get; set; }
        public DateTime? url_expires_at { get; set; }

        /// <summary>
        /// Create a Report of the given type.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the report with. Valid pairs:
        ///   * {"type", string} The type of report to create (e.g. "shipment", "tracker", "payment_log").
        ///   * {"start_date", string} Start date for the report.
        ///   * {"end_date", string} End date for the report.
        /// </param>
        /// <returns>Vanlo.Report instance.</returns>
        public static Report Create(Dictionary<string, object> parameters) {
            string type = (string)parameters["type"];

            Request request = new Request("/reports/" + type, Method.POST);
            request.AddBody(parameters);

            return request.Execute<Report>();
        }

        /// <summary>
        /// Retrieve a Report from its id.
        /// </summary>
        /// <param name="id">String representing a Report ID.</param>
        /// <returns>Vanlo.Report instance.</returns>
        public static Report Retrieve(string id) {
            Request request = new Request("/reports/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Report>();
        }

        /// <summary>
        /// Get a paginated list of reports of the given type.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to filter the list with. Valid pairs:
        ///   * {"type", string} The type of report to list (e.g. "shipment", "tracker", "payment_log").
        ///   * {"before_id", string} Only retrieve reports created before this id.
        ///   * {"after_id", string} Only retrieve reports created after this id.
        ///   * {"page_size", int} Size of page. Default to 20.
        /// </param>
        /// <returns>Instance of Vanlo.ReportList</returns>
        public static ReportList List(Dictionary<string, object> parameters) {
            string type = (string)parameters["type"];
            Dictionary<string, object> query = new Dictionary<string, object>(parameters);
            query.Remove("type");

            Request request = new Request("/reports/" + type);
            request.AddQueryString(query);

            ReportList reportList = request.Execute<ReportList>();
            reportList.filters = parameters;
            return reportList;
        }
    }
}
