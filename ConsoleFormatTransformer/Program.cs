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
            transformer.AddFile(@"..\..\..\CorporaStore\DBCorpora\opcTest.xml");
            transformer.AddRule(@"..\..\..\RuleStore\rule1.xsd");
            transformer.Transform(new DBTransformer());
            var result = transformer.GetResult();
            var corpusManager = new CorpusManager();
            corpusManager.ConnectCorpus(new DBConnector(@"Data Source=LAPTOP-6UGN0SO3\SQLEXPRESS01;Initial Catalog=OpenCorpora;Integrated Security=True;MultipleActiveResultSets=true"));
            corpusManager.AddCorpus(result);
            var corpora = corpusManager.GetCorpora();
            //corpusManager.RemoveCorpus(corpora[1]);

            /*var transformer = new Transformer();
            transformer.AddFile(@"..\..\..\CorporaStore\XMLCorpora\books.xml");
            transformer.AddRule(@"..\..\..\RuleStore\Rule.xsl");
            transformer.Transform(new XMLTransformer());
            var result = transformer.GetResult();
            var corpusManager = new CorpusManager();
            corpusManager.ConnectCorpus(new LocalCorpusConnector());
            var corpora = corpusManager.GetCorpora();
            corpusManager.AddFile(corpora[1] as Icorpora, result.ToString(), "test");*/
        }
    }
}
