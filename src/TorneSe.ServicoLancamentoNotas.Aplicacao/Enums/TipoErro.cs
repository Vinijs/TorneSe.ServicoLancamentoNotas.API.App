﻿using System.ComponentModel;
using TorneSe.ServicoLancamentoNotas.Aplicacao.Constantes;

namespace TorneSe.ServicoLancamentoNotas.Aplicacao.Enums;

public enum TipoErro
{
    [Description(ConstantesAplicacao.MensagensErro.NOTA_INVALIDA)]
    NotaInvalida = 101,

    [Description(ConstantesAplicacao.MensagensErro.INPUT_NOTA_INVALIDO)]
    InputNotaInvalido = 201,

    [Description(ConstantesAplicacao.MensagensErro.NAO_FOI_POSSIVEL_VALIDAR_VINCULOS_CURSO)]
    NaoFoiPossivelValidarVinculosCursos = 301,

    [Description(ConstantesAplicacao.MensagensErro.RECURSO_NAO_ENCONTRADO)]
    RecursoNaoEncontrado = 400,

    [Description(ConstantesAplicacao.MensagensErro.ERRO_INESPERADO)]
    ErroInesperado = 500
}
