using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Core.DTO;
using System.Collections.Generic;

namespace Shop.Web.Models
{
    public class AddOrUpdateProductViewModel : ProductViewModel
    {
        public List<SelectListItem> Categories { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Electronics", Value = "Electronics"},
            new SelectListItem { Text = "Tools", Value = "Tools"},
            new SelectListItem { Text = "Trousers", Value = "Trousers"}
        };

        public AddOrUpdateProductViewModel()
        {
        }

        public AddOrUpdateProductViewModel(ProductDto dto) : base(dto)
        {
        }
    }
}
