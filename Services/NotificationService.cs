using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.FCMHelpers;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationDto>> GetAll()
        {
            var entities = await _unitOfWork.NotificationRepository.GetAll();
            return _mapper.Map<IEnumerable<NotificationDto>>(entities).ToList();
        }

        public async Task<NotificationDto> GetById(int id)
        {
            var entity = await _unitOfWork.NotificationRepository.GetById(id);
            return _mapper.Map<NotificationDto>(entity);
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationByEmail(string email)
        {
            var entities = await _unitOfWork.NotificationRepository.GetNotificationByEmail(email);
            return _mapper.Map<IEnumerable<NotificationDto>>(entities).ToList();
        }

        public Task Inactive(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(NotificationDto dto)
        {
            var entity = _mapper.Map<Notification>(dto);
            await _unitOfWork.NotificationRepository.Insert(entity);
            await _unitOfWork.Commit();
            //get user token
            var account = await _unitOfWork.AccountRepository.Get(dto.SendToUser);
            string tokenNotification = account?.TokenNotification;
            if (!String.IsNullOrEmpty(tokenNotification))
            {
                //send fcm message
                FCMHandler fCMHandler = new FCMHandler();
                var data = new
                {
                    notification = new
                    {
                        body = dto.Message,
                        title = dto.Title
                    },
                    to = tokenNotification
                };
                fCMHandler.SendNotification(data);
            }
        }

        public async Task SendNotificationToAdmin(string title, string message)
        {
            IEnumerable<Manager> managers = await _unitOfWork.ManagerRepository.GetByStatus(GlobalConstants.ACTIVE_STATUS, 1);
            foreach (var manager in managers)
            {
                NotificationDto notification = new NotificationDto
                {
                    Title = title,
                    Message = message,
                    SendToUser = manager.Email,
                    IsRead = false
                };
                //
                var entity = _mapper.Map<Notification>(notification);
                await _unitOfWork.NotificationRepository.Insert(entity);
                await _unitOfWork.Commit();
                //get user token
                var account = await _unitOfWork.AccountRepository.Get(notification.SendToUser);
                string tokenNotification = account?.TokenNotification;
                //
                if (!String.IsNullOrEmpty(tokenNotification))
                {
                    //send fcm message
                    FCMHandler fCMHandler = new FCMHandler();
                    var data = new
                    {
                        notification = new
                        {
                            body = notification.Message,
                            title = notification.Title
                        },
                        to = tokenNotification
                    };
                    fCMHandler.SendNotification(data);
                }
            }
        }

        public async Task SendNotificationToAllManager(string title, string message)
        {
            IEnumerable<Manager> managers = await _unitOfWork.ManagerRepository.GetAllByStatus(GlobalConstants.ACTIVE_STATUS);
            foreach (var manager in managers)
            {
                NotificationDto notification = new NotificationDto
                {
                    Title = title,
                    Message = message,
                    SendToUser = manager.Email,
                };
                //
                var entity = _mapper.Map<Notification>(notification);
                await _unitOfWork.NotificationRepository.Insert(entity);
                await _unitOfWork.Commit();
                //get user token
                var account = await _unitOfWork.AccountRepository.Get(notification.SendToUser);
                string tokenNotification = account?.TokenNotification;
                //
                if (!String.IsNullOrEmpty(tokenNotification))
                {
                    //send fcm message
                    FCMHandler fCMHandler = new FCMHandler();
                    var data = new
                    {
                        notification = new
                        {
                            body = notification.Message,
                            title = notification.Title
                        },
                        to = tokenNotification
                    };
                    fCMHandler.SendNotification(data);
                }
            }





            
            
            
        }

        public async Task Update(NotificationDto dto)
        {
            var entity = _mapper.Map<Notification>(dto);
            await _unitOfWork.NotificationRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
