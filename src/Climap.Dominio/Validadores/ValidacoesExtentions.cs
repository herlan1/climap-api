using System.Linq;
using FluentValidation;

namespace Climap.Dominio.Validadores
{
    public static class ValidacoesExtentions
    {
        public static void ValideParaIncluir<T>(this IValidator<T> validator, T instance, string ruleSet = null)
        {
            var rulesSets = new[]
            {
                "default",
                RegrasDeValidacao.INCLUIR,
                ruleSet
            };

            ruleSet = string.Join(",", rulesSets.Where(c => !string.IsNullOrWhiteSpace(c)));

            validator.ValidateAndThrow(instance, ruleSet);
        }

        public static void ValideParaAtualizar<T>(this IValidator<T> validator, T instance, string ruleSet = null)
        {
            var rulesSets = new[]
            {
                "default",
                RegrasDeValidacao.ATUALIZAR,
                ruleSet
            };

            ruleSet = string.Join(",", rulesSets.Where(c => !string.IsNullOrWhiteSpace(c)));

            validator.ValidateAndThrow(instance, ruleSet);
        }
    }
}
