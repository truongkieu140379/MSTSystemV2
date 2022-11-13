using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TutorSearchSystem.Auth_Models;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class TutorService : ITutorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthContainer _container;

        public TutorService(IUnitOfWork unitOfWork, IMapper mapper,IAuthContainer container)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _container = container;
        }

        public async Task<TutorDto> Get(string email)
        {
            var entity = await _unitOfWork.TutorRepository.Get(email);
            return _mapper.Map<TutorDto>(entity);
        }

        public async Task<IEnumerable<ExtendedTutorDto>> GetForManager(int managerId)
        {
            var tutors = await _unitOfWork.TutorRepository.GetForManager(managerId);
            ////get tutor certification images
            //foreach (var t in tutors)
            //{
            //    var certis = await _unitOfWork.ImageRepository.Get(t.Email, "certification");
            //    foreach (var c in certis)
            //    {
            //        Console.WriteLine("this is images: " + c.ImageLink);
            //        t.CertificationUrls.Append<string>(c.ImageLink);
            //        Console.WriteLine("this is images in extended tutor: " + t.CertificationUrls[0]); ;
            //    }
            //}
            return _mapper.Map<IEnumerable<ExtendedTutorDto>>(tutors).ToList();
        }

        public async Task<IEnumerable<TutorDto>> GetAll()
        {
            var entities = await _unitOfWork.TutorRepository.GetAll();
            return _mapper.Map<IEnumerable<TutorDto>>(entities).ToList();
        }

        public async Task<TutorDto> GetById(int id)
        {
            var entity = await _unitOfWork.TutorRepository.GetById(id);
            return _mapper.Map<TutorDto>(entity);
        }

        public async Task<IEnumerable<ExtendedTutorDto>> GetByStatus(string status)
        {
            var entities = await _unitOfWork.TutorRepository.GetByStatus(status);
            return _mapper.Map<IEnumerable<ExtendedTutorDto>>(entities).ToList();
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.TutorRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.TutorRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(TutorDto dto)
        {
            var entity = _mapper.Map<Tutor>(dto);
            await _unitOfWork.TutorRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(TutorDto dto)
        {
                var entity = _mapper.Map<Tutor>(dto);
                await _unitOfWork.TutorRepository.Update(entity);
                await _unitOfWork.Commit();
        }

        public async Task<bool> Deactive(int tutorId, int managerId)
        {
            return await DeactiveTutor(tutorId, managerId);
        }

        private async Task<bool> DeactiveTutor(int tutorId, int managerId)
        {
            var entity = await _unitOfWork.TutorRepository.GetById(tutorId);
            if(entity != null)
            {
                if (entity.Status == GlobalConstants.INACTIVE_STATUS)
                {
                    return false;
                }
                else
                {
                    entity.Status = GlobalConstants.INACTIVE_STATUS;
                    entity.ConfirmedBy = managerId;
                    entity.ConfirmedDate = Tools.GetUTC();
                    await _unitOfWork.TutorRepository.Update(entity);
                    await _unitOfWork.Commit();
                    //invalidate JWT 
                    Account account = await _unitOfWork.AccountRepository.Get(entity.Email);
                    //
                    var tokenKey = _container.SecretKey;
                    var key = Encoding.ASCII.GetBytes(tokenKey);
                    //
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Email, account.Email),
                        }),
                        //expires in 7 days
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                    };
                    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                    string generatedToken = tokenHandler.WriteToken(token);
                    return true;
                }
            }
            return false;
        }

        private async Task DeactiveCoursesByTutorId(int tutorId, int managerId)
        {
            //get all course of this tutor
            var courses = await _unitOfWork.CourseRepository.Get(tutorId, "All");
            //travese for deactive all course of this tutor
            foreach (var c in courses)
            {
                c.Status = GlobalConstants.INACTIVE_STATUS;
                c.ConfirmedBy = managerId;
                c.ConfirmedDate = Tools.GetUTC();
                await _unitOfWork.CourseRepository.Update(c);
            }
            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<ExtendedTutorDto>> GetAllExtended()
        {
            var entities = await _unitOfWork.TutorRepository.GetAllExtended();
            return _mapper.Map<IEnumerable<ExtendedTutorDto>>(entities).ToList();
        }

        public async Task<ExtendedTutorDto> GetExtendedById(int id)
        {
            var entity = await _unitOfWork.TutorRepository.GetExtendedById(id);
            //
            string[] certiImages = await _unitOfWork.ImageRepository.Get(entity.Email, "certification");
            //
            
            entity.CertificationUrls = certiImages;
            //
            var membership = await _unitOfWork.MembershipRepository.GetById(entity.MembershipId);
            entity.MembershipName = membership.Name;
            //set average rating star
            var avg = await _unitOfWork.FeedbackRepository.GetAverageRatingStateByTutorId(id);
            entity.AverageRatingStar = avg;
            //get total tutee of this tutor id
            var numberOfTutee = await _unitOfWork.TutorRepository.GetNumberOfTuteeByTutorId(id);
            entity.NumberOfTutee = numberOfTutee;
            //get total course of this tutor
            var numberOfCourse = await _unitOfWork.TutorRepository.GetNumberOfCourseByTutorId(id);
            entity.NumberOfCourse = numberOfCourse;
            //gettotal feedback of this tutor
            var numberOfFeedback = await _unitOfWork.TutorRepository.GetNumberOfFeedbackByTutorId(id);
            entity.NumberOfFeedback = numberOfFeedback;
            //
            return _mapper.Map<ExtendedTutorDto>(entity);
        }

        public async Task<int> GetCountInMonth()
        {
            return await _unitOfWork.TutorRepository.GetCountInMonth();
        }

        public async Task<Response<ExtendedTutorDto>> Filter(TutorParameter parameter)
        {
            var entities = await _unitOfWork.TutorRepository.Filter(parameter);
            return new Response<ExtendedTutorDto>
            {
                CurrentPage = entities.CurrentPage,
                PageSize = entities.PageSize,
                TotalPages = entities.TotalPages,
                TotalCount = entities.TotalCount,
                HasNext = entities.HasNext,
                HasPrevious = entities.HasPrevious,
                Data = entities.Select(t => new ExtendedTutorDto
                {
                    Fullname = t.Fullname,
                    Gender = t.Gender,
                    Birthday = t.Birthday,
                    Email = t.Email,
                    Phone = t.Phone,
                    AvatarImageLink = t.AvatarImageLink,
                    Address = t.Address,
                    RoleId = t.RoleId,
                    Id = t.Id,
                    EducationLevel = t.EducationLevel,
                    School = t.School,
                    Points = t.Points,
                    MembershipId = t.MembershipId,
                    SocialIdUrl = t.SocialIdUrl,
                    Description = t.Description,
                    Status = t.Status,
                    ConfirmedBy = t.ConfirmedBy,
                    ConfirmedDate = t.ConfirmedDate,
                    CreatedDate = t.CreatedDate,
                    MembershipName = t.MembershipName,
                    ConfirmerName = t.ConfirmerName
                }).ToList()
            };
        }

        public async Task<int> Count()
        {
            var entities = await _unitOfWork.TutorRepository.GetAll();
            return entities.Count();
        }

        public async Task<bool> Accept(TutorDto dto)
        {
            var entity = await _unitOfWork.TutorRepository.GetById(dto.Id);
            if(entity != null)
            {
                if(entity.Status == GlobalConstants.PENDING_STATUS)
                {
                    entity.Status = GlobalConstants.ACTIVE_STATUS;
                    entity.ConfirmedBy = dto.ConfirmedBy;
                    entity.ConfirmedDate = Tools.GetUTC();
                    await _unitOfWork.TutorRepository.Update(entity);
                    await _unitOfWork.Commit();
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> Deny(TutorDto dto)
        {
            var entity = await _unitOfWork.TutorRepository.GetById(dto.Id);
            if (entity != null)
            {
                if(entity.Status == GlobalConstants.PENDING_STATUS)
                {
                    entity.Status = GlobalConstants.DENIED_STATUS;
                    entity.ConfirmedBy = dto.ConfirmedBy;
                    entity.ConfirmedDate = Tools.GetUTC();
                    await _unitOfWork.TutorRepository.Update(entity);
                    await _unitOfWork.Commit();
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> Active(TutorDto dto)
        {
            var entity = await _unitOfWork.TutorRepository.GetById(dto.Id);
            if(entity != null)
            {
                if (entity.Status == GlobalConstants.INACTIVE_STATUS)
                {
                    entity.Status = GlobalConstants.ACTIVE_STATUS;
                    entity.ConfirmedBy = dto.ConfirmedBy;
                    entity.ConfirmedDate = Tools.GetUTC();
                    await _unitOfWork.TutorRepository.Update(entity);
                    await _unitOfWork.Commit();
                    return true;
                }
                else return false;
            }
            return false;
        }

        public async Task<bool> UpdateProfile(TutorDto dto)
        {
            var tmpTutor = await _unitOfWork.TutorUpdateProfileRepository.GetByEmail(dto.Email);
            if(tmpTutor != null)
            {
                var entity = _mapper.Map<Tutor>(dto);
                await _unitOfWork.TutorRepository.Update(entity);
                await _unitOfWork.TutorUpdateProfileRepository.Delete(tmpTutor);
                await _unitOfWork.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
