using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.Write
{
    public class UpdateProductCommentDTO
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
    }
}
