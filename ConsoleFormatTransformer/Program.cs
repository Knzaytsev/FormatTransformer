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
            var transformer = new Transformer();
            transformer.AddFile(@"..\..\..\CorporaStore\opcTest.xml");
            transformer.AddRule(@"..\..\..\RuleStore\rule1.xsd");
            transformer.Transform(new DBTransformer());
            var result = transformer.GetResult();
            var corpusManager = new CorpusManager();
            corpusManager.ConnectCorpus(new DBConnector(@"Data Source=LAPTOP-6UGN0SO3\SQLEXPRESS01;Initial Catalog=OpenCorpora;Integrated Security=True"));
            corpusManager.AddCorpus(result);
            var corpora = corpusManager.GetCorpora();
            corpusManager.RemoveCorpus(corpora[1]);
        }
    }
}
