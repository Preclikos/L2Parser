using L2DataParser.Enums;

namespace L2DataParser.SubParsers
{
    public static partial class SubParser
    {
        public static ItemType ItemTypeParser(int Id, string Input)
        {
            switch (Input)
            {
                case "etcitem":
                    return ItemType.EtcItem;
                case "asset":
                    return ItemType.Asset;
                case "weapon":
                    return ItemType.Weapon;
                case "armor":
                    return ItemType.Armor;
                case "accessary":
                    return ItemType.Accessary;
                case "questitem":
                    return ItemType.QuestItem;
                default:
                    return ItemType.Unknown;
            }
        }
    }
}
