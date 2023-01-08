using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProject.Models
{
    public class SelectImage
    {
        private const int Season_Winter = 1, Season_Spring = 2, Season_Summer = 3, Season_Autumn = 4;

        public string SelectedImage;
        public SelectImage() {
            SelectedImage = getNameImg();
        }

        public int SeasonNow() {
            int month = DateTime.Now.Month;
         
            const int WINTER = 3;
            const int SPRING = 6;
            const int SUMMER = 9;
            const int AUTUMN = 12;

            if (month <= WINTER)
            {
                return Season_Winter;
            }
            else if (month <= SPRING)
            {
                return Season_Spring;

            }
            else if (month <= SUMMER)
            {
                return Season_Summer;

            }
            else { 
                return Season_Autumn;
            }
        }

        private string selectImg(int season) {
         
            Random rnd = new Random();
            int selectNum = rnd.Next(0, 3);
           
            string[] winter = { "winter.jpg", "winter1.jpg", "winter2.jpg" };
            string[] spring = { "spring.jpg", "spring1.jpg", "spring2.jpg" };
            string[] summer = { "summer.jpg", "summer1.jpg", "summer2.jpg" };
            string[] autumn = { "winter.jpg", "winter1.jpg", "winter2.jpg" };

            string nameImg = "";

            if (season == Season_Winter)
            {
                nameImg = winter[selectNum];
            }
            else if (season == Season_Spring)
            {
                nameImg = spring[selectNum];
            }
            else if (season == Season_Summer)
            {
                nameImg = summer[selectNum];
            }
            else {
                nameImg = autumn[selectNum];
            }

            return nameImg;
            
        }

        private string getNameImg() {
            switch (SeasonNow()) {
                case Season_Winter:
                    return selectImg(Season_Winter);
                case Season_Spring:
                    return selectImg(Season_Spring);
                case Season_Summer:
                    return selectImg(Season_Summer);
            }
            return selectImg(Season_Autumn);
        }
    }
}