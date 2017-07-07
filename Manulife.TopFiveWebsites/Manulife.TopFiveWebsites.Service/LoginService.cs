using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Manulife.TopFiveWebsites.Entity;
using Manulife.TopFiveWebsites.Repository.Interface;

namespace Manulife.TopFiveWebsites.Service
{
    public class LoginService : ILoginService
    {
        private readonly IDataStoreRepository _dataStoreRepository;

        public LoginService(IDataStoreRepository dataStoreRepository)
        {
            _dataStoreRepository = dataStoreRepository;
        }

        public User Login(string userId, string password)
        {
            var profile = _dataStoreRepository.GetEntities<UserProfile>()
                .FirstOrDefault(p => p.userId == userId && p.password == password);

            return Mapper.Map<UserProfile, User>(profile);
        }
    }
}
