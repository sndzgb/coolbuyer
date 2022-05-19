using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Comments
{
    public class UpdateProductCommentViewModel
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
    }
}
