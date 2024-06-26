﻿using System;
using System.Collections.Generic;
using System.Linq;
using TorneSe.ServicoLancamentoNotas.Dominio.ValueObjects;

namespace TorneSe.ServicoLancamentoNotas.TestesIntegracao.FakeComponents.Fakers;

public static class CursoFake
{
    public static IEnumerable<Curso> ObterCursoAluno(int atividadeId = 1, int alunoId = 1, int professorId = 1)
    {
        string[] nomesCursos = { "Java", "CSharp", "JavaScript", "Rust", "Go" };
        string[] alunos = { "Pedro", "Jo�o", "Ana", "Maria", "Gabriel", "Antonio", "Marcela", "Lucas", "Lais" };
        string[] professores = { "Danilo", "Raphael", "Douglas", "Vinicius", "Bruno" };
        string[] atividades = { "POO", "L�gica de Programa��o", "Estrutura de dados", "Algoritmos", "Trabalhando com cole��es" };

        int contador = 0;

        return nomesCursos.Select((nomeCurso, index) =>
            new Curso
            (
                ++contador,
                nomeCurso,
                true,
                DateTime.Now.AddMonths(-contador),
                DateTime.Now.AddMonths(contador),
                atividades.Select((nomeAtividade, indexAtividade) =>
                new Atividade(atividadeId + indexAtividade, nomeAtividade, true, DateTime.Now.AddMonths(-contador), DateTime.Now.AddMonths(contador), new Professor(professorId + indexAtividade, professores[indexAtividade], true))).ToList(),
                alunos.Select((nome, indexAluno) => new Aluno(alunoId + indexAluno, nome, true)).ToList()
            ))
            .ToArray();
    }
}
