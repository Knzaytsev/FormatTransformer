using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.CorpusConnector
{
    class LocalConnector : ICorpusConnect
    {
        /// <summary>
        /// Подключение к корпусу на локальном диске.
        /// Если корпуса не существует, то создать.
        /// Иначе считать все файлы, находящиеся в нём.
        /// </summary>
        public void Connect(object connector)
        {
            throw new NotImplementedException();
        }

        public ICorpora GetCorpora()
        {
            throw new NotImplementedException();
        }
    }
}
