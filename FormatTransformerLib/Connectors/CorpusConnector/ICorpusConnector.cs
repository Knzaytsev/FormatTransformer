using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public interface ICorpusConnector
    {
        public void Connect();
        public Corpus GetCorpora();
        public Corpus AddCorpus(object corpus);
        public void RemoveCorpus(object corpus);
        public Corpus EditCorpus(object corpus, string newName);
        public TextFile AddFile(object corpus, string fileName);
        public void RemoveFile(object file);
        public TextFile EditFile(object file, string newName);
    }
}
