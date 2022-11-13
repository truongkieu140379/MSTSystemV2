using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{

    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MembershipService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<bool> CheckExist(string name)
        {
            return _unitOfWork.MembershipRepository.CheckExist(name);
        }

        public async Task<IEnumerable<MembershipDto>> GetAll()
        {
            var entities = await _unitOfWork.MembershipRepository.GetAll();
            return _mapper.Map<IEnumerable<MembershipDto>>(entities).ToList();
        }

        public async Task<IEnumerable<ExtendedMembershipDto>> GetAllExtendedMembership()
        {
            var entities = await _unitOfWork.MembershipRepository.GetAllExtendedMembership();
            return _mapper.Map<IEnumerable<ExtendedMembershipDto>>(entities).ToList();
        }

        public async Task<MembershipDto> GetById(int id)
        {
            var entity = await _unitOfWork.MembershipRepository.GetById(id);
            return _mapper.Map<MembershipDto>(entity);
        }

        public async Task<IEnumerable<MembershipDto>> GetByStatus(string status)
        {
            var entities = await _unitOfWork.MembershipRepository.GetByStatus(status);
            return _mapper.Map<IEnumerable<MembershipDto>>(entities).ToList();
        }

        public async Task<ExtendedMembershipDto> GetExtendedById(int id)
        {
            var entity = await _unitOfWork.MembershipRepository.GetExtendedById(id);
            return _mapper.Map<ExtendedMembershipDto>(entity);
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.MembershipRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.MembershipRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(MembershipDto dto)
        {
            var entity = _mapper.Map<Membership>(dto);
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.MembershipRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(MembershipDto dto)
        {
            var entity = _mapper.Map<Membership>(dto);
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.MembershipRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
