using L2DataParser.Models;
using L2DataParser.Parsers;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace L2DataParser.Helpers
{
    public static class Finders
    {
        public static IEnumerable<BaseData> GetBaseDataParsedObject(string FileName, ParserFileType parserFileType)
        {
            StreamReader npcNameFile = new StreamReader(FileName, Encoding.Unicode, true);

            List<BaseData> BaseItemCollection = new List<BaseData>();
            string lineOfFile;

            while ((lineOfFile = npcNameFile.ReadLine()) != null)
            {
                string ResultReplace = Replacers.TrimAllWithInplaceCharArray(lineOfFile, '&');
                string[] lineSplit = ResultReplace.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                string[] startBegin = lineSplit[0].Split('_');
                string[] endEnd = lineSplit[lineSplit.Length - 1].Split('_');
                while (startBegin.Length != endEnd.Length && startBegin[0] != endEnd[0] && endEnd[1] != "end")
                {
                    lineOfFile += npcNameFile.ReadLine();
                    endEnd = lineSplit[lineSplit.Length - 1].Split('_');
                }

                string DataType = lineSplit[0] + ";" + lineSplit[lineSplit.Length - 1];

                int? Id = null;
                string NameId = null;
                string ItemType = null;

                string[] ParamsWithEq = lineSplit.Where(s => s.Contains("=")).ToArray();

                if (parserFileType == ParserFileType.DataItem)
                {
                    for (int i = 1; i < lineSplit.Length - 1; i++)
                    {
                        if (lineSplit[i] != "" && !lineSplit[i].Contains('='))
                        {
                            if (int.TryParse(lineSplit[i], out int n))
                            {
                                Id = n;
                            }
                            else if (NameId == null && lineSplit[i][0] == '[' && lineSplit[i][lineSplit[i].Length - 1] == ']')
                            {
                                NameId = Replacers.ClearDataName(lineSplit[i]);// lineSplit[i].Substring(1, lineSplit[i].Length - 2);
                            }
                            else if (ItemType == null)
                            {
                                ItemType = lineSplit[i];
                            }
                        }
                    }
                }
                else if (parserFileType == ParserFileType.GroupItem)
                {
                    List<String> ParamsWithEqList = ParamsWithEq.ToList();
                    string IdFullLine = ParamsWithEqList.Where(w => w.Split('=')[0] == "object_id").FirstOrDefault();
                    Id = Convert.ToInt32(IdFullLine.Split('=')[1]);
                    ParamsWithEqList.Remove(IdFullLine);

                    string NameFullLine = ParamsWithEqList.Where(w => w.Split('=')[0] == "object_name").FirstOrDefault();
                    NameId = Replacers.ClearDataName(NameFullLine.Split('=')[1]);
                    ParamsWithEqList.Remove(NameFullLine);

                    ParamsWithEq = ParamsWithEqList.ToArray();
                }
                else if (parserFileType == ParserFileType.Territory)
                {
                    string test = lineSplit[1].Substring(1, lineSplit[1].Length - 2);
                    /*List<String> ParamsWithEqList = ParamsWithEq.ToList();
                    string IdFullLine = ParamsWithEqList.Where(w => w.Split('=')[0] == "object_id").FirstOrDefault();
                    Id = Convert.ToInt32(IdFullLine.Split('=')[1]);
                    ParamsWithEqList.Remove(IdFullLine);

                    string NameFullLine = ParamsWithEqList.Where(w => w.Split('=')[0] == "object_name").FirstOrDefault();
                    NameId = Replacers.ClearName(NameFullLine.Split('=')[1]);
                    ParamsWithEqList.Remove(NameFullLine);

                    ParamsWithEq = ParamsWithEqList.ToArray();*/
                }

                if (Id == null)
                {
                    //try to find id in attributes
                    if (lineSplit.Where(w => w.ToLower().Contains("id")).Count() > 0)
                    {
                        string IdString = lineSplit.Where(w => w.ToLower().Contains("id")).FirstOrDefault();
                        if (int.TryParse(IdString.Split('=')[1], out int n))
                        {
                            Id = n;
                        }
                    }
                    if (lineSplit.Where(w => w.ToLower().Contains("name")).Count() > 0)
                    {
                        foreach (string IdString in lineSplit.Where(w => w.ToLower().Contains("name")))
                        {
                            if (NameId == null)
                            {
                                string[] NameArr = IdString.Split('=');
                                if (NameArr.Length > 1)
                                {
                                    string Name = NameArr[1];
                                    if (Name[0] == '[' && Name[Name.Length - 1] == ']')
                                    {
                                        NameId = Name.Substring(1, Name.Length - 2);
                                    }
                                    else
                                    {
                                        NameId = Name;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (Id == null)
                    {
                        return null;
                    }
                }
                BaseItemCollection.Add(new BaseData(DataType, (int)Id, NameId, ItemType, ParamsWithEq));
            }
            return BaseItemCollection;
        }

        public static IEnumerable<BaseName> GetBaseNameParsedObject(string FileName, ParserFileType parserFileType)
        {
            StreamReader npcNameFile = new StreamReader(FileName, Encoding.Unicode, true);

            List<BaseName> BaseItemCollection = new List<BaseName>();
            string lineOfFile;
            var header = new String[0];

            while ((lineOfFile = npcNameFile.ReadLine()) != null)
            {

                var delimiters = new char[] { '\t', };
                var segments = lineOfFile.Split(delimiters, StringSplitOptions.None);

                if (header.Length == 0)
                {
                    header = segments;
                    continue;
                }

                /*
                string ResultReplace = Replacers.TrimAllWithInplaceCharArray(lineOfFile, '&');
                string[] lineSplit = ResultReplace.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                string[] startBegin = lineSplit[0].Split('_');
                string[] endEnd = lineSplit[lineSplit.Length - 1].Split('_');
                while (startBegin.Length != endEnd.Length && startBegin[0] != endEnd[0] && endEnd[1] != "end")
                {
                    lineOfFile += npcNameFile.ReadLine();
                    endEnd = lineSplit[lineSplit.Length - 1].Split('_');
                }

                string DataType = lineSplit[0] + ";" + lineSplit[lineSplit.Length - 1];
                */

                int? Id = null;
                string NameId = null;

                List<String> ParamsWithEqList = new List<string>();

                for (int i = 0; segments.Length > i; i++)
                {
                    var currentValue = segments[i];
                    var currentColumn = header[i];
                    if (currentColumn == "id")
                    {
                        Id = Convert.ToInt32(currentValue);
                    }
                    else if (currentColumn == "name")
                    {
                        NameId = currentValue;
                    }
                    else
                    {
                        ParamsWithEqList.Add(currentColumn + "=" + currentValue);
                    }

                    
                }
                BaseItemCollection.Add(new BaseName((int)Id, NameId, ParamsWithEqList.ToArray()));
                /*
            string[] ParamsWithEq = lineSplit.Where(s => s.Contains("=")).ToArray();

            if (parserFileType == ParserFileType.DataItem)
            {
                for (int i = 1; i < lineSplit.Length - 1; i++)
                {
                    if (lineSplit[i] != "" && !lineSplit[i].Contains('='))
                    {
                        if (int.TryParse(lineSplit[i], out int n))
                        {
                            Id = n;
                        }
                        else if (NameId == null && lineSplit[i][0] == '[' && lineSplit[i][lineSplit[i].Length - 1] == ']')
                        {
                            NameId = Replacers.ClearName(lineSplit[i]);// lineSplit[i].Substring(1, lineSplit[i].Length - 2);
                        }
                        else if (ItemType == null)
                        {
                            ItemType = lineSplit[i];
                        }
                    }
                }
            }
            else if (parserFileType == ParserFileType.GroupItem)
            {
                List<String> ParamsWithEqList = ParamsWithEq.ToList();
                string IdFullLine = ParamsWithEqList.Where(w => w.Split('=')[0] == "object_id").FirstOrDefault();
                Id = Convert.ToInt32(IdFullLine.Split('=')[1]);
                ParamsWithEqList.Remove(IdFullLine);

                string NameFullLine = ParamsWithEqList.Where(w => w.Split('=')[0] == "object_name").FirstOrDefault();
                NameId = Replacers.ClearName(NameFullLine.Split('=')[1]);
                ParamsWithEqList.Remove(NameFullLine);

                ParamsWithEq = ParamsWithEqList.ToArray();
            }
            else if (parserFileType == ParserFileType.Territory)
            {
                string test = lineSplit[1].Substring(1, lineSplit[1].Length - 2);
                /*List<String> ParamsWithEqList = ParamsWithEq.ToList();
                string IdFullLine = ParamsWithEqList.Where(w => w.Split('=')[0] == "object_id").FirstOrDefault();
                Id = Convert.ToInt32(IdFullLine.Split('=')[1]);
                ParamsWithEqList.Remove(IdFullLine);

                string NameFullLine = ParamsWithEqList.Where(w => w.Split('=')[0] == "object_name").FirstOrDefault();
                NameId = Replacers.ClearName(NameFullLine.Split('=')[1]);
                ParamsWithEqList.Remove(NameFullLine);

                ParamsWithEq = ParamsWithEqList.ToArray();*/
            }
            /*
                if (Id == null)
                {
                    //try to find id in attributes
                    if (lineSplit.Where(w => w.ToLower().Contains("id")).Count() > 0)
                    {
                        string IdString = lineSplit.Where(w => w.ToLower().Contains("id")).FirstOrDefault();
                        if (int.TryParse(IdString.Split('=')[1], out int n))
                        {
                            Id = n;
                        }
                    }
                    if (lineSplit.Where(w => w.ToLower().Contains("name")).Count() > 0)
                    {
                        foreach (string IdString in lineSplit.Where(w => w.ToLower().Contains("name")))
                        {
                            if (NameId == null)
                            {
                                string[] NameArr = IdString.Split('=');
                                if (NameArr.Length > 1)
                                {
                                    string Name = NameArr[1];
                                    if (Name[0] == '[' && Name[Name.Length - 1] == ']')
                                    {
                                        NameId = Name.Substring(1, Name.Length - 2);
                                    }
                                    else
                                    {
                                        NameId = Name;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (Id == null)
                    {
                        return null;
                    }
                }*/


            return BaseItemCollection;
        }
    }
}
