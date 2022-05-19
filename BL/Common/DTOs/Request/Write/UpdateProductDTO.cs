using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Write
{
    public class UpdateProductDTO
    {
        public UpdateProductDTO()
        {
            this.Images = new List<CreateProductImageDTO>();
        }

        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public List<CreateProductImageDTO> Images { get; set; }
    }
}
