namespace L2DataParser.Models
{
    public class ParameterObject
    {
        public ParameterObject(string name, object value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
