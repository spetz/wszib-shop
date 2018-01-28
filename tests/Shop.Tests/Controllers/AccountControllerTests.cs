using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shop.Core.DTO;
using Shop.Core.Services;
using Shop.Web.Controllers;
using Shop.Web.Framework;
using Shop.Web.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task login_method_should_succeed_and_return_redirect_to_action()
        {
            //Arrange
            var fixture = new Fixture();
            var viewModel = fixture.Create<LoginViewModel>();
            var userDto = fixture.Create<UserDto>();
            var userServiceMock = new Mock<IUserService>();
            var cartServiceMock = new Mock<ICartService>();
            var authenticatorMock = new Mock<IAuthenticator>();
            userServiceMock.Setup(x => x.Get(viewModel.Email)).Returns(userDto);
            var controller = new AccountController(userServiceMock.Object, cartServiceMock.Object,
                    authenticatorMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            //Act
            var result = await controller.Login(viewModel) as RedirectToActionResult;

            //Assert
            result.Should().NotBeNull();
            result.ActionName = "Index";
            result.ControllerName = "Cart";
            userServiceMock.Verify(x => x.Get(viewModel.Email), Times.Once);
            userServiceMock.Verify(x => x.Login(viewModel.Email, viewModel.Password), Times.Once);
            authenticatorMock.Verify(x => x.SignInAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
            cartServiceMock.Verify(x => x.Create(userDto.Id), Times.Once);
        }
    }
}
