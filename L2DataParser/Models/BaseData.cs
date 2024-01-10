namespace L2DataParser.Models
{
    public class BaseData
    {
        public BaseData(string itemType, int id, string nameId, string enumType, string[] items)
        {
            this.ItemType = itemType;
            this.Id = id;
            this.NameId = nameId;
            this.EnumType = enumType;
            this.Items = items;
        }
        public string ItemType { get; set; }
        public int Id { get; set; }
        public string NameId { get; set; }
        public string EnumType { get; set; }
        public string[] Items { get; set; }
    }
}
