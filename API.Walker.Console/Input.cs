using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Walker.Console {
    public static class Input {
        public static dynamic Get(IDictionary<string, object> props) {
            var dict = new Dictionary<string, object>();
            foreach (var i in props) {
                System.Console.Write($"Value for {i.Key}: ");
                var val = i.Value as string;
                if (val == typeof(string).FullName ||
                    val == typeof(int).FullName ||
                    val == typeof(double).FullName ||
                    val == typeof(decimal).FullName ||
                    val == typeof(decimal).FullName ||
                    val == typeof(bool).FullName) {
                    dict[i.Key] = System.Console.ReadLine();
                } else {
                    dict[i.Key] = Get((IDictionary<string, object>)i.Value);
                }
            }

            return dict;
        }
    }
}
