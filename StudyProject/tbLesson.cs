//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudyProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbLesson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbLesson()
        {
            this.tbGroup = new HashSet<tbGroup>();
        }
    
        public System.Guid idLesson { get; set; }
        public Nullable<System.Guid> id_material { get; set; }
        public System.Guid id_test { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string Name { get; set; }
        public bool ShowMaterial { get; set; }
    
        public virtual tbMaterials tbMaterials { get; set; }
        public virtual tbTest tbTest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbGroup> tbGroup { get; set; }
    }
}
