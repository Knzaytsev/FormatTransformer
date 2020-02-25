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

        public void Transform()
        {
            XsltSettings settings = new XsltSettings();
            settings.EnableScript = true;
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(rule, settings, null);
            xslt.Transform(file, result);
        }

        public object GetResult()
        {
            return result;
        }
    }
}
