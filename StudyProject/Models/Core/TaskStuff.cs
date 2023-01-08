using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProject.Models.Core
{
    public class TaskStuff
    {

        public enum TaskType { 
             SelectCheckBox, Input
        }

        public static string getNameType(TaskType type) {
            switch (type) {
                case TaskType.SelectCheckBox:
                    return "Вибір правильної відповіді";
                case TaskType.Input:
                    return "Ручний ввод";
            }
            return "";
        }

    }
}