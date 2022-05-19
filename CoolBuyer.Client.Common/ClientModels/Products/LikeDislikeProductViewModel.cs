using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Products
{
    public class LikeDislikeProductViewModel
    {
        public int ProductId { get; set; }
        public bool LikeDislike { get; set; }
    }
}
