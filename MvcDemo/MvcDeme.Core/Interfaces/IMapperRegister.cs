using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcDemo.Core.Interfaces
{
    public interface IMapperRegister
    {
        void Configure();
        void Initialize();
    }
}
