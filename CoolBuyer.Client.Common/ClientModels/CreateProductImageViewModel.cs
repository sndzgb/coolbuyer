using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels
{
    public class CreateProductImageViewModel
    {
        [StringLength(maximumLength: 100, ErrorMessage = "Invalid image name length.", MinimumLength = 1)]
        public string Name { get; set; }

        public Stream Stream { get; set; }
    }
}
