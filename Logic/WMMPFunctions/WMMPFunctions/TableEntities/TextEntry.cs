using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace WMMPFunctions.TableEntity
{
    public class TextEntry : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {
        public string Data { get; set; }

        public string Lock { get; set; }
    }
}
