using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Walker.Models {
    public class Controller {
        public Controller() {
            Endpoints = new List<Endpoint>();
        }

        public string Url { get; set; }
        public List<Endpoint> Endpoints { get; set; }
    }
}
