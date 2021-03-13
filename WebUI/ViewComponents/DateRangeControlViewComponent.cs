using Application.Enumeration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewComponents
{

    [ViewComponent]
    public class DateRangeControlViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(DateRangeControlArgs args)
        {
            return View(args);
        }
    }

    public class DateRangeControlArgs
    {
        public string IdPrefix { get; set; }
        public string LabelName { get; set; }

        public string RadioName { get; set; }

        public string DateRangeName { get; set; }

        public string CustomDateStartName { get; set; }

        public string CustomDateEndName { get; set; }

        public DateSearchOptions RangeOptions { get; set; }

        public DateSearchOptions SelectedOption { get; set; }
    }

    [Flags]
    public enum DateSearchOptions
    {
        全部 = 0,
        今日 = 1,
        近7日 = 2,
        近30日 = 4,
        近半年 = 8,
        自訂 = 1024
    }
}
