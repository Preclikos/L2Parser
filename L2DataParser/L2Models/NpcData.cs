﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L2DataParser.L2Models
{
    public class NpcData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string NameId { get; set; }

        public int? NpcNameId { get; set; }
        public NpcName NpcName { get; set; }
    /*
        public int? RecipeDataId { get; set; }
        public RecipeData RecipeData { get; set; }

        [InputDataField(Name = "item_type", SubParser = true, ParserName = "ItemTypeParser")]
        public ItemType ItemType { get; set; }
        /*
        [InputDataField(Name = "etcitem_type", SubParser = true, ParserName = "EtcItemTypeParser")]
        public EtcItemType EtcItemType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [InputDataField(Name = "html")]
        public string Html { get; set; }*/
    }
}
