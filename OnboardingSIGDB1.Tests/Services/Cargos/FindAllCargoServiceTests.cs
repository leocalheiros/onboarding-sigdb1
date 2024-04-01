using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.FindAllCargo;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Services.Cargos;

public class FindAllCargoServiceTests
{
    private readonly Mock<ICargoRepository> _cargoRepositoryMock;
    private readonly FindAllCargoService _service;

    public FindAllCargoServiceTests()
    {
        _cargoRepositoryMock = new Mock<ICargoRepository>();
        _service = new FindAllCargoService(_cargoRepositoryMock.Object);
    }

    [Fact]
    public async Task RetornaCargosExistentes_ComCargosNaBase_DeveRetornarCargos()
    {
        var cargos = new List<Cargo>
        {
            new CargoBuilder().ComId(1).ComDescricao("Desenvolvedor").Build(),
            new CargoBuilder().ComId(2).ComDescricao("Analista de Sistemas").Build()
        };
        
        _cargoRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(cargos);
        
        var cargosExistentes = await _service.RetornaCargosExistentes(CancellationToken.None);
        
        Assert.Equal(cargos, cargosExistentes);
        _cargoRepositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RetornaCargosExistentes_SemCargosNaBase_DeveLancarExcecao()
    {
        _cargoRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((List<Cargo>)null);
        
        var exception = await Assert.ThrowsAsync<Exception>(
            async () => await _service.RetornaCargosExistentes(CancellationToken.None));

        Assert.Equal("Não há cargos existentes!", exception.Message);
        _cargoRepositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}