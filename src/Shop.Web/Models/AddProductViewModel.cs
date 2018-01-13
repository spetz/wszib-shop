using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models
{
    public class AddProductViewModel : ProductViewModel
    {
        public List<SelectListItem> Categories { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Electronics", Value = "Electronics"},
            new SelectListItem { Text = "Tools", Value = "Tools"},
            new SelectListItem { Text = "Trousers", Value = "Trousers"}
        };
    }
}
