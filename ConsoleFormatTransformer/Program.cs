using System;
using FormatTransformerLib;
using FormatTransformerLib.Transformers;

namespace ConsoleFormatTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            var transformer = new Transformer();
            transformer.AddFile(@"..\..\..\CorporaStore\opcTest1.xml");
            transformer.AddRule(@"..\..\..\RuleStore\rule1.xsd");
            transformer.Transform(new DBTransformer());
        }
    }
}
