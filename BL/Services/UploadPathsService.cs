using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public class UploadPathsService : IUserUploadsPathsService
    {
        private readonly string productImagesPath;
        private readonly string profileImagePath;
        private readonly string rootPath;


        public UploadPathsService(string rootPath, string productImagesPath, string profileImagePath)
        {
            this.rootPath = rootPath;
            this.productImagesPath = productImagesPath;
            this.profileImagePath = profileImagePath;
        }


        public string GetProductImagesStaticPathPart(bool includeRootPath = false)
        {
            if (includeRootPath)
            {
                return Path.Combine(rootPath, productImagesPath);
            }
            else
            {
                return productImagesPath;
            }
        }

        public string GetProfileImagesStaticPathPart(bool includeRootPath = false)
        {
            if (includeRootPath)
            {
                return Path.Combine(rootPath, profileImagePath);
            }
            else
            {
                return profileImagePath;
            }
        }
    }
}
