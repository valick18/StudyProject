using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProject.Models
{
    public class TimeConverter
    {

        public static string getTime(int minutes) {
               
                if (minutes > 0 && minutes < 60)
                {
                    string str = minutes.ToString() + " хв";
                    return str;
                }

                if(minutes >= 60)
                {
                    double hours = minutes / 60;
                    string str = hours.ToString() + " годин";
                    return str;
                }

            return "0";

        }

    }
}