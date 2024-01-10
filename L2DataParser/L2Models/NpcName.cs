using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L2DataParser.L2Models
{
    public class NpcName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string NameId { get; set; }

        [InputDataField(Name = "description")]
        public string Description { get; set; }
    }
}
