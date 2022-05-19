using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Logic
{
    public static class FileManager
    {
        public static void Save(string path, Stream stream)
        {
            if (path == "")
            {
                throw new ArgumentNullException("path");
            }

            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            CreateDirectoryStructureForPath(path);

            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var inputStream = stream)
                {
                    inputStream.CopyTo(fs);
                }
            }
        }

        public static void Delete(string path)
        {
            if (path == "")
            {
                throw new ArgumentNullException("path");
            }

            File.Delete(path);
        }

        private static void CreateDirectoryStructureForPath(string filePath)
        {
            var fileName = Path.GetFileName(filePath);

            var dirPath = filePath.TrimEnd(fileName.ToCharArray());
            
            Directory.CreateDirectory(dirPath);
        }
    }
}
