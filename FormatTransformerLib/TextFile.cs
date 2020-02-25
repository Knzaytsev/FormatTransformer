using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public class TextFile : ICorpora
    {
        public string Title { get; set; }
        public string Path { get; set; }

        public void Add(ICorpora element)
        {
            throw new NotImplementedException();
        }

        public void Delete(ICorpora element)
        {
            throw new NotImplementedException();
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
