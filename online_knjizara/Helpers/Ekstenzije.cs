using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.Helpers
{
    public static class Ekstenzije
    {
        public static byte[] ToByteArray(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}
