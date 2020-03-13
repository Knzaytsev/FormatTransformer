using FormatTransformerLib.Connectors.CorpusConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public class CorpusManager
    {
        private ICorpusConnector connector;
        //private List<ICorpora> corpora = new List<ICorpora>();
        private Corpus corpora;

        public void ConnectCorpus(ICorpusConnector connector)
        {
            this.connector = connector;
            connector.Connect();
            corpora = connector.GetCorpora();
        }

        public void AddCorpus(object corpus)
        {
            corpora.Add(connector.AddCorpus(corpus));
        }

        public void AddCorpus(string title)
        {
            corpora.Add(connector.AddCorpus(title));
        }

        /*public List<ICorpora> GetCorpora()
        {
            return connector.GetCorpora();
        }*/

        public Corpus GetCorpora()
        {
            return corpora;
        }

        // TODO: возможно стоит добавить возвращаемый объект методу AddFile.
        public void AddFile(Corpus corpus, string fileName)
        {
            corpus.Add(connector.AddFile(corpus.Title, fileName));
        }

        /*public void AddFile(ICorpora corpus, string fileName, string newName)
        {
            connector.AddFile(corpus, fileName, newName);
        }*/

        public void RemoveCorpus(ICorpora corpus)
        {
            connector.RemoveCorpus(corpus.Info);
            corpora.Delete(corpus);
        }

        /*public void RemoveCorpus(ICorpora corpus)
        {
            connector.RemoveCorpus(corpus);
        }*/

        /*public void EditCorpus(ICorpora corpus, string title)
        {
            connector.EditCorpus(corpus, title);
        }*/

        public void EditCorpus(ICorpora corpus, string title)
        {
            corpus = connector.EditCorpus(corpus.Info, title);
        }

        /*public void RemoveFile(Corpus corpus, ICorpora file)
        {
            connector.RemoveFile(corpus, file);
        }*/

        public void RemoveFile(Corpus corpus, ICorpora file)
        {
            connector.RemoveFile(file.Info);
            corpus.Delete(file);
        }

        /*public void EditFile(ICorpora corpus, ICorpora file, string textInfo)
        {
            connector.EditFile(corpus, file, textInfo);
        }*/

        public void EditFile(ICorpora corpus, ICorpora file, string textInfo)
        {
            file = connector.EditFile(file.Info, textInfo);
        }
    }
}
