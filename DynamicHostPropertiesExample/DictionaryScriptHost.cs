using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCs;
using ScriptCs.Contracts;

namespace DynamicHostExample
{
    public class DictionaryScriptHost : ScriptHost
    {
        private ScriptHost _host;

        public DictionaryScriptHost(IScriptPackManager scriptPackManager, ScriptEnvironment environment)
            :base(scriptPackManager, environment)
        {
            _host = new ScriptHost(scriptPackManager, environment);
            Data = new Dictionary<string, object>();
            Data["Foo"] = "Bar";
            Data["Message"] = new Message {Text = "I am a Message"};
        }

        public Dictionary<string, object> Data { get; set; }
        
    }

    public class Message
    {
        public string Text { get; set; }
    }
}
