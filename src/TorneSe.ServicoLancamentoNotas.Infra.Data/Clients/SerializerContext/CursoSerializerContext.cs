using System.Text.Json.Serialization;
using ValueObject = TorneSe.ServicoLancamentoNotas.Dominio.ValueObjects;
namespace TorneSe.ServicoLancamentoNotas.Infra.Data.Clients.SerializerContext;

[JsonSerializable(typeof(IEnumerable<ValueObject.Curso>))]
[JsonSerializable(typeof(ValueObject.Curso))]
[JsonSerializable(typeof(ValueObject.Atividade))]
[JsonSerializable(typeof(ValueObject.Aluno))]
[JsonSerializable(typeof(ValueObject.Professor))]
public partial class CursoSerializerContext : JsonSerializerContext
{
}
