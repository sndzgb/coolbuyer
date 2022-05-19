using CoolBuyer.Server.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.WebApiModels.Files
{
    public class ImageCreateWebApiModel
    {
        [FileLength(1024 * 1024 * 5, ErrorMessage = "Image length must not exceed 5MB.")]
        public Stream Stream { get; set; }

        [FileExtensions(Extensions = ".jpg,.png,.jpeg", ErrorMessage = "Invalid image extension.")]
        public string Name { get; set; }
    }
}