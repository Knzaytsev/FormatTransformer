using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public interface ICorpusConnect
    {
        public void Connect(object connector);

        public Corpus GetCorpora();
    }
}
