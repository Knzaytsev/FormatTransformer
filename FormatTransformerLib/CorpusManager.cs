using FormatTransformerLib.Connectors.CorpusConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public class CorpusManager
    {
        private ICorpusConnector connector;

        public void ConnectCorpus(ICorpusConnector connector)
        {
            this.connector = connector;
            connector.Connect();
        }

        public void AddCorpus(object corpus)
        {
            connector.AddCorpus(corpus);
        }

        public void AddCorpus(string title)
        {
            connector.AddCorpus(title);
        }

        public List<ICorpora> GetCorpora()
        {
            return connector.GetCorpora();
        }

        public void AddFile(Corpus corpus, string fileName)
        {
            connector.AddFile(corpus, fileName);
        }

        public void AddFile(ICorpora corpus, string fileName, string newName)
        {
            connector.AddFile(corpus, fileName, newName);
        }

        public void RemoveCorpus(ICorpora corpus)
        {
            connector.RemoveCorpus(corpus);
        }

        public void EditCorpus(ICorpora corpus, string title)
        {
            connector.EditCorpus(corpus, title);
        }

        public void RemoveFile(Corpus corpus, ICorpora file)
        {
            connector.RemoveFile(corpus, file);
        }

        public void EditFile(ICorpora corpus, ICorpora file, string textInfo)
        {
            connector.EditFile(corpus, file, textInfo);
        }
    }
}
