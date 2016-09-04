using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCs;
using ScriptCs.Contracts;

namespace DynamicHostExample
{
    public class DictionaryScriptHostFactory : IScriptHostFactory
    {
        public IScriptHost CreateScriptHost(IScriptPackManager scriptPackManager, string[] scriptArgs)
        {
            return new DictionaryScriptHost(scriptPackManager, new ScriptEnvironment(scriptArgs));
        }
    }
}
