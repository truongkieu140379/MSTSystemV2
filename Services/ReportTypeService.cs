using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class ReportTypeService : IReportTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CusResponse> Active(ReportTypeDto dto)
        {
            var entity = await _unitOfWork.ReportTypeRepository.GetById(dto.Id);
            if(entity != null)
            {
                if(entity.Status != GlobalConstants.ACTIVE_STATUS)
                {
                    entity.Status = GlobalConstants.ACTIVE_STATUS;
                    entity.UpdatedBy = dto.UpdatedBy;
                    entity.UpdatedDate = Tools.GetUTC();
                    await _unitOfWork.ReportTypeRepository.Update(entity);
                    await _unitOfWork.Commit();
                    return new CusResponse
                    {
                        Status = true,
                        Type = "success"
                    };
                }
                return new CusResponse
                {
                    Status = false,
                    Type = "fail",
                    Message = "This report type has been confirmed!"
                };
            }
            return new CusResponse
            {
                Status = false,
                Type = "fail",
                Message = "This report type was not found!"
            };
        }

        public async Task<Response<ExtendedReportTypeDto>> Filter(ReportTypeParameter parameter)
        {
            var entities = await _unitOfWork.ReportTypeRepository.Filter(parameter);
            return new Response<ExtendedReportTypeDto>
            {
                CurrentPage = entities.CurrentPage,
                HasNext = entities.HasNext,
                HasPrevious = entities.HasPrevious,
                PageSize = entities.PageSize,
                TotalCount = entities.TotalCount,
                TotalPages = entities.TotalPages,
                Data = entities.Select(r => new ExtendedReportTypeDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    CreatedBy = r.CreatedBy,
                    CreatedDate = r.CreatedDate,
                    CreatorName = r.CreatorName,
                    UpdatedBy = r.UpdatedBy,
                    UpdatedDate = r.UpdatedDate,
                    UpdatorName = r.UpdatorName,
                    Status = r.Status,
                    RoleName = r.RoleName,
                    RoleId = r.RoleId
                }).ToList()
            };
        }

        public async Task<IEnumerable<ReportTypeDto>> GetAll()
        {
            var entities = await _unitOfWork.ReportTypeRepository.GetAll();
            return _mapper.Map<IEnumerable<ReportTypeDto>>(entities).ToList();
        }

        public async Task<ReportTypeDto> GetById(int id)
        {
            var entity = await _unitOfWork.ReportTypeRepository.GetById(id);
            return _mapper.Map<ReportTypeDto>(entity);
        }

        public async Task<IEnumerable<ReportTypeDto>> GetByRole(int roleId)
        {
            var entities = await _unitOfWork.ReportTypeRepository.GetByRole(roleId);
            return _mapper.Map<IEnumerable<ReportTypeDto>>(entities).ToList();
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.ReportTypeRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.ReportTypeRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task<CusResponse> Inactive(ReportTypeDto dto)
        {
            var entity = await _unitOfWork.ReportTypeRepository.GetById(dto.Id);
            if (entity != null)
            {
                if (entity.Status != GlobalConstants.INACTIVE_STATUS)
                {
                    entity.Status = GlobalConstants.INACTIVE_STATUS;
                    entity.UpdatedBy = dto.UpdatedBy;
                    entity.UpdatedDate = Tools.GetUTC();
                    await _unitOfWork.ReportTypeRepository.Update(entity);
                    await _unitOfWork.Commit();
                    return new CusResponse
                    {
                        Status = true,
                        Type = "success"
                    };
                }
                return new CusResponse
                {
                    Status = false,
                    Type = "fail",
                    Message = "This report type has been confirmed!"
                };
            }
            return new CusResponse
            {
                Status = false,
                Type = "fail",
                Message = "This report type was not found!"
            };
        }

        public async Task Insert(ReportTypeDto dto)
        {
            var entity = _mapper.Map<ReportType>(dto);
            await _unitOfWork.ReportTypeRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(ReportTypeDto dto)
        {
            var entity = _mapper.Map<ReportType>(dto);
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.ReportTypeRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
