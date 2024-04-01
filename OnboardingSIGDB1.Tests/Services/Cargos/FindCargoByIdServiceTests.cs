using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.FindCargoById;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Services.Cargos;

public class FindCargoByIdServiceTests
{
    private readonly Mock<ICargoRepository> _cargoRepositoryMock;
    private readonly Mock<NotificationContext> _notificationContextMock;
    private readonly FindCargoByIdService _service;

    public FindCargoByIdServiceTests()
    {
        _cargoRepositoryMock = new Mock<ICargoRepository>();
        _notificationContextMock = new Mock<NotificationContext>();
        _service = new FindCargoByIdService(_cargoRepositoryMock.Object, _notificationContextMock.Object);
    }

    [Fact]
    public async Task ValidaCargoExistente_CargoExistente_DeveRetornarCargo()
    {
        var cargoExistente = new CargoBuilder().ComId(1).ComDescricao("Desenvolvedor").Build();
        
        _cargoRepositoryMock.Setup(repo => repo.GetByAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cargoExistente);
        
        var cargoRetornado = await _service.ValidaCargoExistente(1, CancellationToken.None);
        
        Assert.Equal(cargoExistente, cargoRetornado);
        _cargoRepositoryMock.Verify(repo => repo.GetByAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        _notificationContextMock.Verify(not => not.AddNotification(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ValidaCargoExistente_CargoInexistente_DeveLancarNotificacaoENaoRetornarCargo()
    {
        _cargoRepositoryMock.Setup(repo => repo.GetByAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Cargo)null);
        
        var cargoRetornado = await _service.ValidaCargoExistente(1, CancellationToken.None);
        
        Assert.Null(cargoRetornado);
        _cargoRepositoryMock.Verify(repo => repo.GetByAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        _notificationContextMock.Verify(not => not.AddNotification("Cargo não existente!"), Times.Once);
    }
}