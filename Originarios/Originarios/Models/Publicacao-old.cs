namespace Originarios.Models
{
    using System;
    
    public partial class PublicacaoOld
    {
        public int id_public { get; set; }
        public int usuario { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }

        public string locali { get; set; }
        public string nm_img1 { get; set; }
        public byte[] vb_img1 { get; set; }
                
        public string base64_img1 { get; set; }
            
        public virtual Usuario Usuario1 { get; set; }
    }
}
