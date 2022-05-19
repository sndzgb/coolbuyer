using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs
{
    public class UserCommentDTO
    {
        public DateTime PostedDate { get; set; }
        public string CommentText { get; set; }
    }
}
