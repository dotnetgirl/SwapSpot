using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using SwapSpot.DAL.IRepositories;
using SwapSpot.Domain.Entities.Addresses;
using SwapSpot.Domain.Entities.Users;
using SwapSpot.Service.DTOs.Addresses;
using SwapSpot.Service.Exceptions;
using SwapSpot.Service.Services.Addresses;
using Xunit;

namespace TestProjectServices
{
    public class AddressServiceTests : IDisposable
    {
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AddressService _addressService;

        public AddressServiceTests()
        {
            _addressRepositoryMock = new Mock<IAddressRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();

            _addressService = new AddressService(
                _addressRepositoryMock.Object,
                _mapperMock.Object,
                _userRepositoryMock.Object
            );
        }

        [Fact]
        public async Task AddAsync_ValidData_ReturnsAddressDto()
        {
            // Arrange
            var userId = 1;
            var addressForCreationDto = new AddressForCreationDto { Home = "123 Main St", City = "Sample City" };
            var user = new User { Id = userId };
            var mappedAddress = new Address { Home = "123 Main St", City = "Sample City", UserId = userId };
            var expectedResult = new AddressForResultDto { Id = 1, Home = "123 Main St", City = "Sample City" };

            _userRepositoryMock.Setup(repo => repo.SelectAll(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User> { user });

            _addressRepositoryMock.Setup(repo => repo.SelectAll(It.IsAny<Expression<Func<Address, bool>>>()))
                .ReturnsAsync(new List<Address>());

            _mapperMock.Setup(mapper => mapper.Map<Address>(addressForCreationDto))
                .Returns(mappedAddress);

            _addressRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Address>()))
                .ReturnsAsync(mappedAddress);

            _mapperMock.Setup(mapper => mapper.Map<AddressForResultDto>(mappedAddress))
                .Returns(expectedResult);

            // Act
            var result = await _addressService.AddAsync(userId, addressForCreationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.Home, result.Home);
            Assert.Equal(expectedResult.City, result.City);
        }

        [Fact]
        public async Task AddAsync_WhenUserNotFound_ThrowsException()
        {
            // Arrange
            var userId = 1;
            var addressForCreationDto = new AddressForCreationDto { Home = "123 Main St", City = "Sample City" };

            _userRepositoryMock.Setup(repo => repo.SelectAll(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User>());

            // Act & Assert
            await Assert.ThrowsAsync<SwapSpotException>(() => _addressService.AddAsync(userId, addressForCreationDto));
        }

        [Fact]
        public async Task AddAsync_WhenAddressAlreadyExists_ThrowsException()
        {
            // Arrange
            var userId = 1;
            var addressForCreationDto = new AddressForCreationDto { Home = "123 Main St", City = "Sample City" };
            var user = new User { Id = userId };

            _userRepositoryMock.Setup(repo => repo.SelectAll(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User> { user });

            _addressRepositoryMock.Setup(repo => repo.SelectAll(It.IsAny<Expression<Func<Address, bool>>>()))
                .ReturnsAsync(new List<Address> { new Address() });

            // Act & Assert
            await Assert.ThrowsAsync<SwapSpotException>(() => _addressService.AddAsync(userId, addressForCreationDto));
        }

        // Add similar tests for UpdateByIdAsync, DeleteByIdAsync, GetByIdAsync, GetAllAsync

        public void Dispose()
        {
            _addressRepositoryMock.VerifyAll();
            _userRepositoryMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
    }
}
