namespace L2DataParser.Models
{
    public class BaseParameterName
    {
        public BaseParameterName(BaseName baseName)
        {
            Id = baseName.Id;
            NameId = baseName.NameId;
            Items = ReplaceItemWithObject(baseName.Items);
        }

        public string ItemType { get; set; }
        public int Id { get; set; }
        public string NameId { get; set; }
        public string EnumType { get; set; }
        public IEnumerable<ParameterObject> Items { get; set; }

        public IEnumerable<ParameterObject> ReplaceItemWithObject(string[] items)
        {
            List<ParameterObject> ObjectItems = new List<ParameterObject>();
            for (int i = 0; i < items.Length; i++)
            {
                char[] ItemChars = items[i].ToArray();
                for (int c = 0; c < ItemChars.Length; c++)
                {
                    if (ItemChars[c] == '=')
                    {
                        ObjectItems.Add(
                            new ParameterObject(
                                items[i].Substring(0, c),
                                items[i].Substring(c + 1, items[i].Length - 1 - c)
                                )
                            );
                    }
                }
            }
            return ObjectItems;
        }
    }
}
