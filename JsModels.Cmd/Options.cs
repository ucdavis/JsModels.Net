using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace JsModels.Cmd
{
    class Options
    {
        [Option('i', "inputAssembly", DefaultValue = "", HelpText = "Target Input Assebmly")]
        public string InputAssembly { get; set; }

        [Option('o', "output", DefaultValue = "", HelpText = "Output Directory")]
        public string OutputDirectory { get; set; }
        
        [Option('x', "classes", DefaultValue = "", HelpText = "Comma delimited list of classes to export")]
        public string Classes { get; set; }

        [Option('v', "verbose", DefaultValue = false, HelpText = "Verbose Logging")]
        public bool Verbose { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
