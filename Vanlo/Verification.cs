using System.Collections.Generic;

namespace Vanlo {
    public class Verification : Resource {
        public bool success { get; set; }
        public List<Error> errors { get; set; }
    }
}
