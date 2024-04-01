using Moq;
using OnboardingSIGDB1.Application.Services.Cargo.UpdateCargoById;
using OnboardingSIGDB1.Application.UseCases.Cargo.UpdateCargoById;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Services.Cargos;

public class UpdateCargoByIdServiceTests
{
    private readonly Mock<ICargoRepository> _mockCargoRepository;
    private readonly Mock<NotificationContext> _mockNotificationContext;
    private readonly UpdateCargoByIdService _service;
    private readonly CargoBuilder _builder;

    public UpdateCargoByIdServiceTests()
    {
        _mockCargoRepository = new Mock<ICargoRepository>();
        _mockNotificationContext = new Mock<NotificationContext>();
        _service = new UpdateCargoByIdService(_mockCargoRepository.Object, _mockNotificationContext.Object);
        _builder = new CargoBuilder();
    }

    [Fact]
    public async Task ChecaValidacoesUpdate_CargoNaoExistente_DeveAdicionarNotificacao()
    {
        var cargoId = 1L; 
        var command = new UpdateCargoByIdCommand { Descricao = "Descrição Nova" };
        var cancellationToken = new CancellationToken();
        
        _mockCargoRepository.Setup(r => r.GetByAsync(It.IsAny<long>(), cancellationToken))
            .ReturnsAsync((Domain.Entities.Cargo)null);

        await _service.ChecaValidacoesUpdate(cargoId, command, cancellationToken);

        _mockNotificationContext.Verify(c => c.AddNotification("Cargo com ID não encotrado/inexistente!"), Times.Once);
    }

    [Fact]
    public async Task ChecaValidacoesUpdate_DescricaoExistente_DeveAdicionarNotificacao()
    {
        var cargoId = 1L; 
        var descricaoExistente = "Descrição Existente"; 
        var command = new UpdateCargoByIdCommand { Descricao = descricaoExistente }; 
        var cancellationToken = new CancellationToken();
        
        _mockCargoRepository.Setup(r => r.GetByAsync(cargoId, cancellationToken))
            .ReturnsAsync(_builder.ComId(cargoId).ComDescricao(descricaoExistente).Build());
        
        _mockCargoRepository.Setup(r => r.GetByDescricaoAsync(descricaoExistente, cancellationToken))
            .ReturnsAsync(_builder.ComId(2L).ComDescricao(descricaoExistente).Build());

        await _service.ChecaValidacoesUpdate(cargoId, command, cancellationToken);

        _mockNotificationContext.Verify(context => context.AddNotification("Descrição de cargo já existente!"), Times.Once);
    }

    [Fact]
    public async Task ChecaValidacoesUpdate_ComDescricaoValida_DeveAtualizarCargoESemNotificacoes()
    {
        var cargoId = 1L;
        var novaDescricao = "Descrição Atualizada";
        var cancellationToken = new CancellationToken();
        
        _mockCargoRepository.Setup(r => r.GetByAsync(cargoId, cancellationToken))
            .ReturnsAsync(_builder.ComId(cargoId).ComDescricao("Descrição Original").Build());
        
        _mockCargoRepository.Setup(r => r.GetByDescricaoAsync(novaDescricao, cancellationToken))
            .ReturnsAsync((Domain.Entities.Cargo)null);

        var updatedCargo = await _service.ChecaValidacoesUpdate(cargoId, new UpdateCargoByIdCommand { Descricao = novaDescricao }, cancellationToken);

        Assert.Equal(novaDescricao, updatedCargo.Descricao);
        _mockNotificationContext.Verify(c => c.AddNotification(It.IsAny<string>()), Times.Never);
    }
}