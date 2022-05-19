using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Products
{
    public class NewProductViewModel
    {
        public NewProductViewModel()
        {
            this.Images = new List<CreateProductImageViewModel>();
        }


        [Required(ErrorMessage = "Title is required.")]
        [StringLength(maximumLength: 150, ErrorMessage = "Invalid title length.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(maximumLength: 8000, ErrorMessage = "Invalid description length.", MinimumLength = 1)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        //[DataType(DataType.Currency)]
        //[MinLength(0, ErrorMessage = "Price must not be negative.")]
        //[DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Price field does not match the criteria for a decimal number.")]
        [Range(minimum: 0.1, maximum: Double.MaxValue, ErrorMessage = "Invalid price value.")]
        //[RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Price field must be a decimal number, with '.' as a delimiter.")]
        //[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "Invalid category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Subcategory is required.")]
        [Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "Invalid subcategory.")]
        public int SubcategoryId { get; set; }
        public int? SectionId { get; set; }

        [JsonIgnore]
        public List<CreateProductImageViewModel> Images { get; set; }
    }
}
