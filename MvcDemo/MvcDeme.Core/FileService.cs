using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using MvcDemo.Dal.Infrastructure;
using MvcDeme.Core.Models;
using MvcDemo.Core.Interfaces;
using MvcDemo.Dal.Interfaces;
using MvcDemo.Dal;

namespace MvcDemo.Core
{
    public class FileService : ServiceBase<IFileModel>, IFileService
    {
        public FileService()
            : this(new FileRepository())
        {
        }

        private IFileRepository _fileRepository;
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository ?? new FileRepository();
        }

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        public FileViewModel GetSingle(Expression<Func<FileViewModel, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Add(FileViewModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(FileViewModel entity)
        {
            throw new NotImplementedException();
        }

        public void Update(FileViewModel entity)
        {
            throw new NotImplementedException();
        }

        public IList<FileViewModel> GetAll(Expression<Func<FileViewModel, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public IList<FileViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<FileViewModel> Query(Expression<Func<FileViewModel, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<FileViewModel, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }
    }
}
