using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcDemo.Core.Models;

namespace MvcDemo.Core.Interfaces
{
    public interface IDocumentService : IService<DocumentViewModel>
    {
        DocumentListViewModel GetDocumentList(int PageSize, int PageCount);
        List<DocumentFileViewModel> GetDocumentFileList(int DocumentId);
    }
}
