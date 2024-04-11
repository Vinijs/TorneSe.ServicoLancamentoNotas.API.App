using Microsoft.AspNetCore.Mvc;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Comum;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Enums;

namespace TorneSe.ServicoLancamentoNotas.App.Controllers.Base;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected ActionResult RespostaCustomizada<T>(Resultado<T> resultado)
    {
        if (resultado.Sucesso)
            return Ok(resultado);

        if (resultado.Erro is TipoErro.RecursoNaoEncontrado)
            return NotFound(resultado);

        return BadRequest(resultado);
    }
}
