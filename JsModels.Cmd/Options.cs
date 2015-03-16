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

        [Option('v', "verbose", DefaultValue = false, HelpText = "Verbose Logging")]
        public bool Verbose { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
