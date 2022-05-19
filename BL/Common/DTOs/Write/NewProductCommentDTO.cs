using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.Write
{
    public class NewProductCommentDTO
    {
        public NewProductCommentDTO()
        {
            //this.PostedDateUTC = DateTime.UtcNow;
        }

        public string CommentText { get; set; }
        //public DateTime PostedDateUTC { get; private set; }
        public int? ParentCommentId { get; set; }
        public int ProductId { get; set; }
    }
}
