using L2DataParser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2DataParser.SubParsers
{
    public static partial class SubParser
    {
        public static EtcItemType EtcItemTypeParser(int Id, string Input)
        {
            switch (Input)
            {
                case "recipe":
                    return EtcItemType.Recipe;
                case "material":
                    return EtcItemType.Material;
                case "potion":
                    return EtcItemType.Potion;
                default:
                    return EtcItemType.None;
            }
        }
    }
}
