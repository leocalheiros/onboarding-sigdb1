using AutoMapper;
using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.FindCargoById;
using OnboardingSIGDB1.Application.UseCases.Cargo.FindCargoById;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.UseCases.Cargos.FindCargoByIdUseCaseTests;

public class FindCargoByIdHandlerTests
{
    private readonly Mock<IFindCargoByIdService> _findCargoByIdServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<NotificationContext> _notificationContextMock;
    private readonly FindCargoByIdHandler _handler;

    public FindCargoByIdHandlerTests()
    {
        _findCargoByIdServiceMock = new Mock<IFindCargoByIdService>();
        _mapperMock = new Mock<IMapper>();
        _notificationContextMock = new Mock<NotificationContext>();
        _handler = new FindCargoByIdHandler(_findCargoByIdServiceMock.Object, _mapperMock.Object,
            _notificationContextMock.Object);
    }

    [Fact]
    public async Task Handle_CargoExistente_DeveRetornarCargoMapeado()
    {
        var cargoExistente = new CargoBuilder().ComId(1).ComDescricao("Desenvolvedor").Build();
        
        _findCargoByIdServiceMock.Setup(s => s.ValidaCargoExistente(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cargoExistente);
        
        _mapperMock.Setup(m => m.Map<FindCargoByIdResult>(cargoExistente))
            .Returns(new FindCargoByIdResult { Id = 1, Descricao = "Desenvolvedor" }); 
        
        var resultado = await _handler.Handle(new FindCargoByIdQuery { Id = 1 }, CancellationToken.None);
        
        Assert.Equal(1, resultado.Id);
        Assert.Equal("Desenvolvedor", resultado.Descricao); 
        _findCargoByIdServiceMock.Verify(s => s.ValidaCargoExistente(1, It.IsAny<CancellationToken>()), Times.Once);
        _notificationContextMock.Verify(not => not.HasNotifications(), Times.Once);
    }

    [Fact]
    public async Task Handle_CargoInexistente_DeveRetornarObjetoVazio()
    {
        _findCargoByIdServiceMock.Setup(s => s.ValidaCargoExistente(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Cargo)null);
        
        var resultado = await _handler.Handle(new FindCargoByIdQuery { Id = 1 }, CancellationToken.None);

        Assert.Null(resultado);
        _findCargoByIdServiceMock.Verify(s => s.ValidaCargoExistente(1, It.IsAny<CancellationToken>()), Times.Once);
        _notificationContextMock.Verify(not => not.HasNotifications(), Times.Once);
    }
}