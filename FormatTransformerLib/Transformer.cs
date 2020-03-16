using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace FormatTransformerLib
{
    public class Transformer
    {
        private object file;
        private object rule;
        private object result = "result";

        public void AddFile(object file)
        {
            this.file = file;
        }

        public void AddRule(object rule)
        {
            this.rule = rule;
        }

        public void AddResult(object result)
        {
            this.result = result;
        }

        public void Transform(ITransformer transformer)
        {
            result = transformer.Transform(file, rule, result);
        }

        public object GetResult()
        {
            return result;
        }
    }
}
