using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProject.Models.Core
{
    public class CheckField
    {

        public static bool checkOnNullOrEmpty(string[] str) { 
            foreach(string s in str)
            {
                if (string.IsNullOrEmpty(s)) {
                    return true;
                }
            }
            return false;
        }

    }
}