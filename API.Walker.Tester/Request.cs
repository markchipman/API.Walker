using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using API.Walker.Crawler.Loaders;
using API.Walker.Models;
using Newtonsoft.Json;
using RestSharp;

namespace API.Walker.Tester {
    public static class Request {
        public static string Do(this Schema schema, Endpoint end) {
            return Req(schema, end, Randomize(end.Input));

        }

        public static string Do(this Schema schema, Endpoint end, IDictionary<string, object> values) {
            return Req(schema, end, Fill(end.Input, values));
        }

        private static string Req(Schema schema, Endpoint end, dynamic body) {
            var client = new RestClient(end.Url);
            var method = Method(end.Verb);
            var request = new RestRequest(end.Url, method);

            if (method == RestSharp.Method.GET)
                //foreach (var p in (Dictionary<string, object>)body)
                foreach (var p in Get(end.Input, (IDictionary<string, object>) body))
                    request.AddParameter(p.Key, p.Value);
            else request.AddJsonBody(body);

            //Console.WriteLine(JsonConvert.SerializeObject(request, Formatting.Indented));

            // easily add HTTP Headers
            if (schema.Headers != null)
                foreach (var h in schema.Headers)
                    request.AddHeader(h.Key, h.Value);

            // execute the request
            var response = client.Execute(request);
            return response.Content; // raw content as string
        }

        private static IDictionary<string, string> Get(IDictionary<string, object> props, IDictionary<string, object> values, string pre = "") {
            var dict = new Dictionary<string, string>();
            foreach (var i in props) {
                var val = i.Value as string;
                if (val == typeof(string).FullName ||
                    val == typeof(int).FullName ||
                    val == typeof(double).FullName ||
                    val == typeof(decimal).FullName ||
                    val == typeof(decimal).FullName ||
                    val == typeof(bool).FullName) {
                    dict[string.IsNullOrEmpty(pre) ? i.Key : pre + "." + i.Key] = values[i.Key].ToString();
                } else {
                    foreach (var v in Get((IDictionary<string, object>)i.Value, 
                        (IDictionary<string, object>)values[i.Key], 
                        string.IsNullOrEmpty(pre) ? i.Key : pre + "." + i.Key))
                        dict.Add(v.Key, v.Value);
                }
            }

            return dict;
        }

        private static string Unwrap(IDictionary<string, object> values, StringBuilder sb) {
            foreach (var i in values) {
                var val = i.Value as string;
                if (val == typeof(string).FullName ||
                    val == typeof(int).FullName ||
                    val == typeof(double).FullName ||
                    val == typeof(decimal).FullName ||
                    val == typeof(decimal).FullName ||
                    val == typeof(bool).FullName) {
                    sb.Append(i.Key + "=").Append(values[i.Key]).Append("&");
                } else {
                    sb.Append(i.Key + "=").Append(Unwrap((IDictionary<string, object>)i.Value, sb)).Append("&");
                }
            }

            return sb.ToString();
        }

        private static Method Method(Consts.Verbs verb) {
            switch (verb) {
                case Consts.Verbs.DELETE: return RestSharp.Method.DELETE;
                case Consts.Verbs.PATCH: return RestSharp.Method.PATCH;
                case Consts.Verbs.POST: return RestSharp.Method.POST;
                case Consts.Verbs.PUT: return RestSharp.Method.PUT;
                default: return RestSharp.Method.GET;
            }
        }

        private static dynamic Fill(IDictionary<string, object> props, IDictionary<string, object> values) {
            var dict = new Dictionary<string, object>();
            foreach (var i in props) {
                var val = i.Value as string;
                if (val == typeof(string).FullName ||
                    val == typeof(int).FullName ||
                    val == typeof(double).FullName ||
                    val == typeof(decimal).FullName ||
                    val == typeof(decimal).FullName ||
                    val == typeof(bool).FullName) {
                    dict[i.Key] = values[i.Key];
                } else {
                    dict[i.Key] = Fill((IDictionary<string, object>)i.Value, (IDictionary<string, object>)values[i.Key]);
                }
            }

            return dict;
        }


        private static dynamic Randomize(IDictionary<string, object> props) {
            var dict = new Dictionary<string, object>();
            var r = new Random();
            foreach (var i in props) {
                var val = i.Value as string;
                if (val == typeof(string).FullName) {
                    dict[i.Key] = string.Concat(Enumerable.Range(0, r.Next(5, 20)).Select(x => (char)r.Next('A', 'z')));

                } else if (val == typeof(int).FullName) {
                    dict[i.Key] = r.Next();

                } else if (val == typeof(double).FullName) {
                    dict[i.Key] = r.NextDouble();

                } else if (val == typeof(decimal).FullName) {
                    dict[i.Key] = (decimal)r.NextDouble();

                } else if (val == typeof(decimal).FullName) {
                    dict[i.Key] = (float)r.NextDouble();

                } else if (val == typeof(bool).FullName) {
                    dict[i.Key] = r.Next() % 2 == 0;

                } else {
                    dict[i.Key] = Randomize((IDictionary<string, object>)i.Value);
                }
            }

            return dict;
        }
    }
}
