using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    public class LocalCorpusFlatFilesConnector : ICorpusConnector
    {
        private string connectorString = @"..\..\..\CorporaStore\";

        public LocalCorpusFlatFilesConnector() { }

        public LocalCorpusFlatFilesConnector(string connectorString)
        {
            this.connectorString = connectorString;
        }

        public Corpus AddCorpus(object corpus)
        {
            var flatFiles = corpus as FlatFile;
            var corpora = new Corpus() 
            { 
                Title = flatFiles.CorpusTitle, 
                Info = connectorString + flatFiles.CorpusTitle 
            };

            var directoryInfo = new DirectoryInfo(connectorString + flatFiles.CorpusTitle);

            if (!directoryInfo.Exists)
                directoryInfo.Create();

            foreach(var tf in flatFiles.FlatFiles.Keys)
            {
                var directoryInfoSub = directoryInfo.CreateSubdirectory(tf);
                var textFile = new TextFile()
                {
                    Title = tf,
                    Info = connectorString + flatFiles.CorpusTitle + @"\" + tf
                };

                foreach(var f in flatFiles.FlatFiles[tf])
                {
                    var fileInfo = new FileInfo(f);
                    fileInfo.CopyTo(connectorString + flatFiles.CorpusTitle + @"\" + Path.GetFileName(f));
                }
                corpora.Add(textFile);
            }

            return corpora;
        }

        public TextFile AddFile(object corpus, string fileName)
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(connectorString);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public Corpus EditCorpus(object corpus, string newName)
        {
            throw new NotImplementedException();
        }

        public TextFile EditFile(object file, string newName)
        {
            throw new NotImplementedException();
        }

        public Corpus GetCorpora()
        {
            Corpus corpora = new Corpus() 
            { 
                Title = "CorporaStore", 
                Info = connectorString
            };
            var directoryInfo = new DirectoryInfo(connectorString);

            foreach(var du in directoryInfo.GetDirectories())
            {
                var corpus = new Corpus() 
                { 
                    Title = du.Name, 
                    Info = connectorString + du.Name 
                };
                foreach(var dd in du.GetDirectories())
                {
                    var textFile = new TextFile()
                    {
                        Title = dd.Name,
                        Info = connectorString + dd.Name
                    };

                    corpus.Add(textFile);
                }
                corpora.Add(corpus);
            }

            return corpora;
        }

        public void RemoveCorpus(object corpus)
        {
            var directoryInfo = new DirectoryInfo(corpus as string);
            directoryInfo.Delete(true);
        }

        public void RemoveFile(object file)
        {
            var directoryInfo = new DirectoryInfo(file as string);
            directoryInfo.Delete(true);
        }
    }
}
