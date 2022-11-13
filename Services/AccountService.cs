using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AccountDto> Get(string email)
        {
            var entity = await _unitOfWork.AccountRepository.Get(email);
            return _mapper.Map<AccountDto>(entity);
        }

        public async Task<IEnumerable<AccountDto>> GetAll()
        {
            var entities = await _unitOfWork.AccountRepository.GetAll();
            return _mapper.Map<IEnumerable<AccountDto>>(entities).ToList();
        }

        public async Task<AccountDto> GetById(int id)
        {
            var entity = await _unitOfWork.AccountRepository.GetById(id);
            return _mapper.Map<AccountDto>(entity);
        }

        public Task Inactive(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(AccountDto dto)
        {
            var entity = _mapper.Map<Account>(dto);
            await _unitOfWork.AccountRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public Task<bool> IsEmailExist(string email)
        {
            return _unitOfWork.AccountRepository.IsEmailExist(email);
        }

        public async Task ResetToken(string email, string token)
        {
            var account = await _unitOfWork.AccountRepository.Get(email);
            account.TokenNotification = token;
            await _unitOfWork.AccountRepository.UpdateAccount(account);
            await _unitOfWork.Commit();
        }

        public async Task Update(AccountDto dto)
        {
            var entity = _mapper.Map<Account>(dto);
            await _unitOfWork.AccountRepository.UpdateAccount(entity);
            await _unitOfWork.Commit();
        }
    }
}
