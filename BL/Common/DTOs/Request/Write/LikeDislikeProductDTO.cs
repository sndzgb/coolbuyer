using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Write
{
    public class LikeDislikeProductDTO
    {
        public int ProductId { get; set; }
        public bool LikeDislike { get; set; }
    }
}
