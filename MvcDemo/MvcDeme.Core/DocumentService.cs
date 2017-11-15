using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using MvcDemo.Core;
using MvcDemo.Core.Models;
using MvcDemo.Core.Interfaces;
using MvcDemo.Dal.EntityModels;
using MvcDemo.Dal;
using MvcDemo.Dal.Interfaces;
using System.Data;

namespace MvcDemo.Core
{
    public class DocumentService : ServiceBase<IDocumentModel>, IDocumentService
    {
        public DocumentService()
            : this(new DocumentRepository())
        {
        }

        private IDocumentRepository _documentRepository;
        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository ?? new DocumentRepository();
        }

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        public DocumentListViewModel GetDocumentList(int PageSize, int CurrentPage)
        {
            try
            {
                if ((CurrentPage == 0) || (PageSize == 0))
                    return null;

                IQueryable<Document> query = _documentRepository.GetQueryable();
                DocumentListViewModel model = new DocumentListViewModel();
                model.TotalItemCount = query.Count();
                if (model.TotalItemCount == 0)
                    return null;
                model.TotalPageCount = (int)Math.Ceiling((double)model.TotalItemCount / (double)PageSize);
                if (model.TotalPageCount != 1)
                    query = query.OrderBy(x => x.DocumentName).Skip((CurrentPage - 1) * PageSize).Take(PageSize);

                model.ThisPageItemCount = query.Count();
                model.DocumentList = new List<DocumentViewModel>();
                AutoMapper.Mapper.CreateMap<Document, DocumentViewModel>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.DocumentId));
                model.DocumentList = AutoMapper.Mapper.Map(query.ToList(), model.DocumentList);

                return model;
            }
            catch (System.Exception e)
            {
                this.LogError("Error getting the document list", e);
                return null;
            }
        }

        public List<DocumentFileViewModel> GetDocumentFileList(int DocumentId)
        {
            try
            {
                if (DocumentId == 0)
                    return null;

                IQueryable<Document> query = _documentRepository.GetQueryable();

                FileVersion maxVersion = null;
                AutoMapper.Mapper.CreateMap<DocFile, DocumentFileViewModel>()
                    .BeforeMap((s, d) => maxVersion = s.FileVersions.OrderBy(x => x.Version).FirstOrDefault())
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.FileId))
                    .ForMember(dest => dest.VersionNumber, opt => opt.MapFrom(src => maxVersion.Version.ToString("V 0.0.0")))
                    .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => maxVersion.AddedDate))
                    .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => maxVersion.AddedByName))
                    .ForAllMembers(x => x.NullSubstitute("No Record Found"));

                DocFile[] docFiles = query.SingleOrDefault(x => x.DocumentId == DocumentId).DocFiles.ToArray();
                List<DocumentFileViewModel> dfList = new List<DocumentFileViewModel>();
                dfList = AutoMapper.Mapper
                    .Map(docFiles, dfList);

                return dfList;
            }
            catch (Exception e)
            {
                this.LogError("Error getting Document File List", e);
                return null;
            }
        }

        public DocumentViewModel GetSingle(Expression<Func<DocumentViewModel, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Add(DocumentViewModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DocumentViewModel entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DocumentViewModel entity)
        {
            throw new NotImplementedException();
        }

        public IList<DocumentViewModel> GetAll(Expression<Func<DocumentViewModel, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public IList<DocumentViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<DocumentViewModel> Query(Expression<Func<DocumentViewModel, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<DocumentViewModel, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }
    }
}
