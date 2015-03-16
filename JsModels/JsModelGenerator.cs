using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humanizer;

namespace JsModels
{
    public class JsModelGenerator
    {
        private readonly IEnumerable<Type> _refTypes;

        private static readonly Type[] _numberTypes = { typeof(byte), typeof(short), typeof(int), typeof(long), typeof(float), typeof(decimal), typeof(double) };
        private static readonly Type[] _stringTypes = { typeof(string), typeof(Guid) };
        private static readonly Type[] _dateTypes = { typeof(DateTime), typeof(DateTimeOffset) };

        public JsModelGenerator(IEnumerable<Type> refTypes)
        {
            _refTypes = refTypes;
        }

        public string GenerateModels(IEnumerable<Type> models)
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

                if (_refTypes.Contains(property.PropertyType))
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
