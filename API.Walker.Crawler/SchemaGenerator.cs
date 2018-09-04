using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using API.Walker.Crawler.Loaders;
using API.Walker.Models;

namespace API.Walker.Crawler {
    public class SchemaGenerator {
        private List<Assembly> _assemblies;

        public SchemaGenerator(List<string> dlls) {
            _assemblies = AssemblyLoader.Load(dlls);
        }

        ~SchemaGenerator() {
            _assemblies = null;
        }

        public Schema Generate(string url = "", Dictionary<string, string> headers = null) {
            var schema = new Schema { Headers = headers, Url = url};

            var controllers = TypeLoader.Get(_assemblies, x => x.FullName.EndsWith("Controller"));

            foreach (var ctrl in controllers) {
                var controllerUrl = $"{url}/{ctrl.Name.Replace("Controller", "")}";
                var schCtrl = new Controller { Url = controllerUrl };

                var methods = MethodLoader.Get(ctrl, x => !x.IsSpecialName && x.DeclaringType?.FullName == ctrl.FullName);

                foreach (var method in methods) {
                    var methodUrl = $"{controllerUrl}/{method.Name}";
                    var endp = new Endpoint { Url = methodUrl, Verb = VerbLoader.Get(method) };

                    foreach (var p in ParamLoader.Get(method)) {
                        endp.Input.Add(p.Name, p.ParameterType.IsPrimitive || p.ParameterType == typeof(string) ? p.ParameterType.FullName : GetBody(p));
                    }

                    schCtrl.Endpoints.Add(endp);
                }

                schema.Controllers.Add(schCtrl);
            }

            return schema;
        }

        private dynamic GetBody(ParameterInfo p) {
            dynamic expando = new System.Dynamic.ExpandoObject();
            if (p.ParameterType.IsPrimitive || p.ParameterType == typeof(string)) {
                ((IDictionary<string, object>)expando)[p.Name] = p.ParameterType.FullName;
                return expando;
            }

            try {
                var type = TypeLoader.Find(p.ParameterType.FullName, _assemblies);
                var props = PropLoader.Get(type);

                foreach (var pr in props) {
                    if (pr.PropertyType.IsPrimitive || pr.PropertyType == typeof(string)) {
                        ((IDictionary<string, object>)expando)[pr.Name] = pr.PropertyType.FullName;
                    } else ((IDictionary<string, object>)expando)[pr.Name] = GetBody(pr);
                }

                return expando;
            } catch (Exception e) {
                var abc = e;
            }

            throw new Exception("Something has gone wrong!");
        }

        private dynamic GetBody(PropertyInfo pr) {
            dynamic expando = new System.Dynamic.ExpandoObject();
            if (pr.PropertyType.IsPrimitive || pr.PropertyType == typeof(string)) {
                ((IDictionary<string, object>)expando)[pr.Name] = pr.PropertyType.FullName;
                return expando;
            }

            var type = TypeLoader.Find(pr.PropertyType.FullName, _assemblies);
            var props = PropLoader.Get(type);

            foreach (var pr2 in props) {
                if (pr2.PropertyType.IsPrimitive || pr2.PropertyType == typeof(string)) {
                    ((IDictionary<string, object>)expando)[pr2.Name] = pr2.PropertyType.FullName;
                } else ((IDictionary<string, object>)expando)[pr2.Name] = GetBody(pr2);

            }

            return expando;
        }
    }
}
