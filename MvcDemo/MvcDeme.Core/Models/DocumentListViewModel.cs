using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcDemo.Core.Interfaces;

namespace MvcDemo.Core.Models
{
    public class DocumentListViewModel : IDocumentModel
    {
        public List<DocumentViewModel> DocumentList { get; set; }
        public int TotalItemCount { get; set; }
        public int ThisPageItemCount { get; set; }
        public int TotalPageCount { get; set; }
    }
}
