﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StudyPlatformEntities : DbContext
    {
        public StudyPlatformEntities()
            : base("name=StudyPlatformEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tbGroup> tbGroup { get; set; }
        public virtual DbSet<tbInstitution> tbInstitution { get; set; }
        public virtual DbSet<tbInvite> tbInvite { get; set; }
        public virtual DbSet<tbLesson> tbLesson { get; set; }
        public virtual DbSet<tbLesson_Group> tbLesson_Group { get; set; }
        public virtual DbSet<tbMaterial_Institution> tbMaterial_Institution { get; set; }
        public virtual DbSet<tbMaterials> tbMaterials { get; set; }
        public virtual DbSet<tbTask> tbTask { get; set; }
        public virtual DbSet<tbTaskResult> tbTaskResult { get; set; }
        public virtual DbSet<tbTaskVariant> tbTaskVariant { get; set; }
        public virtual DbSet<tbTest> tbTest { get; set; }
        public virtual DbSet<tbTestResult> tbTestResult { get; set; }
        public virtual DbSet<tbUser> tbUser { get; set; }
    }
}
