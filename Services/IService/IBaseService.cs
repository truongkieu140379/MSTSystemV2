using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Services.IService
{
    public interface IBaseService<D> where D : class
    {
        //D here is Dto
        Task<IEnumerable<D>> GetAll();
        Task<D> GetById(int id);
        Task Update(D dto);
        Task Insert(D dto);
        Task Inactive(int id);

    }
}
