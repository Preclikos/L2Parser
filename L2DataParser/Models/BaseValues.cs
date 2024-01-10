namespace L2DataParser.Models
{
    public class BaseValues
    {
        public BaseValues(string nameId, string[] items)
        {
            this.NameId = nameId;
            this.Items = items;
        }

        public string NameId { get; set; }
        public string[] Items { get; set; }
    }
}
