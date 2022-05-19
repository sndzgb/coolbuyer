using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Favorites
{
    public class UserFavoritesIndexViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
