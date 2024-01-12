using L2DataParser.Helpers;
using L2DataParser.L2Models;
using L2DataParser.Models;
using L2DataParser.Parsers;
using Microsoft.EntityFrameworkCore;

namespace L2DataParser
{
    internal class Program
    {
        public static string dataFolder = "C:\\L2Parsing\\Data";
        public static string nameFolder = "C:\\L2Parsing\\Names";
        static void Main(string[] args)
        {

            var dbContext = new L2DataContext();

            dbContext.Database.ExecuteSqlRaw("DELETE FROM [dbo].[ItemNames]");
            dbContext.Database.ExecuteSqlRaw("DELETE FROM [dbo].[ItemDatas]");
            dbContext.SaveChanges();

            IEnumerable<BaseData> BaseItems = Finders.GetBaseDataParsedObject(dataFolder + "\\ItemData.txt", ParserFileType.DataItem);
            MainParser BaseItemDatasParser = new MainParser(typeof(ItemData));
            List<ItemData> itemData = new List<ItemData>();

            foreach (BaseData item in BaseItems)
            {
                if (item.ItemType == "item_begin;item_end")
                {

                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        ItemData itemDataParsed = (ItemData)BaseItemDatasParser.Parse(item);
                        itemData.Add(itemDataParsed);
                        dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ItemDatas] ON");
                        dbContext.ItemDatas.Add(itemDataParsed);
                        dbContext.SaveChanges();
                        dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ItemDatas] OFF");
                        transaction.Commit();
                    }
                }
                else
                if (item.ItemType == "set_begin;set_end")
                {

                }
            }

            IEnumerable<BaseName> BaseItemNames = Finders.GetBaseNameParsedObject(nameFolder + "\\itemname.txt", ParserFileType.DataItem);
            MainParser BaseItemNameParser = new MainParser(typeof(ItemName));
            List<ItemName> itemNames = new List<ItemName>();
            foreach (BaseName item in BaseItemNames)
            {
                if (itemData.Any(a => a.Id == item.Id))
                {

                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        ItemName itemNameParsed = (ItemName)BaseItemNameParser.Parse(item);
                        dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ItemNames] ON");
                        itemNames.Add(itemNameParsed);
                        dbContext.ItemNames.Add(itemNameParsed);
                        dbContext.ItemDatas.SingleOrDefault(s => s.Id == itemNameParsed.Id).ItemNameId = itemNameParsed.Id;
                        dbContext.SaveChanges();
                        dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ItemNames] OFF");
                        transaction.Commit();
                    }
                }
                else
                {
                    Console.WriteLine(item.NameId + " not found in item datas.");
                }
            }

            /*
            IEnumerable<BaseData> BaseNpcs = Finders.GetBaseDataParsedObject(dataFolder + "\\NpcData.txt", ParserFileType.DataItem);
            MainParser BaseNpcDatasParser = new MainParser(typeof(NpcData));
            List<NpcData> npcData = new List<NpcData>();
            foreach (BaseData item in BaseNpcs)
            {
                NpcData npcDataParsed = (NpcData)BaseNpcDatasParser.Parse(item);
                npcData.Add(npcDataParsed);
            }

            IEnumerable<BaseName> BaseNpcNames = Finders.GetBaseNameParsedObject(nameFolder + "\\npcname.txt", ParserFileType.DataItem);
            MainParser BaseNpcNameParser = new MainParser(typeof(NpcName));
            List<NpcName> npcNames = new List<NpcName>();
            foreach (BaseName item in BaseNpcNames)
            {
                if (npcData.Any(a => a.Id == item.Id))
                {
                    NpcName itemDataParsed = (NpcName)BaseNpcNameParser.Parse(item);

                }
                else
                {
                    Console.WriteLine(item.NameId + " not found in npc datas.");
                }
            }*/
        }
    }
}
