using AutoFixture;
using FluentAssertions;
using OnboardingSIGDB1.Application.UseCases.Cargo.UpdateCargoById;

namespace OnboardingSIGDB1.Tests.UseCases.Cargos.UpdateCargoUseCaseTests;

public class UpdateCargoValidatorTests
{
    private UpdateCargoByIdValidator _validator;
    private Fixture _fixture;

    public UpdateCargoValidatorTests()
    {
        _validator = new UpdateCargoByIdValidator();
        _fixture = new Fixture();
    }

    [Fact]
    public void Validator_QuandoCommandForInvalida_DeveRetornarErro()
    {
        var command = new UpdateCargoByIdCommand();

        var resultadoValidacao = _validator.Validate(command);

        resultadoValidacao.IsValid.Should().BeFalse();
        resultadoValidacao.Errors.Should().HaveCount(1);
    }
    
    [Fact]
    public void Validator_QuandoCommandForValida_DeveRetornarSucesso()
    {
        var command = _fixture.Build<UpdateCargoByIdCommand>().Create();

        var resultadoValidacao = _validator.Validate(command);

        resultadoValidacao.IsValid.Should().BeTrue();
    }
}