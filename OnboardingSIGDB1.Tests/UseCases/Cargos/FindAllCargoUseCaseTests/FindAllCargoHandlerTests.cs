using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.FindAllCargo;
using OnboardingSIGDB1.Application.UseCases.Cargo.FindAllCargo;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.UseCases.Cargos.FindAllCargoUseCaseTests;

public class FindAllCargoHandlerTests
{
    private readonly Mock<IFindAllCargoService> _findAllCargoServiceMock;
    private readonly FindAllCargoHandler _handler;

    public FindAllCargoHandlerTests()
    {
        _findAllCargoServiceMock = new Mock<IFindAllCargoService>();
        _handler = new FindAllCargoHandler(_findAllCargoServiceMock.Object);
    }

    [Fact]
    public async Task Handle_DeveChamarFindAllCargoService_ERetornarResultado()
    {
        var cargos = new List<Cargo>
        {
            new CargoBuilder().ComId(1).ComDescricao("Desenvolvedor").Build(),
            new CargoBuilder().ComId(2).ComDescricao("Analista de Sistemas").Build()
        };
        
        _findAllCargoServiceMock.Setup(s => s.RetornaCargosExistentes(It.IsAny<CancellationToken>()))
            .ReturnsAsync(cargos);
        
        var listaCargos = await _handler.Handle(new FindAllCargoQuery(), CancellationToken.None);
        
        _findAllCargoServiceMock.Verify(s => s.RetornaCargosExistentes(It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.Equal(cargos, listaCargos);
    }
}
