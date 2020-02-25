using FormatTransformerLib.Connectors.CorpusConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public class CorpusManager
    {
        private ICorpusConnect connector;
        private Corpus corpus;
        public void ConnectCorpus(ICorpusConnect connector)
        {
            this.connector = connector;
            connector.Connect(null);
            corpus = connector.GetCorpora();
        }
    }
}
