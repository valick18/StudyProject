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
    
    public partial class tbTestResult
    {
        public System.Guid idTestResult { get; set; }
        public System.Guid id_test { get; set; }
        public System.Guid id_user { get; set; }
        public int Rate { get; set; }
    
        public virtual tbTest tbTest { get; set; }
        public virtual tbUser tbUser { get; set; }
    }
}
