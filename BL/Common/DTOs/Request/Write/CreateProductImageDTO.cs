using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Write
{
    public class CreateProductImageDTO
    {
        public string Name { get; set; }
        public Stream Stream { get; set; }
    }
}
