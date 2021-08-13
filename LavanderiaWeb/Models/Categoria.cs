using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LavanderiaWeb.Models
{
    [Table("categoria")]
    public class Categoria
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_categoria")]
        [Key]
        public int Id{get; set; }

        [Display(Name="Descripcion")]
        [Column("descripcion_categoria")]
        [Required(ErrorMessage="Por favor, ingrese un producto.")]
        public string Descripcion{get; set;}

        public List<Producto> Productos{get; set;}
        //public ICollection<Producto> Productos{get; set;}
        //public Categoria()
        //{
        //    Productos=new List<Producto>();
        //}
    }
}