using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace FormatTransformerLib
{
    public class XMLTransformer : ITransform
    {
        public void Transform(object input, object rule, object output)
        {
            XsltSettings settings = new XsltSettings();
            settings.EnableScript = true;
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(rule as string, settings, null);
            xslt.Load(rule as string, settings, null);
            xslt.Transform(input as string, output as string);
        }
    }
}
