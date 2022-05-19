using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Write
{
    public class NewProductDTO
    {
        public NewProductDTO()
        {
            this.Images = new List<CreateProductImageDTO>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SelectedCurrency { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public int? SectionId { get; set; }

        public List<CreateProductImageDTO> Images { get; set; }
    }
}
