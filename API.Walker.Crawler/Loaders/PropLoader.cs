using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace API.Walker.Crawler.Loaders {
    public static class PropLoader {
        public static List<PropertyInfo> Get(Type type) => type.GetProperties().ToList();
    }
}
