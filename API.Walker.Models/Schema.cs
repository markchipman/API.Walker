using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Walker.Models {
    public class Schema {
        public Schema() {
            Headers = new Dictionary<string, string>();
            Controllers = new List<Controller>();
            Tests = new List<Test>();
        }

        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public List<Controller> Controllers { get; set; }
        public List<Test> Tests { get; set; }
    }
}
