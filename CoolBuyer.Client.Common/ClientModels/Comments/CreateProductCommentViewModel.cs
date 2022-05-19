using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Comments
{
    public class CreateProductCommentViewModel
    {
        public int ProductId { get; set; }
        public int Text { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
