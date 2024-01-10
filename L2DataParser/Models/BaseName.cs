namespace L2DataParser.Models
{
    public class BaseName
    {
        public BaseName(int id, string nameId, string[] items)
        {
            this.Id = id;
            this.NameId = nameId;
            this.Items = items;
        }

        public int Id { get; set; }
        public string NameId { get; set; }

        public string[] Items { get; set; }
    }
}
