using Bogus;
using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.UpdateCargoById;
using OnboardingSIGDB1.Application.Services.Funcionario.VincularFuncionarioCargoid;
using OnboardingSIGDB1.Application.UseCases.Funcionario.VincularFuncionarioCargoId;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Services.Funcionario;

public class VincularFuncionarioCargoIdServiceTests
{
    private readonly Faker _faker;
    private readonly CargoBuilder _cargoBuilder;
    private readonly FuncionarioBuilder _funcionarioBuilder;

    private readonly Mock<IFuncionarioRepository> _funcionarioRepositoryMock;
    private readonly Mock<IFuncionarioCargoRepository> _funcionarioCargoRepositoryMock;
    private readonly Mock<ICargoRepository> _cargoRepositoryMock;
    private readonly Mock<NotificationContext> _notificationContextMock;

    private readonly IVincularFuncionarioCargoIdService _service;

    public VincularFuncionarioCargoIdServiceTests()
    {
        _faker = new Faker();
        _cargoBuilder = new CargoBuilder();
        _funcionarioBuilder = new FuncionarioBuilder();

        _funcionarioRepositoryMock = new Mock<IFuncionarioRepository>();
        _funcionarioCargoRepositoryMock = new Mock<IFuncionarioCargoRepository>();
        _cargoRepositoryMock = new Mock<ICargoRepository>();
        _notificationContextMock = new Mock<NotificationContext>();

        _service = new VincularFuncionarioCargoIdService(
            _funcionarioRepositoryMock.Object,
            _funcionarioCargoRepositoryMock.Object,
            _cargoRepositoryMock.Object,
            _notificationContextMock.Object
        );
    }

    [Fact]
    public async Task VincularFuncionarioCargo_FuncionarioNaoEncontrado_DeveNotificar()
    {
        var command = new VincularFuncionarioCargoIdCommand { FuncionarioId = 1, CargoId = 1 };
        var cancellationToken = new CancellationToken();

        _funcionarioRepositoryMock.Setup(repo => repo.GetByAsync(command.FuncionarioId, cancellationToken))
            .ReturnsAsync((Domain.Entities.Funcionario)null);

        var result = await _service.VincularFuncionarioCargoIdAsync(command, cancellationToken);

        Assert.Null(result);
        _notificationContextMock.Verify(not => not.AddNotification
            ("Funcionário não encontrado/existente!"), Times.Once);
    }

    [Fact]
    public async Task VincularFuncionarioCargo_CargoNaoEncontrado_DeveNotificar()
    {
        var funcionario = _funcionarioBuilder.Build();
        var command = new VincularFuncionarioCargoIdCommand { FuncionarioId = funcionario.Id, CargoId = 1 };
        var cancellationToken = new CancellationToken();

        _funcionarioRepositoryMock.Setup(repo => repo.GetByAsync(command.FuncionarioId, cancellationToken))
            .ReturnsAsync(funcionario);
        _cargoRepositoryMock.Setup(repo => repo.GetByAsync(command.CargoId, cancellationToken))
            .ReturnsAsync((Domain.Entities.Cargo)null);

        var result = await _service.VincularFuncionarioCargoIdAsync(command, cancellationToken);

        Assert.Null(result);
        _notificationContextMock.Verify(not => not.AddNotification
            ("Cargo não encontrado/existente!"), Times.Once);
    }

    [Fact]
    public async Task VincularFuncionarioCargo_FuncionarioSemEmpresa_DeveNotificar()
    {
        var funcionario = new Domain.Entities.Funcionario("José", "146.312.199-71", DateTimeOffset.Now);

        var cargo = _cargoBuilder.Build();

        var command = new VincularFuncionarioCargoIdCommand { FuncionarioId = funcionario.Id, CargoId = cargo.Id };
        var cancellationToken = new CancellationToken();

        _funcionarioRepositoryMock.Setup(repo => repo.GetByAsync(command.FuncionarioId, cancellationToken))
            .ReturnsAsync(funcionario);

        _cargoRepositoryMock.Setup(repo => repo.GetByAsync(command.CargoId, cancellationToken))
            .ReturnsAsync(cargo);

        var result = await _service.VincularFuncionarioCargoIdAsync(command, cancellationToken);

        Assert.Null(result);
        _notificationContextMock.Verify(not => not.AddNotification("O funcionário não está vinculado a uma empresa!"),
            Times.Once);
    }

    [Fact]
    public async Task VincularFuncionarioCargo_CargoRepetido_DeveNotificar()
    {
        var empresa = new Empresa("Empresa XYZ", "12345678901234", DateTimeOffset.Now);
        var funcionario = new Domain.Entities.Funcionario("José", "146.312.199-71", DateTimeOffset.Now);
        funcionario.AlterarEmpresaId(empresa.Id);
        var cargo = _cargoBuilder.Build();
        var command = new VincularFuncionarioCargoIdCommand { FuncionarioId = funcionario.Id, CargoId = cargo.Id };
        var cancellationToken = new CancellationToken();

        _funcionarioRepositoryMock.Setup(repo => repo.GetByAsync(command.FuncionarioId, cancellationToken))
            .ReturnsAsync(funcionario);

        _cargoRepositoryMock.Setup(repo => repo.GetByAsync(command.CargoId, cancellationToken))
            .ReturnsAsync(cargo);

        _funcionarioCargoRepositoryMock
            .Setup(repo => repo.GetCargoIdsByFuncionarioIdAsync(funcionario.Id, cancellationToken))
            .ReturnsAsync(new List<long> { cargo.Id });

        var result = await _service.VincularFuncionarioCargoIdAsync(command, cancellationToken);

        Assert.Null(result);
        _notificationContextMock.Verify(not => not.AddNotification("O funcionário já possui esse cargo!"), Times.Once);
    }

    [Fact]
    public async Task VincularFuncionarioCargo_Sucesso_DeveRetornarFuncionarioCargo()
    {
        var empresa = new Empresa("Empresa XYZ", "12345678901234", DateTimeOffset.Now);
        var funcionario = new Domain.Entities.Funcionario("José", "146.312.199-71", DateTimeOffset.Now);
        funcionario.AlterarEmpresaId(empresa.Id);
        var cargo = _cargoBuilder.Build();
        var dataVinculo = DateTimeOffset.Now; 
        var command = new VincularFuncionarioCargoIdCommand
        {
            FuncionarioId = funcionario.Id,
            CargoId = cargo.Id,
            DataVinculo = dataVinculo
        };
        var cancellationToken = new CancellationToken();

        _funcionarioRepositoryMock.Setup(repo => repo.GetByAsync(command.FuncionarioId, cancellationToken))
            .ReturnsAsync(funcionario);
        _cargoRepositoryMock.Setup(repo => repo.GetByAsync(command.CargoId, cancellationToken))
            .ReturnsAsync(cargo);
        _funcionarioCargoRepositoryMock
            .Setup(repo => repo.GetCargoIdsByFuncionarioIdAsync(funcionario.Id, cancellationToken))
            .ReturnsAsync(new List<long>()); 
        
        var funcionarioCargoEsperado = new FuncionarioCargo(funcionario.Id, cargo.Id, dataVinculo);
        _funcionarioCargoRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<FuncionarioCargo>(), cancellationToken))
            .ReturnsAsync(funcionarioCargoEsperado);

        var result = await _service.VincularFuncionarioCargoIdAsync(command, cancellationToken);
        
        Assert.NotNull(result);
        Assert.Equal(command.FuncionarioId, result.FuncionarioId);
        Assert.Equal(command.CargoId, result.CargoId);
        Assert.Equal(command.DataVinculo, result.DataVinculo);

        _funcionarioCargoRepositoryMock.Verify(repo => repo.CreateAsync(
                It.Is<FuncionarioCargo>(fc =>
                    fc.FuncionarioId == funcionarioCargoEsperado.FuncionarioId &&
                    fc.CargoId == funcionarioCargoEsperado.CargoId &&
                    fc.DataVinculo == funcionarioCargoEsperado.DataVinculo),
                cancellationToken),
            Times.Once);
    }
}