using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WMMPFunctions.Entity
{
    public enum AccountLevel
    {
        Limited = 0,
        Secure = 1,
        Pro = 2,
        Advanced = 3,
        Integrated = 4,
        Test = 99
    }

    public enum AccountState
    {
        Ready = 0,
        Locked = 1,
        Archived = 2,
        Test = 99
    }

    [DataContract]
    public class Account
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "AccountLevel")]
        public AccountLevel AccountLevel { get; set; }

        [DataMember(Name = "AccountState")]
        public AccountState State { get; set; }

        [DataMember(Name = "User")]
        public User User { get; set; }
    }
}
