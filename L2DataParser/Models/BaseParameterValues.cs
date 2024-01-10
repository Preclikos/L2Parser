namespace L2DataParser.Models
{
    public class BaseParameterValues
    {
        public BaseParameterValues(BaseValues baseItem)
        {
            this.Items = ReplaceItemWithObject(baseItem.Items);
        }

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
