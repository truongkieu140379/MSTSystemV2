using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class TuteeService : ITuteeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TuteeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CusResponse> Active(int id)
        {
            var entity = await _unitOfWork.TuteeRepository.GetById(id);
            if(entity != null)
            {
                if(entity.Status != GlobalConstants.ACTIVE_STATUS)
                {
                    entity.Status = GlobalConstants.ACTIVE_STATUS;
                    await _unitOfWork.TuteeRepository.Update(entity);

                    NotificationDto noti = new NotificationDto
                    {
                        Title = "Account status",
                        Message = "Your account has been active by system",
                        IsRead = false,
                        SendToUser = entity.Email
                    };
                    INotificationService notificationService = new NotificationService(_unitOfWork, _mapper);
                    await notificationService.Insert(noti);
                    return new CusResponse
                    {
                        Message = "Update status successfully",
                        Status = true,
                        Type = "success"
                    };
                }
                else
                {
                    return new CusResponse
                    {
                        Message = "Tutee has been update by another one",
                        Status = false,
                        Type = "fail"
                    };
                }
            }
            else
            {
                return new CusResponse
                {
                    Message = "Tutee account not found",
                    Status = false,
                    Type = "fail"
                };
            }
        }

        public async Task<Response<TuteeDto>> Filter(TuteeParameter parameter)
        {
            var entities = await _unitOfWork.TuteeRepository.Filter(parameter);
            return new Response<TuteeDto>
            {
                CurrentPage = entities.CurrentPage,
                PageSize = entities.PageSize,
                TotalCount = entities.TotalCount,
                TotalPages = entities.TotalPages,
                HasNext = entities.HasNext,
                HasPrevious = entities.HasPrevious,
                Data = entities.Select(t => new TuteeDto
                {
                    Id = t.Id,
                    Fullname = t.Fullname,
                    Address = t.Address,
                    AvatarImageLink = t.AvatarImageLink,
                    Birthday = t.Birthday,
                    CreatedDate = t.CreatedDate,
                    Description = t.Description,
                    Email = t.Email,
                    Gender =t.Gender,
                    Phone = t.Phone,
                    RoleId = t.RoleId,
                    Status = t.Status
                }).ToList()
            };
        }

        public async Task<TuteeDto> Get(string email)
        {
            var entity = await _unitOfWork.TuteeRepository.Get(email);
            return _mapper.Map<TuteeDto>(entity);
        }

        public async Task<IEnumerable<TuteeDto>> GetAll()
        {
            var entities = await _unitOfWork.TuteeRepository.GetAll();
            return _mapper.Map<IEnumerable<TuteeDto>>(entities).ToList();
        }

        public async Task<TuteeDto> GetById(int id)
        {
            var entity = await _unitOfWork.TuteeRepository.GetById(id);
            return _mapper.Map<TuteeDto>(entity);
        }

        public async Task<int> GetCountInMonth()
        {
            return await _unitOfWork.TuteeRepository.GetCountInMonth();
        }

        public async Task<IEnumerable<TuteeDto>> GetTuteeInCourse(int courseId)
        {
            var entities = await _unitOfWork.TuteeRepository.GetTuteeInCourse(courseId);
            return _mapper.Map<IEnumerable<TuteeDto>>(entities).ToList();
        }

        //public async Task Inactive(int id)
        //{
        //    var entity = await _unitOfWork.TuteeRepository.GetById(id);
        //    entity.Status = GlobalConstants.INACTIVE_STATUS;
        //    await _unitOfWork.TuteeRepository.Update(entity);
        //    await _unitOfWork.Commit();
        //}

        public async Task Insert(TuteeDto dto)
        {
            var entity = _mapper.Map<Tutee>(dto);
            await _unitOfWork.TuteeRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(TuteeDto dto)
        {
            var entity = _mapper.Map<Tutee>(dto);
            await _unitOfWork.TuteeRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task<CusResponse> Inactive(int id)
        {
                var entity = await _unitOfWork.TuteeRepository.GetById(id);
                if (entity != null)
                {
                    if (entity.Status != GlobalConstants.INACTIVE_STATUS)
                    {
                        entity.Status = GlobalConstants.INACTIVE_STATUS;
                        await _unitOfWork.TuteeRepository.Update(entity);
                  
                    return new CusResponse
                        {
                            Message = "Update status successfully",
                            Status = true,
                            Type = "success"
                        };
                    }
                    else
                    {
                        return new CusResponse
                        {
                            Message = "Tutee has been update by another one",
                            Status = false,
                            Type = "fail"
                        };
                    }
                }
                else
                {
                    return new CusResponse
                    {
                        Message = "Tutee account not found",
                        Status = false,
                        Type = "fail"
                    };
                }
        }

        Task IBaseService<TuteeDto>.Inactive(int id)
        {
            throw new NotImplementedException();
        }
    }
}
