using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class ApiResult
    {
        public static JsonResult Success(object data)
        {
            return new JsonResult(new
            {
                status = true,
                data = data,
                msg = (string) null
            });
        }
        public static JsonResult Fail(string message)
        {
            return new JsonResult(new
            {
                status = false,
                data = (object) null,
                msg = message
            });
        }

    }
}
