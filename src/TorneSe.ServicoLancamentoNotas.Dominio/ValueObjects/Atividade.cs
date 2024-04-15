namespace TorneSe.ServicoLancamentoNotas.Dominio.ValueObjects;

public class Atividade
{
    private object p;
    private string nomeAtividade;
    private bool v;
    private DateTime dateTime1;
    private DateTime dateTime2;
    private Professor professor;

    public Atividade(int id, string nome, bool ativo, DateTime dataInicio, DateTime dataTermino, Professor professor)
    {
        Id = id;
        Nome = nome;
        Ativo = ativo;
        DataInicio = dataInicio;
        DataTermino = dataTermino;
        Professor = professor;
    }

    public Atividade(object p, string nomeAtividade, bool v, DateTime dateTime1, DateTime dateTime2, Professor professor)
    {
        this.p = p;
        this.nomeAtividade = nomeAtividade;
        this.v = v;
        this.dateTime1 = dateTime1;
        this.dateTime2 = dateTime2;
        this.professor = professor;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataTermino { get; set; }
    public Professor Professor { get; set; }
}
