using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JsModels.Cmd
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var options = new Options();
                if (!CommandLine.Parser.Default.ParseArguments(args, options)) return 1;

                // check options
                if (string.IsNullOrWhiteSpace(options.Classes))
                {
                    Console.WriteLine("Classes Option must be provided");
                    Console.WriteLine("");
                    Console.WriteLine(options.GetUsage());
                    return 1;
                }

                if (string.IsNullOrWhiteSpace(options.OutputDirectory))
                {
                    options.OutputDirectory = Environment.CurrentDirectory;
                }

                if (!Directory.Exists(options.OutputDirectory))
                {
                    Directory.CreateDirectory(options.OutputDirectory);
                }

                // load assembly
                var assembly = Assembly.LoadFile(options.InputAssembly);

                // find types
                var classNames = options.Classes.Split(',');
                var types = classNames.Select(assembly.GetType);

                // use types for references
                var generator = new JsModelGenerator(types);

                foreach (var name in classNames)
                {
                    Console.WriteLine("Generating model for: {0}", name);

                    var filename = string.Format("{0}\\{1}.js", options.OutputDirectory, name);
                    using (var writer = new StreamWriter(filename, false))
                    {
                        generator.GenerateModel(assembly.GetType(name), writer);
                        writer.Flush();
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return 1;
            }
        }
    }
}
