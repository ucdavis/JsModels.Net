﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
                if (string.IsNullOrWhiteSpace(options.InputAssembly))
                {
                    Console.WriteLine("Input Assembly required.");
                    Console.WriteLine("");
                    Console.WriteLine(options.GetUsage());
                    return 1;
                }

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
                var names = options.Classes.Split(',').Select(n => n.Trim()).ToList();
                var models = new List<Type>();
                foreach (var name in names)
                {
                    var model = assembly.GetType(name);
                    if (model == null)
                    {
                        Console.WriteLine("Could not find model by name of: {0}", name);
                        
                        var lower = name.ToLower();
                        var results = assembly.GetTypes().Where(t => t.Name.ToLower() == lower).ToList();
                        if (!results.Any())
                        {
                            Console.WriteLine("Could not find model by any other name. Exiting.");
                            return 1;
                        }

                        if (results.Count() > 1)
                        {
                            Console.WriteLine("Multiple models found by that name. Exiting.");
                            return 1;
                        }
                        model = results.First();

                        Console.WriteLine("Found model by name of: {0}", model.FullName);
                    }

                    models.Add(model);
                }

                if (!models.Any())
                {
                    Console.WriteLine("No models found. Exiting.");
                    return 1;
                }

                // use types for references
                var generator = new JsModelGenerator(models);

                foreach (var model in models)
                {
                    Console.WriteLine("Generating model for: {0}", model.FullName);

                    var filename = string.Format("{0}\\{1}.js", options.OutputDirectory, model.Name);
                    using (var writer = new StreamWriter(filename, false))
                    {
                        generator.GenerateModel(model, writer);
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
