using System.Collections.Generic;

namespace Vanlo {
    public interface IResource {
        void Merge(object source);
        Dictionary<string, object> AsDictionary();
    }
}
