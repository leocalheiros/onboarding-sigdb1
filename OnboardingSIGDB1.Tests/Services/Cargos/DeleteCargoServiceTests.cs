using Bogus;
using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.DeleteCargoById;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Tests.Services.Cargos;

public class DeleteCargoServiceTests
{
    private readonly Mock<ICargoRepository> _mockCargoRepository;
    private readonly Mock<NotificationContext> _mockNotificationContext;
    private readonly Faker _faker;
    private readonly DeleteCargoByIdService _service;

    public DeleteCargoServiceTests()
    {
        _mockCargoRepository = new Mock<ICargoRepository>();
        _mockNotificationContext = new Mock<NotificationContext>();
        _faker = new Faker();
        _service = new DeleteCargoByIdService(_mockCargoRepository.Object, _mockNotificationContext.Object);
    }
    
    [Fact]
        public async Task ChecaValidacoesDeleteCargo_CargoInexistente_DeveAdicionarNotificacao()
        {
            var nonExistingId = _faker.Random.Long();
            var cancellationToken = new CancellationToken();

            _mockCargoRepository.Setup(repo => repo.GetByAsync(nonExistingId, cancellationToken))
                .ReturnsAsync((Cargo)null);
            
            var cargo = await _service.ChecaValidacoesDeleteCargo(nonExistingId, cancellationToken);
            
            Assert.Null(cargo);
            _mockNotificationContext.Verify(context => context.AddNotification(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ChecaValidacoesDeleteCargo_CargoExistenteSemFuncionarios_DeveRetornarCargo()
        {
            var existingCargoId = _faker.Random.Long();
            var existingCargo = new Cargo(existingCargoId, "Test Cargo");
            var cancellationToken = new CancellationToken();

            _mockCargoRepository.Setup(repo => repo.GetByAsync(existingCargoId, cancellationToken))
                .ReturnsAsync(existingCargo);
            _mockCargoRepository.Setup(repo => repo.HasFuncionariosVinculadosAsync(existingCargoId, cancellationToken))
                .ReturnsAsync(false);
            
            var cargo = await _service.ChecaValidacoesDeleteCargo(existingCargoId, cancellationToken);
            
            Assert.NotNull(cargo);
            Assert.Equal(existingCargo, cargo);
            _mockNotificationContext.Verify(context => context.AddNotification(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ChecaValidacoesDeleteCargo_CargoExistenteComFuncionarios_DeveAdicionarNotificacao()
        {
            var existingCargoId = _faker.Random.Long();
            var existingCargo = new Cargo(existingCargoId, "Test Cargo");
            var cancellationToken = new CancellationToken();

            _mockCargoRepository.Setup(repo => repo.GetByAsync(existingCargoId, cancellationToken))
                .ReturnsAsync(existingCargo);
            _mockCargoRepository.Setup(repo => repo.HasFuncionariosVinculadosAsync(existingCargoId, cancellationToken))
                .ReturnsAsync(true);
            
            var cargo = await _service.ChecaValidacoesDeleteCargo(existingCargoId, cancellationToken);
            
            Assert.Null(cargo);
            _mockNotificationContext.Verify(context => context.AddNotification(It.IsAny<string>()), Times.Once);
        }
    }