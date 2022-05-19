//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CoolBuyer.Server.BusinessLogic.Common.Attributes
//{
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
//    public sealed class ValidateHTMLAttribute : ValidationAttribute
//    {
//        public ICollection<string> AllowedTags { get; private set; }


//        #region ctors

//        public ValidateHTMLAttribute()
//        {
//        }
//        public ValidateHTMLAttribute(string msg, string[] allowedTags)
//            : base(msg)
//        {
//            this.AllowedTags = allowedTags;
//        }
//        public ValidateHTMLAttribute(string[] allowedTags)
//        {
//            this.AllowedTags = allowedTags;
//        }
//        public ValidateHTMLAttribute(string msg)
//            : base(msg)
//        {
//        }

//        #endregion

        
//        public override bool IsValid(object value)
//        {
//            string stringToValidate = value as string;

//            HtmlSanitizer sanitizer = new HtmlSanitizer();

//        }
//    }
//}
