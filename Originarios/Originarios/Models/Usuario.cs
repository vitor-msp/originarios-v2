namespace Originarios.Models
{
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Postagem = new HashSet<Postagem>();
        }
    
        public int id_usu { get; set; }
        public string nome { get; set; }
        public System.DateTime dt_nasc { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string ddd { get; set; }
        public string whatsapp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Postagem> Postagem { get; set; }
    }
}
