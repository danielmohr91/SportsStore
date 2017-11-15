using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using MvcDemo.Dal.Interfaces;
using MvcDemo.Dal.EntityModels;
using System.Data.Objects;
using MvcDemo.Dal.Infrastructure;

namespace MvcDemo.Dal
{
    public class FileRepository : RepositoryBase<DocFile>, IFileRepository
    {
        public FileRepository()
            : this(new DmsRepositoryContext())
        {
        }

        public FileRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }

    public class CopyOfFileRepository : RepositoryBase<DocFile>, IFileRepository
    {
        public CopyOfFileRepository()
            : this(new DmsRepositoryContext())
        {
        }

        public CopyOfFileRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
