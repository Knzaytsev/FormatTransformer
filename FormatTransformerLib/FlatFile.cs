using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public class FlatFile
    {
        public string CorpusTitle { get; set; }
        //public string TextFileTitle { get; set; }

        // Ключ - название "файла", значение - плоские файлы.
        public Dictionary<string, List<string>> FlatFiles { get; } = new Dictionary<string, List<string>>();
    }
}
