using AutoMapper;
using Bogus;
using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.DeleteCargoById;
using OnboardingSIGDB1.Application.UseCases.Cargo.DeleteCargoById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.UseCases.Cargos.DeleteCargoUseCaseTests;

public class DeleteCargoHandlerTests
{
    private readonly Faker _faker;
    private readonly Mock<ICargoRepository> _cargoRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IDeleteCargoByIdService> _deleteCargoService;
    private readonly Mock<NotificationContext> _notificationContextMock;
    private readonly DeleteCargoByIdHandler _handler;
    private readonly CargoBuilder _builder;

    public DeleteCargoHandlerTests()
    {
        _faker = new Faker();
        _builder = new CargoBuilder();
        
        _cargoRepositoryMock = new Mock<ICargoRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _deleteCargoService = new Mock<IDeleteCargoByIdService>();
        _notificationContextMock = new Mock<NotificationContext>();

        _handler = new DeleteCargoByIdHandler(
            _unitOfWorkMock.Object,
            _cargoRepositoryMock.Object,
            _mapperMock.Object,
            _deleteCargoService.Object,
            _notificationContextMock.Object);
    }

    [Fact]
    public async Task Handle_QuandoTiverNotificacoes_NaoDeveDeletarCargo()
    {
        var query = new DeleteCargoByIdQuery { Id = _faker.Random.Long() };
        var cancellationToken = new CancellationToken();

        _notificationContextMock.Setup(not => not.HasNotifications()).Returns(true);

        await _handler.Handle(query, cancellationToken);

        _cargoRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Cargo>(), cancellationToken),
            Times.Never);
        _unitOfWorkMock.Verify(uow => uow.Commit(cancellationToken), Times.Never);
    }

    [Fact]
    public async Task Handle_QuandoNaoTiverNotificacoes_DeveDeletarCargo()
    {
        var query = new DeleteCargoByIdQuery { Id = _faker.Random.Long() };
        var cancellationToken = new CancellationToken();

        _notificationContextMock.Setup(not => not.HasNotifications()).Returns(false);
        var cargoToDelete = _builder.ComId(_faker.Random.Long())
            .ComDescricao(_faker.Random.Word())
            .Build();

        _deleteCargoService.Setup(s => s.ChecaValidacoesDeleteCargo
            (query.Id, cancellationToken)).Returns(Task.FromResult(cargoToDelete));

        await _handler.Handle(query, cancellationToken);

        _cargoRepositoryMock.Verify(repo => repo.Delete(cargoToDelete), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.Commit(cancellationToken), Times.Once);
    }
}