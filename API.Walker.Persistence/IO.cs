using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Walker.Models;
using Newtonsoft.Json;

namespace API.Walker.Persistence {
    public static class IO {

        public static void Save(this Schema schema, string path) {
            File.WriteAllText(path, JsonConvert.SerializeObject(schema));
        }

        public static Schema Load(string path) {
            return JsonConvert.DeserializeObject<Schema>(File.ReadAllText(path));
        }
    }
}
