using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class FileLengthAttribute : ValidationAttribute //DataTypeAttribute
    {
        public long MaxFileLength { get; }

        public FileLengthAttribute(long maxFileLength)
        {
            this.MaxFileLength = maxFileLength;
        }

        public override bool IsValid(object value)
        {
            var stream = value as Stream;

            if (stream.Length > MaxFileLength)
            {
                return false;
            }

            return true;
        }
    }
}