using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace API.Walker.Crawler.Loaders {
    public static class TypeLoader {
        public static List<Type> Get(List<Assembly> assemblies) {
            var result = new List<Type>();

            foreach (var assembly in assemblies)
                result.AddRange(assembly.GetTypes());

            return result;
        }

        public static List<Type> Get(Assembly assembly) => assembly.GetTypes().ToList();

        public static List<Type> Get(Assembly assembly, Func<Type, bool> condition) =>
            assembly.GetTypes().Where(condition).ToList();

        public static List<Type> Get(List<Assembly> assemblies, Func<Type, bool> condition) {
            var result = new List<Type>();

            foreach (var assembly in assemblies)
                result.AddRange(assembly.GetTypes().Where(condition));

            return result;
        }

        public static Type Find(string fullName, List<Assembly> assemblies) {
            foreach (var assembly in assemblies) {
                var types = assembly.GetTypes();

                foreach (var type in types) {
                    if (type.FullName == fullName)
                        return type;
                }
            }

            return null;
        }
    }
}
