using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using API.Walker.Models;

namespace API.Walker.Crawler {
    public static class VerbLoader {
        public static Consts.Verbs Get(MethodInfo method) {
            var types = method.CustomAttributes.Select(x => x.AttributeType.FullName);

            if (types.Contains("System.Web.Mvc.HttpPostAttribute"))
                return Consts.Verbs.POST;

            if (types.Contains("System.Web.Mvc.HttpPutAttribute"))
                return Consts.Verbs.PUT;

            if (types.Contains("System.Web.Mvc.HttpDeleteAttribute"))
                return Consts.Verbs.DELETE;

            if (types.Contains("System.Web.Mvc.HttpPatchAttribute"))
                return Consts.Verbs.PATCH;

            return Consts.Verbs.GET;
        }
    }
}