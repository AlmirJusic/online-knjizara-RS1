using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_knjizara.Helpers
{
    public static class SlikaBase64
    {
        public static string Prikaz(byte[] slika)
        {

            return slika != null ? $"data:image/jpg;base64,{Convert.ToBase64String(slika)}" : null;
        }
    }
}
