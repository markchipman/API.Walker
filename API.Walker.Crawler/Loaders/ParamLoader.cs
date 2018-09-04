using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace API.Walker.Crawler.Loaders {
    public static class ParamLoader {
        public static List<ParameterInfo> Get(MethodInfo method) => method.GetParameters().ToList();
    }
}
