//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApplicationDbContext.Controllers
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbMaterials
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbMaterials()
        {
            this.tbLesson = new HashSet<tbLesson>();
            this.tbTask = new HashSet<tbTask>();
        }
    
        public System.Guid idMaterial { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbLesson> tbLesson { get; set; }
        public virtual tbMaterial_Institution tbMaterial_Institution { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbTask> tbTask { get; set; }
    }
}
