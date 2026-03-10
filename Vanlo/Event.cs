using System;
using System.Collections.Generic;

namespace Vanlo {
    public class Event : Resource {
        public string id { get; set; }
        public string @object { get; set; }
        public string description { get; set; }
        public List<string> pending_urls { get; set; }
        public List<string> completed_urls { get; set; }
        public List<string> failed_urls { get; set; }
        public Dictionary<string, object> result { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

        /// <summary>
        /// Get a paginated list of events.
        /// </summary>
        /// Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///   * {"before_id", string} Only retrieve events created before this id.
        ///   * {"after_id", string} Only retrieve events created after this id.
        ///   * {"start_datetime", string} ISO 8601 datetime string. Only retrieve events created after this datetime.
        ///   * {"end_datetime", string} ISO 8601 datetime string. Only retrieve events created before this datetime.
        ///   * {"page_size", int} Size of page. Default to 20.
        /// All invalid keys will be ignored.
        /// <param name="parameters">
        /// </param>
        /// <returns>Instance of Vanlo.EventList</returns>
        public static EventList List(Dictionary<string, object> parameters = null) {
            Request request = new Request("/events");
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            EventList eventList = request.Execute<EventList>();
            eventList.filters = parameters;
            return eventList;
        }
    }
}
