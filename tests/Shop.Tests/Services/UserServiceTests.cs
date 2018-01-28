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
            userRepositoryMock.Verify(x => x.Get(user.Email), Times.Once);
        }

        [Fact]
        public void login_should_fail_for_invalid_password()
        {
            //Arrange
            var user = fixture.Create<User>();
            userRepositoryMock.Setup(x => x.Get(user.Email)).Returns(user);
            IUserService userService = new UserService(userRepositoryMock.Object,
                mapperMock.Object);

            //Act & Assert
            Action login = () => userService.Login(user.Email, "test");
            var exceptionAssertion = login.ShouldThrow<Exception>();
            exceptionAssertion.And.Message.Should().BeEquivalentTo("Invalid password.");
            userRepositoryMock.Verify(x => x.Get(user.Email), Times.Once);
        }

        [Fact]
        public void register_should_fail_for_not_unique_email()
        {
            //Arrange
            var user = fixture.Create<User>();
            userRepositoryMock.Setup(x => x.Get(user.Email)).Returns(user);
            IUserService userService = new UserService(userRepositoryMock.Object,
                mapperMock.Object);

            //Act & Assert
            Action register = () => userService.Register(user.Email, user.Password, RoleDto.User);
            var exceptionAssertion = register.ShouldThrow<Exception>();
            exceptionAssertion.And.Message.Should().BeEquivalentTo($"User '{user.Email}' already exists.");
            userRepositoryMock.Verify(x => x.Get(user.Email), Times.Once);
        }

        [Fact]
        public void register_should_succeed()
        {
            //Arrange
            IUserService userService = new UserService(userRepositoryMock.Object,
                mapperMock.Object);

            //Act
            userService.Register("test@test.com", "test", RoleDto.User);

            //Assert
            userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        }
    }
}
