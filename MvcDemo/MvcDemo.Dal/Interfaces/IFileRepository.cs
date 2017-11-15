using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcDemo.Dal.EntityModels;

namespace MvcDemo.Dal.Interfaces
{
    public interface IFileRepository : IRepository<DocFile>
    {
    }
}
