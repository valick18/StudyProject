using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProject.Models.Core
{
    public class TaskAnswer
    {
        public Guid idTask { get; set; }
        public Guid idTest { get; set; }
        public string answer { get; set; }

        public Guid? idTaskVariant { get; set; }

        public int PositionNumber { get; set; }
        public List<SelectedId> SelectedIds {get; set;}

        public List<VariantPosition> variantPosition { get; set; }

    }
}