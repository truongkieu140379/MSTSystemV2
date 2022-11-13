﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Global;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Models.ExtendedModels;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        //get classes by subject id
        Task<IEnumerable<Class>> Search(int subjectId, string status);
        Task<bool> CheckExist(string name);
        Task<PagedList<Class>> Filter(ClassParameter parameter);
        Task<ExtendedClass> GetExtendedClassById(int id);
        Task<IEnumerable<Class>> GetActive();
    }
}
