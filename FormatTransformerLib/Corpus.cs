using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    class Corpus : ICorpora
    {
        private List<ICorpora> corpora = new List<ICorpora>();

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
    }
}
