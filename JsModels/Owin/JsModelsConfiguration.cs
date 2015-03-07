using System;
using System.Collections.Generic;

namespace JsModels.Owin
{
    public class JsModelsConfiguration
    {
        public JsModelsConfiguration()
        {
            Path = "/jsmodels";
            Models = new List<Type>();
        }

        /// <summary>
        /// Models to transform
        /// </summary>
        public List<Type> Models { get; set; }

        public string Path { get; set; }
    }
}
