using System;

namespace JsModels.Example.Models
{
    public class SampleModel
    {
        public string MyValue { get; set; }
        public decimal MyDecimal { get; set; }
        public SampleChildModel MyChildModel { get; set; }
    }
}