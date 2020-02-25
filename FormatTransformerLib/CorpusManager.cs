using FormatTransformerLib.Connectors.CorpusConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    class CorpusManager
    {
        private ICorpusConnect connector;
        private ICorpora corpus;
        public void ConnectCorpus(ICorpusConnect connector)
        {
            this.connector = connector;
            corpus = connector.GetCorpora();
        }
    }
}
