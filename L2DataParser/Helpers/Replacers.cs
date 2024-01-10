using L2DataParser.Models;

namespace L2DataParser.Helpers
{
    public static class Replacers
    {
        public static string TrimAllWithInplaceCharArray(string str)
        {

            var len = str.Length;
            var src = str.ToCharArray();
            int dstIdx = 0;

            for (int i = 0; i < len; i++)
            {
                var ch = src[i];

                switch (ch)
                {

                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':

                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':

                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u202F':

                    case '\u205F':
                    case '\u3000':
                    case '\u2028':
                    case '\u2029':
                    case '\u0009':

                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                        continue;

                    default:
                        src[dstIdx++] = ch;
                        break;
                }
            }
            return new string(src, 0, dstIdx);
        }
        public static string TrimAllWithInplaceCharArray(string Input, char Replace)
        {
            bool CancelationTrigger = false;
            var len = Input.Length;
            var src = Input.ToCharArray();
            int dstIdx = 0;

            for (int i = 0; i < len; i++)
            {
                var ch = src[i];
                if (ch == '[')
                {
                    CancelationTrigger = true;
                }
                if (CancelationTrigger)
                {
                    if (ch == ']')
                    {
                        CancelationTrigger = false;

                    }
                    src[dstIdx++] = ch;
                }
                else
                    switch (ch)
                    {

                        case '\u0020':
                        case '\u00A0':
                        case '\u1680':
                        case '\u2000':
                        case '\u2001':

                        case '\u2002':
                        case '\u2003':
                        case '\u2004':
                        case '\u2005':
                        case '\u2006':

                        case '\u2007':
                        case '\u2008':
                        case '\u2009':
                        case '\u200A':
                        case '\u202F':

                        case '\u205F':
                        case '\u3000':
                        case '\u2028':
                        case '\u2029':
                        case '\u0009':

                        case '\u000A':
                        case '\u000B':
                        case '\u000C':
                        case '\u000D':
                        case '\u0085':
                            src[dstIdx++] = Replace;
                            continue;

                        default:
                            src[dstIdx++] = ch;
                            break;
                    }
            }
            return new string(src, 0, dstIdx);
        }
        public static IEnumerable<BaseParameterData> ReplaceBaseWithdObject(IEnumerable<BaseData> baseItems)
        {
            List<BaseParameterData> baseParameterItems = new List<BaseParameterData>();
            foreach (BaseData baseItem in baseItems)
            {
                baseParameterItems.Add(new BaseParameterData(baseItem));
            }
            return baseParameterItems;
        }

        public static string ClearDataName(string Name)
        {
            if (Name[0] == '[' && Name[(Name.Length - 1)] == ']')
            {
                return Name.Substring(1, Name.Length - 2);
            }
            else
            {
                return Name;
            }
        }

        public static string ClearName(string Name)
        {
            if(Name.StartsWith("a,"))
            {
                Name = Name.Substring(2);
            }

            if(Name.EndsWith("\\0"))
            {
                Name = Name.Substring(0, Name.Length - 2);
            }

            return Name;
        }
    }
}
