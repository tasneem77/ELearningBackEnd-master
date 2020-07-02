using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend
{
    public static class AllowedImageExtensions
    {
        private static string AllowedExtensions = "jpgpng";
        public static bool IsImageExtensionAllowed(string imageExtension)
        {
            return AllowedExtensions.Contains(imageExtension);
        }
    }
}
