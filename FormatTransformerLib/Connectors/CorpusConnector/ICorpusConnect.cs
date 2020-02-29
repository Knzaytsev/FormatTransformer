using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public interface ICorpusConnect
    {
        public void Connect();

        public List<ICorpora> GetCorpora();

        public void AddCorpus(string title);
        void AddFile(Corpus corpus, string fileName);
        void RemoveCorpus(Corpus corpus);
        void EditCorpus(ICorpora corpus, string title);
    }
}
