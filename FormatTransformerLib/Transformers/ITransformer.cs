using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public interface ITransformer
    {
        public object Transform(object input, object rule, object output);
    }
}
