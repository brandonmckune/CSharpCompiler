using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolingPackage
{
    public static class Extensions
    {
        public enum AccessModifier { Public, Protected, Private }

        public static string ToLower(this AccessModifier accessModifier)
        {
            return accessModifier.ToString().ToLower();
        }
        
    }
}
