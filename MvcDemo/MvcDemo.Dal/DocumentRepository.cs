using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Linq.Expressions;
using MvcDemo.Dal.Interfaces;
using MvcDemo.Dal.EntityModels;
using System.Data.Objects;
using MvcDemo.Dal.Infrastructure;

namespace MvcDemo.Dal
{
    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        public DocumentRepository()
            : this(new DmsRepositoryContext())
        {
        }

        public DocumentRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }

    public class CopyOfDocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        public CopyOfDocumentRepository()
            : this(new DmsRepositoryContext())
        {
        }

        public CopyOfDocumentRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
