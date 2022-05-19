using CoolBuyer.Client.Common.ClientModels;
using CoolBuyer.Client.Common.ClientModels.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolBuyer.Client.Web.MVC.ObjectModelBinders
{
    /// <summary>
    /// Binds the incoming model using custom 'CreateProductModelBinder'
    /// </summary>
    public class CreateProductModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(NewProductViewModel))
            {
                HttpRequestBase request = controllerContext.HttpContext.Request;

                var model = new NewProductViewModel();
                
                decimal p;
                if (decimal.TryParse(request.Form.Get("Price"), out p))
                {
                    model.Price = p;
                }

                string title = request.Form.Get("Title");
                int categoryId;
                Int32.TryParse(request.Form.Get("CategoryId"), out categoryId);
                int subcategoryId;
                Int32.TryParse(request.Form.Get("SubcategoryId"), out subcategoryId);
                int sectionId;
                Int32.TryParse(request.Form.Get("SectionId"), out sectionId);
                string description = request.Unvalidated.Form["Description"];
                
                model.Title = title;
                model.Description = description;
                model.CategoryId = categoryId;
                model.SubcategoryId = subcategoryId;
                model.SectionId = sectionId;

                HttpFileCollectionBase files = request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].InputStream.Length != 0)
                    {
                        HttpPostedFileBase file = files[i];
                        model.Images.Add(new CreateProductImageViewModel()
                        {
                            Name = file.FileName,
                            Stream = file.InputStream
                        });
                    }
                }
                
                return model;
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }
    }
}