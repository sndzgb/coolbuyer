using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.Read
{
    public class FilterProductCommentsDTO
    {
        //int productId, int take, int page, int? commentId
        public int ProductId { get; set; }
        public int Take { get; set; } = 5;
        public int Page { get; set; } = 1;
        public int? CommentId { get; set; }
    }
}
