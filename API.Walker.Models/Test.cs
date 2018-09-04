using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Walker.Models {
    public class Test {
        public Test() {
            Input = new Dictionary<string, string>();
            Output = new Dictionary<string, string>();
        }

        public string Url { get; set; }
        public Dictionary<string, string> Input { get; set; }
        public Dictionary<string, string> Output { get; set; }
    }
}
