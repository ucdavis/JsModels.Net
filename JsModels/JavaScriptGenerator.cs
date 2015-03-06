using System;
using System.Collections;
using System.Linq;
using System.Text;
using Humanizer;

namespace ModelPrototype
{
    public class JsModelGenerator
    {
        private readonly Type[] _types;

        private static readonly Type[] _numberTypes = { typeof(byte), typeof(short), typeof(int), typeof(long), typeof(float), typeof(decimal), typeof(double) };
        private static readonly Type[] _stringTypes = { typeof(string), typeof(Guid) };
        private static readonly Type[] _dateTypes = { typeof(DateTime), typeof(DateTimeOffset) };

        public JsModelGenerator(Type[] types)
        {
            _types = types;
        }

        public string GenerateModels(Type[] models)
        {
            var builder = new StringBuilder();
            foreach (var model in models)
            {
                GenerateModel(model, builder);
                builder.Append("\n");
            }
            return builder.ToString();
        }

        public string GenerateModel(Type model, StringBuilder builder = null)
        {
            if (builder == null)
            {
                builder = new StringBuilder();
            }

            builder.Append(string.Format("function {0}() ", model.Name) + "{\n");

            foreach (var property in model.GetProperties())
            {
                var propName = property.Name.Camelize();

                builder.Append(string.Format("    this.{0}", propName));

                if (_types.Contains(property.PropertyType))
                {
                     builder.Append(string.Format(" = new {0}()", property.PropertyType.Name));
                }
                else if (_stringTypes.Contains(property.PropertyType))
                {
                    builder.Append(" = ''");
                }
                else if (_numberTypes.Contains(property.PropertyType))
                {
                    builder.Append(" = 0");
                }
                else if (typeof (IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    builder.Append(" = []");
                }
                else
                {
                    builder.Append(" = {}");
                }

                builder.Append(";\n");
            }

            builder.Append("}");

            return builder.ToString();
        }
    }
}
