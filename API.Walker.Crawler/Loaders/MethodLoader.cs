using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace API.Walker.Crawler.Loaders {
    public static class MethodLoader {
        public static List<MethodInfo> Get(List<Type> types, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance) {
            var result = new List<MethodInfo>();

            foreach (var type in types)
                result.AddRange(type.GetMethods(bindingFlags));

            return result;
        }

        public static List<MethodInfo> Get(Type type,
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance) => type.GetMethods(bindingFlags).ToList();

        public static List<MethodInfo> Get(List<Type> types, Func<MethodInfo, bool> condition, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance) {
            var result = new List<MethodInfo>();

            foreach (var type in types)
                result.AddRange(type.GetMethods(bindingFlags).Where(condition));

            return result;
        }

        public static List<MethodInfo> Get(Type type, Func<MethodInfo, bool> condition,
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance) => type.GetMethods(bindingFlags).Where(condition).ToList();
    }
}
