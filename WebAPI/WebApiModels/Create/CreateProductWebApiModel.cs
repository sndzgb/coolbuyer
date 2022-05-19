using CoolBuyer.Server.WebApi.Attributes;
using CoolBuyer.Server.WebApi.WebApiModels.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.WebApiModels.Create
{
    public class CreateProductWebApiModel
    {
        public CreateProductWebApiModel()
        {
            this.Images = new List<ImageCreateWebApiModel>();
        }


        [Required]
        [JsonProperty("title")]
        [StringLength(50, ErrorMessage = "Title must be between 2 and 50 characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [JsonProperty("description")]
        [StringLength(1000, ErrorMessage = "Description must be between 2 and 1000 characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        [JsonProperty("price")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Invalid price value.")]
        public int Price { get; set; }
                
        public List<ImageCreateWebApiModel> Images { get; set; }
    }
}