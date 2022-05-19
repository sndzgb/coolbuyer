using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Models
{
    [Table("AccountTypes")]
    public class AccountType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
