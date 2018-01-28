using AutoFixture;
using FluentAssertions;
using Shop.Core.Domain;
using Shop.Core.Repositories;
using Xunit;

namespace Shop.Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public void adding_the_user_should_succeed()
        {
            //Arrange
            IUserRepository userRepository = new UserRepository();
            var fixture = new Fixture();
            var user = fixture.Create<User>();

            //Act
            userRepository.Add(user);

            //Assert
            var expectedUserById = userRepository.Get(user.Id);
            var expectedUserByEmail = userRepository.Get(user.Email);

            expectedUserById.Should().NotBeNull();
            expectedUserByEmail.Should().NotBeNull();
            user.ShouldBeEquivalentTo(expectedUserById);
            user.ShouldBeEquivalentTo(expectedUserByEmail);
        }
    }
}
