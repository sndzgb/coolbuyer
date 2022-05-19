using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.Write
{
    public class ProductCommentReactionDTO
    {
        public int ProductId { get; set; }
        public int CommentId { get; set; }
        public bool LikeDislike { get; set; }
        //public int UserId { get; set; }
    }
}
