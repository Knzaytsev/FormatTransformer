using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public class LocalCorpusConnector : ICorpusConnect
    {
        private string connectorString = @"..\..\..\";
        private Corpus corpora = new Corpus();

        public LocalCorpusConnector()
        {

        }

        public LocalCorpusConnector(string connectorString)
        {
            this.connectorString = connectorString;
        }

        /// <summary>
        /// Подключение к корпусу на локальном диске.
        /// Если корпуса не существует, то создать.
        /// Иначе считать все файлы, находящиеся в нём.
        /// </summary>
        public void Connect()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString + "CorporaStore");
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            foreach(var d in directoryInfo.GetDirectories())
            {
                Corpus corpus = new Corpus() { Title = d.Name };
                foreach (var f in d.GetFiles())
                {
                    var file = new TextFile()
                    {
                        Path = f.FullName,
                        Title = f.Name
                    };
                    corpus.Add(file);
                }
                corpora.Add(corpus);
            }
        }

        public Corpus GetCorpora()
        {
            return corpora;
        }
    }
}
