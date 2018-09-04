using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Walker.Crawler;
using API.Walker.Tester;
using Newtonsoft.Json;

namespace API.Walker.Console {
    class Program {
        static void Main(string[] args) {

            while (true) {
                var gen = new SchemaGenerator(new List<string> { @"C:\Users\toshe\source\repos\Test\Test\bin\Test.dll" });

                var schema = gen.Generate(url: "http://localhost:61588");

                Persistence.IO.Save(schema, @"C:\Users\toshe\Desktop\Test.schema");

                var endpoint = schema.Controllers[0].Endpoints[0];

                System.Console.WriteLine(JsonConvert.SerializeObject(schema, Formatting.Indented));

                System.Console.WriteLine("============================");

                System.Console.WriteLine(schema.Do(endpoint));

                System.Console.WriteLine("============================");

                System.Console.WriteLine(Request.Do(schema, schema.Controllers[0].Endpoints[1], Input.Get(schema.Controllers[0].Endpoints[1].Input)));

                System.Console.WriteLine("============================");

                System.Console.WriteLine(schema.Do(schema.Controllers[0].Endpoints[2]));

                System.Console.ReadKey();
            }
        }
    }
}
