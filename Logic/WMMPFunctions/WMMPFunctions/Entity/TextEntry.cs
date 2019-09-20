using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace WMMPFunctions
{
    public class TextEntry : TableEntity
    {
        public TextEntry(string lastName, string firstName)
        {
            this.PartitionKey = lastName;
            this.RowKey = firstName;
        }

        public TextEntry() { }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
