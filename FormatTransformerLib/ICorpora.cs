using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public interface ICorpora
    {
        public string Title { get; set; }
        public string Info { get; set; }
        public void Add(ICorpora element);

        public void Delete(ICorpora element);

        public void Edit(object info);

        public ICorpora Get();
    }
}
