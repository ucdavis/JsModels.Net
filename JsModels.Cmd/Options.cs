using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace JsModels.Cmd
{
    class Options
    {
        [Option('i', "inputAssembly", HelpText = "Target Input Assebmly.", Required = true)]
        public string InputAssembly { get; set; }

        [Option('x', "classes", HelpText = "Comma delimited list of classes to export.", Required = true)]
        public string Classes { get; set; }

        [Option('o', "output", HelpText = "Output Directory.")]
        public string OutputDirectory { get; set; }

        [Option('v', "verbose", Default = false, HelpText = "Verbose Logging")]
        public bool Verbose { get; set; }

        [Usage(ApplicationAlias = "JsModels.Cmd")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Generate models", new Options { InputAssembly = "MyAssembly.dll", Classes = "Class1,Class2", OutputDirectory = "./output" });
            }
        }
    }
}
