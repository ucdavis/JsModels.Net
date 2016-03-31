using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Humanizer;

namespace JsModels
{
    public class JsModelGenerator
    {
        private readonly IEnumerable<Type> _refTypes;

        private static readonly Type[] _boolTypes = { typeof(bool) };
        private static readonly Type[] _numberTypes = { typeof(byte), typeof(short), typeof(int), typeof(long), typeof(float), typeof(decimal), typeof(double) };
        private static readonly Type[] _stringTypes = { typeof(string), typeof(Guid) };
        private static readonly Type[] _dateTypes = { typeof(DateTime), typeof(DateTimeOffset) };

        public JsModelGenerator(IEnumerable<Type> refTypes)
        {
            _refTypes = refTypes;
        }

        public string GenerateModels(IEnumerable<Type> models)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                foreach (var model in models)
                {
                    GenerateModel(model, writer);
                }
            }
            return sb.ToString();
        }

        public string GenerateModel(Type model)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                GenerateModel(model, writer);
                writer.Flush();
            }
            return sb.ToString();
        }

        public void GenerateModel(Type model, TextWriter writer)
        {
            writer.WriteLine("function {0}() {{", model.Name);

            foreach (var property in model.GetProperties())
            {
                var propName = property.Name.Camelize();

                writer.Write("    this.{0}", propName);

                if (_refTypes.Contains(property.PropertyType))
                {
                    writer.Write(" = new {0}()", property.PropertyType.Name);
                }
                else if (_stringTypes.Contains(property.PropertyType))
                {
                    writer.Write(" = ''");
                }
                else if (_numberTypes.Contains(property.PropertyType))
                {
                    writer.Write(" = 0");
                }
                else if (_boolTypes.Contains(property.PropertyType))
                {
                    writer.Write(" = false");
                }
                else if (typeof (IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    writer.Write(" = []");
                }
                else
                {
                    writer.Write(" = {}");
                }

                writer.WriteLine(";");
            }

            writer.WriteLine("}");
        }
    }
}
