using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Walker.Models {
    public class Endpoint {
        public Endpoint() {
            Input = new Dictionary<string, dynamic>();
            Output = new Dictionary<string, dynamic>();
        }

        public string Url { get; set; }
        public Consts.Verbs Verb { get; set; }
        public Dictionary<string, dynamic> Input { get; set; }
        public Dictionary<string, dynamic> Output { get; set; }
    }
}
