//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudyProject.Controllers
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTask()
        {
            this.tbMaterials = new HashSet<tbMaterials>();
            this.tbTest1 = new HashSet<tbTest>();
        }
    
        public System.Guid idTask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public byte[] Picture { get; set; }
        public byte[] Audio { get; set; }
        public byte[] Video { get; set; }
        public Nullable<int> Rate { get; set; }
        public Nullable<System.Guid> id_test { get; set; }
        public Nullable<System.Guid> id_taskResult { get; set; }
    
        public virtual tbTaskResult tbTaskResult { get; set; }
        public virtual tbTest tbTest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbMaterials> tbMaterials { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbTest> tbTest1 { get; set; }
    }
}