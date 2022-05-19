using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public interface IUserUploadsPathsService
    {
        /// <summary>
        /// Specifies the static path for the product images uploads location.
        /// </summary>
        /// <param name="includeRootPath">If true, the hard drive letter will be incuded at the beginning.</param>
        /// <returns>Upload path as a string.</returns>
        string GetProductImagesStaticPathPart(bool includeRootPath = false);

        /// <summary>
        /// Specifies the static path for the profile images uploads location.
        /// </summary>
        /// <param name="includeRootPath">If true, the hard drive letter will be incuded at the beginning.</param>
        /// <returns>Upload path as a string.</returns>
        string GetProfileImagesStaticPathPart(bool includeRootPath = false);
    }
}
