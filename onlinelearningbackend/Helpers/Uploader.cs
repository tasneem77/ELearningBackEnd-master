
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Helpers
{
    public class Uploader
    {
        private IWebHostEnvironment hostingEnvironment;
        public Uploader(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        public string GetPathAndFilename(string filename)
        {
            var UniqueFileName = new Guid();
            string path = this.hostingEnvironment.WebRootPath + @"\uploads\";
            //string path =  @"\uploads\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path +UniqueFileName+ filename;
           // return   filename;
            //~/root/upload/filename.ext
        }
    }
}
