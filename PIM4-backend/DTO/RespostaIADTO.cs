public class RespostaIaDTO
{
    public int IdRespostaIA { get; set; }
    public int IdChamado { get; set; }
    public string Mensagem { get; set; }
    public string Modelo { get; set; }
    public DateTime DataGeracao { get; set; }
    public string IdExecucaoN8N { get; set; }
}
