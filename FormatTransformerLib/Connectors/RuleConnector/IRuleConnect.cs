using System;
using System.Collections.Generic;
using System.Text;

namespace FormatTransformerLib.Connectors.RuleConnector
{
    public interface IRuleConnect
    {
        public void Connect();

        public List<Rule> GetRules();
    }
}
