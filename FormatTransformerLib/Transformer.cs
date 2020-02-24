using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace FormatTransformerLib
{
    public class Transformer
    {
        private string file;
        private string rule;
        private string result;

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
            XslTransform xslt = new XslTransform();
            xslt.Load(rule);
            xslt.Transform(file, result);
        }

        public object GetResult()
        {
            return result;
        }
    }
}
