using System.Collections.Generic;
using System.Reflection;

namespace API.Walker.Crawler.Loaders {
    public static class AssemblyLoader {
        public static List<Assembly> Load(List<string> paths) {
            var result = new List<Assembly>();

            foreach (var path in paths)
                result.Add(Assembly.LoadFrom(path));

            return result;
        }

        public static Assembly Load(string path) => Assembly.LoadFrom(path);
    }
}
