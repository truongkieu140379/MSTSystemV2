using AutoMapper;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<RoleDto>> GetAll()
        {
            var entities = await _unitOfWork.RoleRepository.GetAll();
            return _mapper.Map<IEnumerable<RoleDto>>(entities).ToList();
        }

        public async Task<RoleDto> GetById(int id)
        {
            var entity = await _unitOfWork.RoleRepository.GetById(id);
            return _mapper.Map<RoleDto>(entity);
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.RoleRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.RoleRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(RoleDto dto)
        {
            var entity = _mapper.Map<Role>(dto);
            await _unitOfWork.RoleRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(RoleDto dto)
        {
            var entity = _mapper.Map<Role>(dto);
            await _unitOfWork.RoleRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
