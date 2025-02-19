using CalculadoraDeCotacoes.Application.Models.InputModels;
using CalculadoraDeCotacoes.Domain.Entities;
using CalculadoraDeCotacoes.Persistence.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraDeCotacoes.Application.Beneficiarios.AlterarBeneficiario;

public class AlterarBeneficiarioCommandHandler(
    ApplicationDbContext context,
    IValidator<AlterarBeneficiarioCommand> alterarBeneficiarioValidator,
    IValidator<BeneficiarioInputModel> beneficiarioValidator)
    : IRequestHandler<AlterarBeneficiarioCommand, AlterarBeneficiarioResult>
{
    public async Task<AlterarBeneficiarioResult> Handle(AlterarBeneficiarioCommand request,
        CancellationToken cancellationToken)
    {
        var alterarBeneficiarioValidationResult =
            await alterarBeneficiarioValidator.ValidateAsync(request, cancellationToken);

        if (!alterarBeneficiarioValidationResult.IsValid)
            throw new ValidationException(alterarBeneficiarioValidationResult.Errors);

        foreach (var beneficiario in request.Beneficiarios!)
        {
            var beneficiarioValidationResult =
                await beneficiarioValidator.ValidateAsync(beneficiario, cancellationToken);

            if (!beneficiarioValidationResult.IsValid)
                throw new ValidationException(beneficiarioValidationResult.Errors);
        }

        var beneficiarios = await context.CotacoesBeneficiarios.Where(cb => cb.IdCotacao == request.IdCotacao)
            .ToListAsync(cancellationToken);

        var beneficiariosRequisicao = request.Beneficiarios!.Adapt<IEnumerable<CotacaoBeneficiario>>().ToList();
        var novosBeneficiarios =
            beneficiariosRequisicao.ExceptBy(beneficiarios.Select(b => b.Id), br => br.Id).ToList();
        var beneficiariosRemovidos =
            beneficiarios.ExceptBy(beneficiariosRequisicao.Select(br => br.Id), b => b.Id).ToList();
        var beneficiariosSupostamenteAlterados =
            beneficiarios.IntersectBy(beneficiariosRequisicao.Select(br => br.Id), b => b.Id).ToList();

        var beneficiariosAlterados = beneficiarios
            .Join(beneficiariosSupostamenteAlterados,
                original => original.Id,
                updated => updated.Id,
                (original, updated) => new { Original = original, Updated = updated })
            .Where(pair => pair.Original.Equals(pair.Updated))
            .Select(pair => pair.Updated)
            .ToList();

        var porcentagemNovosMaisAlterados =
            novosBeneficiarios.Sum(b => b.Percentual) + beneficiariosAlterados.Sum(b => b.Percentual);
        var porcentagemRemovidos = beneficiariosRemovidos.Sum(b => b.Percentual);

        if (porcentagemNovosMaisAlterados - porcentagemRemovidos != 100)
            throw new ValidationException("A soma total das porcentagens dos beneficiários deve ser 100.");

        await context.AddAsync(novosBeneficiarios, cancellationToken);
        context.RemoveRange(beneficiariosRemovidos);

        foreach (var beneficiario in beneficiariosAlterados)
            context.Entry(beneficiario).State = EntityState.Modified;

        var sucesso = await context.SaveChangesAsync(cancellationToken) > 0;

        return new AlterarBeneficiarioResult(sucesso);
    }
}