using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Models
{
    [Table("CommentsLikesDislikes")]
    public class CommentLikeDislikeModel
    {
        [Key]
        [Column(Order = 0)]
        public int CommentId { get; set; }
        public ProductCommentModel Comment { get; set; }
        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public bool? LikeDislike { get; set; }
    }
}
