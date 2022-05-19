using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Models
{
    [Table("Subcategories")]
    public class SubcategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryModel Category { get; set; }
        public int CategoryId { get; set; }
    }
}
