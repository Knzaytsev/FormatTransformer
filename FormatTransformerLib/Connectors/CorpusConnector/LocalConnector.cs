using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public class LocalConnector : ICorpusConnect
    {
        private Corpus corpora = new Corpus();
        /// <summary>
        /// Подключение к корпусу на локальном диске.
        /// Если корпуса не существует, то создать.
        /// Иначе считать все файлы, находящиеся в нём.
        /// </summary>
        public void Connect(object connector)
        {
            connector = @"..\..\..\";
            DirectoryInfo directoryInfo = new DirectoryInfo(connector + "CorporaStore");
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
