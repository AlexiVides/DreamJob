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
    
    public partial class OfertaEmpleo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OfertaEmpleo()
        {
            this.Aplicacion = new HashSet<Aplicacion>();
        }
    
        public int idOfertaEmpleo { get; set; }
        public string nombre { get; set; }
        public int nVacantes { get; set; }
        public string descripcion { get; set; }
        public string requerimientos { get; set; }
        public string funciones { get; set; }
        public Nullable<double> Salario { get; set; }
        public string prestaciones { get; set; }
        public string estado { get; set; }
        public Nullable<int> idCategoria { get; set; }
        public Nullable<int> idCargo { get; set; }
        public Nullable<int> idDatosEmpresa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aplicacion> Aplicacion { get; set; }
        public virtual Cargo Cargo { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual DatosEmpresa DatosEmpresa { get; set; }
    }
}
