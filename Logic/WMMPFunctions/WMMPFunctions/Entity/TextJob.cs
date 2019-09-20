using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WMMPFunctions
{
    public enum TextJobType
    {
        LixCalc = 0,
        WordCount = 1,
        SpellCheck = 2,
        Translate = 3
    }
    
    [DataContract]
    public class TextJob
    {
        [DataMember(Name = "TextJobType")]
        public TextJobType TextJobType { get; set; }

        [DataMember(Name = "TextId")]
        public string TextId { get; set; }

        [DataMember(Name = "OwnerId")]
        public int OwnerId { get; set; }

        [DataMember(Name = "TextfileId")]
        public int TextfileId { get; set; }
    }
}
