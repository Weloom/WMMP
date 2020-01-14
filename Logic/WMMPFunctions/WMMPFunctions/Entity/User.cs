using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WMMPFunctions.Entity
{
 
    [DataContract]
    public class User
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }
    }
}
