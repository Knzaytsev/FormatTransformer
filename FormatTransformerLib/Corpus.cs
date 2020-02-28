using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public class Corpus : ICorpora
    {
        private List<ICorpora> corpora = new List<ICorpora>();

        //private Dictionary<string, ICorpora> t = new Dictionary<string, ICorpora>();

        public string Title { get; set; }

        public void Add(ICorpora element)
        {
            corpora.Add(element);
        }

        public void Delete(ICorpora element)
        {
            corpora.Remove(element);
        }

        public void Edit(object info)
        {
            throw new NotImplementedException();
        }

        public ICorpora Get()
        {
            throw new NotImplementedException();
        }

        public List<ICorpora> GetCorpora()
        {
            return corpora;
        }
    }
}
