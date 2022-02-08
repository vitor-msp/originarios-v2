namespace Originarios.Models
{
    public partial class Contato
    {
        public int id_ctt { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string endereco { get; set; }
        public string assunto { get; set; }
        public string mensagem { get; set; }
    }
}
