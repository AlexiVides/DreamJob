//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dream.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Curriculum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curriculum()
        {
            this.Aplicacion = new HashSet<Aplicacion>();
        }
    
        public int idCurriculum { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int edad { get; set; }
        public string genero { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public int dui { get; set; }
        public string licencia { get; set; }
        public string nivelAcademico { get; set; }
        public string historialAcademico { get; set; }
        public string referenciaPers { get; set; }
        public string experienciaLab { get; set; }
        public string descripcion { get; set; }
        public string correoOpc { get; set; }
        public string segundoIdioma { get; set; }
        public byte[] imagen { get; set; }
        public string estado { get; set; }
        public Nullable<int> idUsuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aplicacion> Aplicacion { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
