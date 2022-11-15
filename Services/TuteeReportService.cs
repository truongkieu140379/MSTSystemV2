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
using Microsoft.EntityFrameworkCore;

namespace TutorSearchSystem.Services
{
    public class TuteeReportService : ITuteeReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TuteeReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CusResponse> Accept(CustomerReportDto dto)
        {
            
            var entity = await _unitOfWork.TuteeReportRepository.GetById(dto.Id);
            if(entity != null)
            {
                if(entity.Status == GlobalConstants.PENDING_STATUS)
                {
                    entity.Status = GlobalConstants.ACCEPTED_STATUS;
                    entity.ConfirmedBy = dto.ConfirmedBy;
                    entity.ConfirmedDate = Tools.GetUTC();
                    await _unitOfWork.TuteeReportRepository.Update(entity);
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
                    Message = "This report has been confirmd!",
                    Type = "fail"
                };
            }
            return new CusResponse
            {
                Status = false,
                Message = "This report was not found!",
                Type = "fail"
            };
        }

        public async Task<int> CountPending()
        {
            return await _unitOfWork.TuteeReportRepository.CountPending();
        }

        public async Task<CusResponse> Deny(CustomerReportDto dto)
        {
            var entity = await _unitOfWork.TuteeReportRepository.GetById(dto.Id);
            if (entity != null)
            {
                if (entity.Status == GlobalConstants.PENDING_STATUS)
                {
                    entity.Status = GlobalConstants.DENIED_STATUS;
                    entity.ConfirmedBy = dto.ConfirmedBy;
                    entity.ConfirmedDate = Tools.GetUTC();
                    await _unitOfWork.TuteeReportRepository.Update(entity);
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
                    Message = "This report has been confirmd!",
                    Type = "fail"
                };
            }
            return new CusResponse
            {
                Status = false,
                Message = "This report was not found!",
                Type = "fail"
            };
        }

        public async Task<Response<ExtendedCustomerReportDto>> Filter(TuteeReportParameter parameter)
        {
            var entities = await _unitOfWork.TuteeReportRepository.Filter(parameter);
            return new Response<ExtendedCustomerReportDto>
            {
                CurrentPage = entities.CurrentPage,
                HasNext = entities.HasNext,
                HasPrevious = entities.HasPrevious,
                TotalCount = entities.TotalCount,
                TotalPages = entities.TotalPages,
                PageSize = entities.PageSize,
                Data = entities.Select(t => new ExtendedCustomerReportDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    ConfirmedBy = t.ConfirmedBy,
                    ConfirmedDate = t.ConfirmedDate,
                    ConfirmName = t.ConfirmName,
                    TuteeName = t.TuteeName,
                    TutorName = t.TutorName,
                    CourseName = t.CourseName,
                    CreatedDate = t.CreatedDate,
                    Image = t.Image,
                    ReportName = t.ReportName,
                    Status = t.Status,
                    TuteeEmail = t.TuteeEmail,
                    TutorEmail = t.TutorEmail,
                    CourseId = t.CourseId
                }).ToList()
            };
        }

        public async Task<IEnumerable<CustomerReportDto>> GetAll()
        {
            var entities = await _unitOfWork.TuteeReportRepository.GetAll();
            return _mapper.Map<IEnumerable<CustomerReportDto>>(entities).ToList();
        }

        public async Task<CustomerReportDto> GetById(int id)
        {
            var entity = await _unitOfWork.TuteeReportRepository.GetById(id);
            return _mapper.Map<CustomerReportDto>(entity);
        }

        public Task Inactive(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(CustomerReportDto dto)
        {
            var entity = _mapper.Map<TuteeReport>(dto);
            await _unitOfWork.TuteeReportRepository.Insert(entity);
            await _unitOfWork.Commit();
            INotificationService notificationService = new NotificationService(_unitOfWork, _mapper);
            await notificationService.SendNotificationToAdmin("Report Request", "You have new report request!");
        }

        public async Task Update(CustomerReportDto dto)
        {
            var entity =_mapper.Map<TuteeReport>(dto);
            entity.ConfirmedDate = Tools.GetUTC();
            await _unitOfWork.TuteeReportRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        
    }
}
