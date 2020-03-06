using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace FormatTransformerLib
{
    public class Transformer
    {
        private string file = @"..\..\..\..\FormatTransformer\CorporaStore\books.xml";
        private string rule = @"..\..\..\..\FormatTransformer\RuleStore\Rule.xsl";
        private string result = @"..\..\..\..\FormatTransformer\CorporaStore\result.html";

        public void AddFile(string file)
        {
            this.file = file;
        }

        public void AddRule(string rule)
        {
            this.rule = rule;
        }

        public bool CheckFileRule()
        {
            return false;
        }

        public void Transform(ITransformer transformer)
        {
            transformer.Transform(file, rule, result);
        }

        public object GetResult()
        {
            return result;
        }
    }
}
