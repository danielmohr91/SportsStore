using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MvcDemo.Core.Interfaces;

namespace MvcDeme.Core.Models
{
    [Description("Files of the Sub Document")]
    public class FileViewModel : IFileModel
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        public Dictionary<string, object> UDF { get; set; }  
        [DisplayName("Document Name")]
        public string DocName { get; set; }
        [DisplayName("File Path")]
        public string Path { get; set; }
        [DisplayName("Version Number")]
        public int Version { get; set; }
        [DisplayName("Added Date")]
        public DateTime AddedDate { get; set; }
        [DisplayName("Added By")]
        public string AddedByName { get; set; }
        [DisplayName("File Sequence Number")]
        public int Sequence { get; set; }
    }
}
