using System;
using System.Collections.Generic;

namespace JsModels.Example.Models
{
    public class SampleChildModel
    {
        public SampleChildModel()
        {
            MyOtherDictionary = new Dictionary<string, string>();
        }

        public string MyOtherValue { get; set; }
        public decimal MyOtherDecimal { get; set; }
        public bool MyOtherBool { get; set; }
        public Dictionary<string, string> MyOtherDictionary { get; set; } 
    }
}