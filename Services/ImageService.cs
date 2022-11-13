using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImageDto>> GetAll()
        {
            var entities = await _unitOfWork.ImageRepository.GetAll();
            return _mapper.Map<IEnumerable<ImageDto>>(entities).ToList();
        }

        public async Task<ImageDto> GetById(int id)
        {
            var entity = await _unitOfWork.ImageRepository.GetById(id);
            return _mapper.Map<ImageDto>(entity);
        }

        public Task Inactive(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.ImageRepository.Delete(id);
            await _unitOfWork.Commit();
        }

        public async Task Insert(ImageDto dto)
        {
            var entity = _mapper.Map<Image>(dto);
            await _unitOfWork.ImageRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(ImageDto dto)
        {
            var entity = _mapper.Map<Image>(dto);
            await _unitOfWork.ImageRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task<string[]> Get(string ownerEmail, string imageType)
        {
            //var entities = 
                return await _unitOfWork.ImageRepository.Get(ownerEmail, imageType);
            //return _mapper.Map<IEnumerable<ImageDto>>(entities).ToList();
        }

        public async Task DeleteByOwnerEmail(string email)
        {
            await _unitOfWork.ImageRepository.DeleteByOwnerEmail(email);
            await _unitOfWork.Commit();
        }
    }
}
