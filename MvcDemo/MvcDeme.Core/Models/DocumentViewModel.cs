using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MvcDemo.Core.Interfaces;

namespace MvcDemo.Core.Models
{
    public class DocumentViewModel : IDocumentModel
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        [Obsolete("This property is obsolete. please use DocumentName instead")]
        public string Name { get; set; }
        [DisplayName("Name")]
        public string DocumentName { get; set; }
        [DisplayName("Updated Date")]
        public DateTime UpdateDate { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Document Owner")]
        public string OwnerName { get; set; }
        [DisplayName("Document Counts")]
        public int DocFilesCount { get; set; }
    }

    public class DocumentFileViewModel : IDocumentModel
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        [DisplayName("Document File Name")]
        public string DocName { get; set; }
        public string VersionNumber { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
    }
}