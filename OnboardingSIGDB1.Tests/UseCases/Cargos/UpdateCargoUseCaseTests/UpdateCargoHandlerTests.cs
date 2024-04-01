using AutoMapper;
using Bogus;
using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.CreateCargo;
using OnboardingSIGDB1.Application.Services.Cargo.UpdateCargoById;
using OnboardingSIGDB1.Application.UseCases.Cargo.CreateCargo;
using OnboardingSIGDB1.Application.UseCases.Cargo.UpdateCargoById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.UseCases.Cargos.UpdateCargoUseCaseTests;

public class UpdateCargoHandlerTests
{
    private readonly Faker _faker;
    private readonly CargoBuilder _builder;
    private readonly Mock<ICargoRepository> _cargoRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUpdateCargoByIdService> _updateCargoServiceMock;
    private readonly Mock<NotificationContext> _notificationContextMock;
    private readonly UpdateCargoByIdHandler _handler;
        
    public UpdateCargoHandlerTests()
    {
        _faker = new Faker();
        _builder = new CargoBuilder();
            
        _cargoRepositoryMock = new Mock<ICargoRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _updateCargoServiceMock = new Mock<IUpdateCargoByIdService>();
        _notificationContextMock = new Mock<NotificationContext>();

        _handler = new UpdateCargoByIdHandler(
            _unitOfWorkMock.Object,
            _cargoRepositoryMock.Object,
            _mapperMock.Object,
            _updateCargoServiceMock.Object,
            _notificationContextMock.Object);
    }
    
    [Fact]
    public async Task Handle_QuandoCargoTiverNotificacoes_NaoDeveAtualizarCargo()
    {
        var command = new UpdateCargoByIdCommand() { Descricao = _faker.Random.Word() };
        var cancellationToken = new CancellationToken();

        _notificationContextMock.Setup(not => not.HasNotifications()).Returns(true);
            
        await _handler.Handle(command, cancellationToken);
        
        _unitOfWorkMock.Verify(uow => uow.Commit(cancellationToken), Times.Never);
    }
    
    [Fact]
    public async Task Handle_QuandoCargoNaoTiverNotificacoes_DeveAtualizarCargo()
    {
        var cargoOriginal = _builder.ComDescricao("Descrição Antiga").Build();
        var novoCargo = _builder.ComDescricao("Descrição Nova").Build();

        var command = new UpdateCargoByIdCommand
        { 
            Id = cargoOriginal.Id, 
            Descricao = novoCargo.Descricao    
        }; 
        var cancellationToken = new CancellationToken();
        
        _updateCargoServiceMock
            .Setup(service => service.ChecaValidacoesUpdate(command.Id, command, cancellationToken))
            .ReturnsAsync(novoCargo); 

        _notificationContextMock.Setup(not => not.HasNotifications()).Returns(false);
        
        await _handler.Handle(command, cancellationToken);
        
        _unitOfWorkMock.Verify(uow => uow.Commit(cancellationToken), Times.Once); 
    }
}