using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Comments
{
    public class ProductCommentViewModel
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public DateTime PostedDate { get; set; }
        public string CommentText { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public int? ParentCommentId { get; set; }
        public int ProductId { get; set; }
        public int? TotalReplies { get; set; }
    }
}
