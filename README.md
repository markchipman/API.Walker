Creates JSON structured data from dll's

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers {
    public class HomeController : Controller {
        public ActionResult Index(LoginViewModel modelLogin) {
            return Json(modelLogin, JsonRequestBehavior.AllowGet);
        }

        public ActionResult A(string a) {
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult B(LoginViewModel b) {
            return Json(b, JsonRequestBehavior.AllowGet);
        }
    }
}
```

to

```json
{
   "Url":"http://localhost:61588",
   "Headers":null,
   "Controllers":[
      {
         "Url":"http://localhost:61588/Home",
         "Endpoints":[
            {
               "Url":"http://localhost:61588/Home/Index",
               "Verb":0,
               "Input":{
                  "modelLogin":{
                     "Email":"System.String",
                     "Password":"System.String",
                     "RememberMe":"System.Boolean",
                     "registerModel":{
                        "Email":"System.String",
                        "Password":"System.String",
                        "ConfirmPassword":"System.String"
                     }
                  }
               },
               "Output":{

               }
            },
            {
               "Url":"http://localhost:61588/Home/A",
               "Verb":0,
               "Input":{
                  "a":"System.String"
               },
               "Output":{

               }
            },
            {
               "Url":"http://localhost:61588/Home/B",
               "Verb":1,
               "Input":{
                  "b":{
                     "Email":"System.String",
                     "Password":"System.String",
                     "RememberMe":"System.Boolean",
                     "registerModel":{
                        "Email":"System.String",
                        "Password":"System.String",
                        "ConfirmPassword":"System.String"
                     }
                  }
               },
               "Output":{

               }
            }
         ]
      }
   ],
   "Tests":[

   ]
}
```

with possibility to add automatic tests.