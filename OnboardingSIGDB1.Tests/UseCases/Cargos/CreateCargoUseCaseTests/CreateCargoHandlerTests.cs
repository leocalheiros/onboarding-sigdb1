using AutoMapper;
using Bogus;
using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.CreateCargo;
using OnboardingSIGDB1.Application.UseCases.Cargo;
using OnboardingSIGDB1.Application.UseCases.Cargo.CreateCargo;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.UseCases.Cargos.CreateCargoUseCaseTests;

public class CreateCargoHandlerTests
{
        private readonly Faker _faker;
        private readonly CargoBuilder _builder;
        private readonly Mock<ICargoRepository> _cargoRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICreateCargoService> _createCargoServiceMock;
        private readonly Mock<NotificationContext> _notificationContextMock;
        private readonly CreateCargoHandler _handler;
        
        public CreateCargoHandlerTests()
        {
            _faker = new Faker();
            _builder = new CargoBuilder();
            
            _cargoRepositoryMock = new Mock<ICargoRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _createCargoServiceMock = new Mock<ICreateCargoService>();
            _notificationContextMock = new Mock<NotificationContext>();

            _handler = new CreateCargoHandler(
                _unitOfWorkMock.Object,
                _cargoRepositoryMock.Object,
                _mapperMock.Object,
                _createCargoServiceMock.Object,
                _notificationContextMock.Object);
        }

        [Fact]
        public async Task Handle_QuandoCargoTiverNotificacoes_NaoDeveCriarCargo()
        {
            var command = new CreateCargoCommand { Descricao = _faker.Commerce.ProductName() };
            var cancellationToken = new CancellationToken();

            _notificationContextMock.Setup(not => not.HasNotifications()).Returns(true);
            
            await _handler.Handle(command, cancellationToken);
            
            _cargoRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Cargo>(), cancellationToken),
                Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(cancellationToken), Times.Never);
        }

        [Fact]
        public async Task Handle_QuandoCargoNaoTiverNotificacoes_DeveCriarCargo()
        {
            var command = new CreateCargoCommand { Descricao = _faker.Commerce.ProductName() };
            var cancellationToken = new CancellationToken();

            _createCargoServiceMock.Setup(service => service.ChecaValidacoesCargo(command, cancellationToken));
            _notificationContextMock.Setup(not => not.HasNotifications()).Returns(false);

            var createdCargo = _builder.ComId(1)
                .ComDescricao(command.Descricao)
                .Build();
            
            _mapperMock.Setup(mapper => mapper.Map<Domain.Entities.Cargo>(command))
                .Returns(createdCargo);
            
            await _handler.Handle(command, cancellationToken);
            
            _cargoRepositoryMock.Verify(repo => repo.CreateAsync(createdCargo, cancellationToken), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(cancellationToken), Times.Once);
        }
    }