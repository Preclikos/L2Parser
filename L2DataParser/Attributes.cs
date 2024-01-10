namespace L2DataParser
{
    public class InputDataField : Attribute
    {
        private string name;
        private bool subParser;
        private string parserName;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public bool SubParser
        {
            get { return this.subParser; }
            set { this.subParser = value; }
        }
        public string ParserName
        {
            get { return this.parserName; }
            set { this.parserName = value; }
        }
    }
}
