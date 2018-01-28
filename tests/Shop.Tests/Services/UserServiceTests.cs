using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Repositories;
using Shop.Core.Services;
using Xunit;

namespace Shop.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void get_should_return_user()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var fixture = new Fixture();
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
    }
}
