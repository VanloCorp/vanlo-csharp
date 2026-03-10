using System.Collections.Generic;
using System.Linq;

namespace Vanlo {
    public class EventList : Resource {
        public List<Event> events { get; set; }
        public bool has_more { get; set; }

        public Dictionary<string, object> filters { get; set; }

        /// <summary>
        /// Get the next page of events based on the original parameters passed to Event.List().
        /// </summary>
        /// <returns>A new Vanlo.EventList instance.</returns>
        public EventList Next() {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = events.Last().id;

            return Event.List(filters);
        }
    }
}
