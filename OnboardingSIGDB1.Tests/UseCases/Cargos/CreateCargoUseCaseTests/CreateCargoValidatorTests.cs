using AutoFixture;
using FluentAssertions;
using OnboardingSIGDB1.Application.UseCases.Cargo;

namespace OnboardingSIGDB1.Tests.UseCases.Cargos.CreateCargoUseCaseTests;

public class CreateCargoValidatorTests
{
    private CreateCargoValidator _validator;
    private Fixture _fixtures;

    public CreateCargoValidatorTests()
    {
        _validator = new CreateCargoValidator();
        _fixtures = new Fixture();
    }

    [Fact]
    public void Validator_QuandoCommandForInvalida_DeveRetornarErro()
    {
        var command = new CreateCargoCommand();

        var resultadoValidacao = _validator.Validate(command);

        resultadoValidacao.IsValid.Should().BeFalse();
        resultadoValidacao.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void Validator_QuandoCommandForValida_DeveRetornarSucesso()
    {
        var command = _fixtures.Build<CreateCargoCommand>().Create();

        var resultadoValidacao = _validator.Validate(command);

        resultadoValidacao.IsValid.Should().BeTrue();
    }
}