using Bogus;
using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.CreateCargo;
using OnboardingSIGDB1.Application.UseCases.Cargo;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Tests.Services.Cargos;

public class CreateCargoServiceTests
{
    private readonly Mock<ICargoRepository> _mockCargoRepository;
    private readonly Mock<NotificationContext> _mockNotificationContext;
    private readonly Faker _faker;
    private readonly CreateCargoService _service;

    public CreateCargoServiceTests()
    {
        _mockCargoRepository = new Mock<ICargoRepository>();
        _mockNotificationContext = new Mock<NotificationContext>();
        _faker = new Faker();
        _service = new CreateCargoService(_mockCargoRepository.Object, _mockNotificationContext.Object);
    }

    [Fact]
    public async Task ChecaValidacoesCargo_SeTiverDescricaoExistente_DeveAdicionarNotificao()
    {
        var existingCargoDescription = _faker.Random.Word();
        var command = new CreateCargoCommand { Descricao = existingCargoDescription };
        var cancellationToken = new CancellationToken();

        _mockCargoRepository.Setup(repo => repo.GetByDescricaoAsync(existingCargoDescription, cancellationToken))
            .ReturnsAsync(new Cargo(1, existingCargoDescription));
        
        await _service.ChecaValidacoesCargo(command, cancellationToken);
        
        _mockNotificationContext.Verify(context => context.AddNotification(It.IsAny<string>()), Times.Once);
        _mockCargoRepository.Verify(repo => repo.CreateAsync(It.IsAny<Cargo>(), cancellationToken), Times.Never);
    }

    [Fact]
    public async Task ChecaValidacoesCargo_SeNaoTiverDescricaoExistente_NaoDeveAdicionarNotificacao()
    {
        var validDescription = _faker.Random.Word();
        var command = new CreateCargoCommand { Descricao = validDescription };
        var cancellationToken = new CancellationToken();

        _mockCargoRepository.Setup(repo => repo.GetByDescricaoAsync(validDescription, cancellationToken))
            .ReturnsAsync((Cargo)null);
        
        await _service.ChecaValidacoesCargo(command, cancellationToken);
        
        _mockNotificationContext.Verify(context => context.AddNotification(It.IsAny<string>()), Times.Never);
    }
}