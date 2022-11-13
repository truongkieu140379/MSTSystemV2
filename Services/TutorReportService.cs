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
    public class TutorReportService : ITutorReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TutorReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CusResponse> Accept(TutorReportDto dto)
        {
            var entity = await _unitOfWork.TutorReportRepository.GetById(dto.Id);
            if(entity != null)
            {
                if(entity.Status == GlobalConstants.PENDING_STATUS)
                {
                    entity.Status = GlobalConstants.ACCEPTED_STATUS;
                    entity.ConfirmedBy = dto.ConfirmedBy;
                    entity.ConfirmedDate = Tools.GetUTC();
                    await _unitOfWork.TutorReportRepository.Update(entity);
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
                    Message = "This report has been confirmed!"
                };
            }
            return new CusResponse
            {
                Status = false,
                Type = "fail",
                Message = "This report was not found!"
            };
        }

        public async Task<int> CountPending()
        {
            return await _unitOfWork.TutorReportRepository.CountPending();
        }

        public async Task<CusResponse> Deny(TutorReportDto dto)
        {
            var entity = await _unitOfWork.TutorReportRepository.GetById(dto.Id);
            if (entity != null)
            {
                if (entity.Status == GlobalConstants.PENDING_STATUS)
                {
                    entity.Status = GlobalConstants.DENIED_STATUS;
                    entity.ConfirmedBy = dto.ConfirmedBy;
                    entity.ConfirmedDate = Tools.GetUTC();
                    await _unitOfWork.TutorReportRepository.Update(entity);
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
                    Message = "This report has been confirmed!"
                };
            }
            return new CusResponse
            {
                Status = false,
                Type = "fail",
                Message = "This report was not found!"
            };
        }

        public async Task<Response<ExtendedTutorReportDto>> Filter(TutorReportParameter parameter)
        {
            var entities = await _unitOfWork.TutorReportRepository.Filter(parameter);
            return new Response<ExtendedTutorReportDto>
            {
                PageSize = entities.PageSize,
                CurrentPage = entities.CurrentPage,
                HasNext = entities.HasNext,
                HasPrevious = entities.HasPrevious,
                TotalCount = entities.TotalCount,
                TotalPages = entities.TotalPages,
                Data = entities.Select(t => new ExtendedTutorReportDto
                {
                    Id = t.Id,
                    ConfirmedBy = t.ConfirmedBy,
                    ConfirmedDate = t.ConfirmedDate,
                    ConfirmName = t.ConfirmName,
                    Image = t.Image,
                    Status = t.Status,
                    Description = t.Description,
                    TutorName = t.TutorName,
                    ReportName = t.ReportName,
                    CreatedDate = t.CreatedDate,
                    TutorEmail = t.TutorEmail
                }).ToList()
            };
        }

        public async Task<IEnumerable<TutorReportDto>> GetAll()
        {
            var entities = await _unitOfWork.TutorReportRepository.GetAll();
            return _mapper.Map<IEnumerable<TutorReportDto>>(entities).ToList();
        }

        public async Task<TutorReportDto> GetById(int id)
        {
            var entity = await _unitOfWork.TutorReportRepository.GetById(id);
            return _mapper.Map<TutorReportDto>(entity);
        }

        public Task Inactive(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(TutorReportDto dto)
        {
            var entity = _mapper.Map<TutorReport>(dto);
            await _unitOfWork.TutorReportRepository.Insert(entity);
            await _unitOfWork.Commit();
            INotificationService notificationService = new NotificationService(_unitOfWork, _mapper);
            await notificationService.SendNotificationToAdmin("Report Request", "You have new report request!");
        }

        public async Task Update(TutorReportDto dto)
        {
            var entity = _mapper.Map<TutorReport>(dto);
            entity.ConfirmedDate = Tools.GetUTC();
            await _unitOfWork.TutorReportRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
