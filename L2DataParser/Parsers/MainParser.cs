using System.Reflection;
using L2DataParser.Helpers;
using L2DataParser.Models;
using L2DataParser.SubParsers;

namespace L2DataParser.Parsers
{
    public class MainParser
    {
        private Type classModel { get; set; }
        private Object classObject { get; set; }
        ConstructorInfo constructorIWithoutAttrs { get; set; }
        public MainParser(Type outClassModel)
        {
            try
            {
                classModel = outClassModel;
                constructorIWithoutAttrs = outClassModel.GetConstructors()[0];

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot invoke Constructor");
            }
        }

        public object Parse(BaseData item)
        {
            classObject = constructorIWithoutAttrs.Invoke(null);

            BaseParameterData baseParameterItem = new BaseParameterData(item);
            MemberInfo[] memberInfos = classModel.GetMembers();
            foreach (MemberInfo member in memberInfos)
            {
                PropertyInfo memberInfo = null;

                if (member.Name == "Id")
                {
                    memberInfo = ((PropertyInfo)member);
                    memberInfo.SetValue(classObject, item.Id, null);
                }
                if (member.Name == "NameId")
                {
                    memberInfo = ((PropertyInfo)member);
                    memberInfo.SetValue(classObject, item.NameId, null);
                }

                IEnumerable<CustomAttributeData> parserMainAttributes = member.GetCustomAttributesData().Where(w => w.AttributeType.Name == "InputDataField");
                if (parserMainAttributes.Count() > 0)
                {
                    CustomAttributeData InputDataAttr = parserMainAttributes.FirstOrDefault();
                    string NameValue = Convert.ToString(InputDataAttr.NamedArguments.Where(w => w.MemberName == "Name").FirstOrDefault().TypedValue.Value);
                    bool HasParser = Convert.ToBoolean(InputDataAttr.NamedArguments.Where(w => w.MemberName == "SubParser").FirstOrDefault().TypedValue.Value);
                    IEnumerable<ParameterObject> parameterObjects = baseParameterItem.Items.Where(w => w.Name == NameValue);
                    if (parameterObjects.Count() == 1)
                    {
                        memberInfo = ((PropertyInfo)member);
                        object ValueFromItem = parameterObjects.FirstOrDefault().Value;
                        if (!HasParser)
                        {
                            if (memberInfo.PropertyType == typeof(string))
                            {
                                ValueFromItem = Replacers.ClearDataName(Convert.ToString(ValueFromItem));
                            }
                            else if (memberInfo.PropertyType == typeof(int))
                            {
                                ValueFromItem = Convert.ToInt32(ValueFromItem);
                            }
                            else if (memberInfo.PropertyType == typeof(long))
                            {
                                ValueFromItem = Convert.ToInt64(ValueFromItem);
                            }
                            else if (memberInfo.PropertyType == typeof(bool))
                            {
                                ValueFromItem = Convert.ToBoolean(ValueFromItem);
                            }
                            memberInfo.SetValue(classObject, ValueFromItem, null);
                        }
                        else
                        {
                            string ParserName = Convert.ToString(InputDataAttr.NamedArguments.Where(w => w.MemberName == "ParserName").FirstOrDefault().TypedValue.Value);
                            MethodInfo IndividualParserCall = typeof(SubParser).GetMethod(ParserName, new Type[] { typeof(int), typeof(string) });

                            object ParserReturn = IndividualParserCall.Invoke(null, new[] { item.Id, ValueFromItem });
                            memberInfo.SetValue(classObject, ParserReturn, null);
                        }
                    }

                }
            }
            return classObject;
        }

        public object Parse(BaseName item)
        {
            classObject = constructorIWithoutAttrs.Invoke(null);

            BaseParameterName baseParameterItem = new BaseParameterName(item);
            MemberInfo[] memberInfos = classModel.GetMembers();
            foreach (MemberInfo member in memberInfos)
            {
                PropertyInfo memberInfo = null;

                if (member.Name == "Id")
                {
                    memberInfo = ((PropertyInfo)member);
                    memberInfo.SetValue(classObject, item.Id, null);
                }
                if (member.Name == "NameId")
                {
                    memberInfo = ((PropertyInfo)member);
                    memberInfo.SetValue(classObject, item.NameId, null);
                }

                IEnumerable<CustomAttributeData> parserMainAttributes = member.GetCustomAttributesData().Where(w => w.AttributeType.Name == "InputDataField");
                if (parserMainAttributes.Count() > 0)
                {
                    CustomAttributeData InputDataAttr = parserMainAttributes.FirstOrDefault();
                    string NameValue = Convert.ToString(InputDataAttr.NamedArguments.Where(w => w.MemberName == "Name").FirstOrDefault().TypedValue.Value);
                    bool HasParser = Convert.ToBoolean(InputDataAttr.NamedArguments.Where(w => w.MemberName == "SubParser").FirstOrDefault().TypedValue.Value);
                    IEnumerable<ParameterObject> parameterObjects = baseParameterItem.Items.Where(w => w.Name == NameValue);
                    if (parameterObjects.Count() == 1)
                    {
                        memberInfo = ((PropertyInfo)member);
                        object ValueFromItem = parameterObjects.FirstOrDefault().Value;
                        if (!HasParser)
                        {
                            if (memberInfo.PropertyType == typeof(string))
                            {
                                ValueFromItem = Replacers.ClearName(Convert.ToString(ValueFromItem));
                            }
                            else if (memberInfo.PropertyType == typeof(int))
                            {
                                ValueFromItem = Convert.ToInt32(ValueFromItem);
                            }
                            else if (memberInfo.PropertyType == typeof(long))
                            {
                                ValueFromItem = Convert.ToInt64(ValueFromItem);
                            }
                            else if (memberInfo.PropertyType == typeof(bool))
                            {
                                ValueFromItem = Convert.ToBoolean(ValueFromItem);
                            }
                            memberInfo.SetValue(classObject, ValueFromItem, null);
                        }
                        else
                        {
                            string ParserName = Convert.ToString(InputDataAttr.NamedArguments.Where(w => w.MemberName == "ParserName").FirstOrDefault().TypedValue.Value);
                            MethodInfo IndividualParserCall = typeof(SubParser).GetMethod(ParserName, new Type[] { typeof(int), typeof(string) });

                            object ParserReturn = IndividualParserCall.Invoke(null, new[] { item.Id, ValueFromItem });
                            memberInfo.SetValue(classObject, ParserReturn, null);
                        }
                    }

                }
            }
            return classObject;
        }

        public object Parse(BaseValues item)
        {
            classObject = constructorIWithoutAttrs.Invoke(null);

            BaseParameterValues baseParameterItem = new BaseParameterValues(item);
            MemberInfo[] memberInfos = classModel.GetMembers();



            foreach (MemberInfo member in memberInfos)
            {
                PropertyInfo memberInfo = null;

                if (member.Name == "NameId")
                {
                    memberInfo = ((PropertyInfo)member);
                    memberInfo.SetValue(classObject, item.NameId, null);
                }

                IEnumerable<CustomAttributeData> parserMainAttributes = member.GetCustomAttributesData().Where(w => w.AttributeType.Name == "InputDataField");
                if (parserMainAttributes.Count() > 0)
                {
                    CustomAttributeData InputDataAttr = parserMainAttributes.FirstOrDefault();
                    string NameValue = Convert.ToString(InputDataAttr.NamedArguments.Where(w => w.MemberName == "Name").FirstOrDefault().TypedValue.Value);
                    bool HasParser = Convert.ToBoolean(InputDataAttr.NamedArguments.Where(w => w.MemberName == "SubParser").FirstOrDefault().TypedValue.Value);
                    IEnumerable<ParameterObject> parameterObjects = baseParameterItem.Items.Where(w => w.Name == NameValue);
                    if (parameterObjects.Count() == 1)
                    {
                        memberInfo = ((PropertyInfo)member);
                        object ValueFromItem = parameterObjects.FirstOrDefault().Value;
                        if (!HasParser)
                        {
                            if (memberInfo.PropertyType == typeof(string))
                            {
                                ValueFromItem = Replacers.ClearDataName(Convert.ToString(ValueFromItem));
                            }
                            else if (memberInfo.PropertyType == typeof(int))
                            {
                                ValueFromItem = Convert.ToInt32(ValueFromItem);
                            }
                            else if (memberInfo.PropertyType == typeof(long))
                            {
                                ValueFromItem = Convert.ToInt64(ValueFromItem);
                            }
                            else if (memberInfo.PropertyType == typeof(bool))
                            {
                                ValueFromItem = Convert.ToBoolean(ValueFromItem);
                            }
                            memberInfo.SetValue(classObject, ValueFromItem, null);
                        }
                        else
                        {
                            string ParserName = Convert.ToString(InputDataAttr.NamedArguments.Where(w => w.MemberName == "ParserName").FirstOrDefault().TypedValue.Value);
                            MethodInfo IndividualParserCall = typeof(SubParser).GetMethod(ParserName, new Type[] { typeof(int), typeof(string) });

                            object ParserReturn = IndividualParserCall.Invoke(null, new[] { 0, ValueFromItem });
                            memberInfo.SetValue(classObject, ParserReturn, null);
                        }
                    }

                }
            }
            return classObject;
        }
    }
}
