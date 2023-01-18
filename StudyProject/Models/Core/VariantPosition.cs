using System;

namespace StudyProject.Models.Core
{
    public class VariantPosition
    {
        public int PositionNumber { get; set; }
        public string Variant {get; set; }
        public Guid idTaskVariant { get; set; }
    }
}