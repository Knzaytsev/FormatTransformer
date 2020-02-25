using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib
{
    public interface ITransform
    {
        public void Transform(object input, object rule, object output);
    }
}
