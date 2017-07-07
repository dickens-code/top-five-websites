using Microsoft.VisualStudio.TestTools.UnitTesting;
using Manulife.TopFiveWebsites.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Manulife.TopFiveWebsites.Repository.Interface;
using Manulife.TopFiveWebsites.Entity;

namespace Manulife.TopFiveWebsites.Service.Test
{
    [TestClass()]
    public class LoginServiceTest
    {
        public LoginServiceTest()
        {
            Mapper.Initialize(cfg =>
              cfg.CreateMap<UserProfile, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.password))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.roles)));

            Mapper.Configuration.AssertConfigurationIsValid();
        }

        [TestMethod()]
        public void Login_WithMatchedUserInRepo_ReturnsUserObject()
        {
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            dataStoreRepositoryMock.Setup(x => x.GetEntities<UserProfile>()).Returns(new List<UserProfile>() {
                new UserProfile { userId = "goodmanuser@good.com", password = "hbwnhs13" }
            }.AsQueryable());

            var loginService = new LoginService(dataStoreRepositoryMock.Object);

            var user = loginService.Login("goodmanuser@good.com", "hbwnhs13");

            Assert.AreEqual("goodmanuser@good.com", user.Email);
            Assert.AreEqual("hbwnhs13", user.Password);
        }

        [TestMethod()]
        public void Login_WithUnmatchedPasswordInRepo_ReturnsNull()
        {
            var dataStoreRepository = new Mock<IDataStoreRepository>();
            dataStoreRepository.Setup(x => x.GetEntities<UserProfile>()).Returns(new List<UserProfile>() {
                new UserProfile { userId = "goodmanuser@good.com", password = "somepassword" }
            }.AsQueryable());

            var loginService = new LoginService(dataStoreRepository.Object);

            var user = loginService.Login("goodmanuser@good.com", "hbwnhs13");

            Assert.IsNull(user);
        }

        [TestMethod()]
        public void Login_WithNoUserInRepo_ReturnsNull()
        {
            var dataStoreRepository = new Mock<IDataStoreRepository>();
            dataStoreRepository.Setup(x => x.GetEntities<UserProfile>()).Returns(new List<UserProfile>().AsQueryable());

            var loginService = new LoginService(dataStoreRepository.Object);

            var user = loginService.Login("goodmanuser@good.com", "hbwnhs13");

            Assert.IsNull(user);
        }
    }
}