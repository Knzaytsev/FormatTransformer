using System;
using FormatTransformerLib;
using FormatTransformerLib.Transformers;
using FormatTransformerLib.Connectors.CorpusConnector;

namespace ConsoleFormatTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            TransformDBXML();
            //TransformXMLDB();
        }
        
        static private void TransformDBXML()
        {
            var transformer = new Transformer();
            var dbCorpusManager = new DBCorpusManager();
            dbCorpusManager.ConnectCorpus(
                new DBConnector(
                    @"Data Source=LAPTOP-6UGN0SO3\SQLEXPRESS01;Initial Catalog=OpenCorpora;Integrated Security=True;MultipleActiveResultSets=true"));
            var ds = dbCorpusManager.GetDataSetFromDB();
            transformer.AddFile(ds);
            transformer.Transform(new DBXMLTransformer());
            var result = transformer.GetResult();
            var corpusManager = new CorpusManager();
            corpusManager.ConnectCorpus(new LocalCorpusConnector());
            corpusManager.AddCorpus(result);
        }

        static private void TransformXMLDB()
        {
            var transformer = new Transformer();
            transformer.AddFile(@"..\..\..\CorporaStore\DBCorpora\opcTest.xml");
            transformer.AddRule(@"..\..\..\RuleStore\rule1.xsd");
            transformer.Transform(new XMLDBTransformer());
            var result = transformer.GetResult();
            var corpusManager = new CorpusManager();
            corpusManager.ConnectCorpus(new DBConnector(@"Data Source=LAPTOP-6UGN0SO3\SQLEXPRESS01;Initial Catalog=OpenCorpora;Integrated Security=True;MultipleActiveResultSets=true"));
            corpusManager.AddCorpus(result);
        }

        private void TransformXMLXML()
        {
            var transformer = new Transformer();
            transformer.AddFile(@"..\..\..\CorporaStore\XMLCorpora\books.xml");
            transformer.AddRule(@"..\..\..\RuleStore\Rule.xsl");
            transformer.Transform(new XMLXMLTransformer());
            var result = transformer.GetResult();
            var corpusManager = new CorpusManager();
            corpusManager.ConnectCorpus(new LocalCorpusConnector());
        }
    }
}
