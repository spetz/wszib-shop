using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Repositories;
using Shop.Core.Services;
using System;
using Xunit;

namespace Shop.Tests.Services
{
    public class UserServiceTests
    {
        Mock<IUserRepository> userRepositoryMock;
        Mock<IMapper> mapperMock;
        Fixture fixture;

        public UserServiceTests()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            mapperMock = new Mock<IMapper>();
            fixture = new Fixture();
        }

        [Fact]
        public void get_should_return_user()
        {
            //Arrange
            var user = fixture.Create<User>();
            var userDto = fixture.Create<UserDto>();
            userRepositoryMock.Setup(x => x.Get(user.Email)).Returns(user);
            mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(userDto);
            IUserService userService = new UserService(userRepositoryMock.Object,
                mapperMock.Object);

            //Act
            var expectedUserDto = userService.Get(user.Email);

            //Assert
            expectedUserDto.Should().NotBeNull();
            userRepositoryMock.Verify(x => x.Get(user.Email), Times.Once);
            mapperMock.Verify(x => x.Map<UserDto>(user), Times.Once);
        }

        [Fact]
        public void login_should_fail_for_non_existing_user()
        {
            //Arrange
            var user = fixture.Create<User>();
            IUserService userService = new UserService(userRepositoryMock.Object,
                mapperMock.Object);

            //Act & Assert
            Action login = () => userService.Login(user.Email, user.Password);
            var exceptionAssertion = login.ShouldThrow<Exception>();
            exceptionAssertion.And.Message.Should().BeEquivalentTo($"User '{user.Email}' was not found.");
        }
    }
}
