namespace Originarios.Models
{
    using System;
    
    public partial class Postagem
    {
        public int id_post { get; set; }
        public int usuario { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string corpo { get; set; }
        public string nm_img1 { get; set; }
        public byte[] vb_img1 { get; set; }
        public string nm_img2 { get; set; }
        public byte[] vb_img2 { get; set; }
        public string nm_img3 { get; set; }
        public byte[] vb_img3 { get; set; }
        public string nm_img4 { get; set; }
        public byte[] vb_img4 { get; set; }
        
        public string base64_img1 { get; set; }
        public string base64_img2 { get; set; }
        public string base64_img3 { get; set; }
        public string base64_img4 { get; set; }

        public Nullable<decimal> valor { get; set; }
    
        public virtual Usuario Usuario1 { get; set; }
    }
}
