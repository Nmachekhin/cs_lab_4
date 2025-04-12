using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDisplay
{
    internal static class ChineeseZodiakSign
    {
        private static string[] s_Signs = 
            {
            "Monkey",
            "Rooster",
            "Dog",
            "Pig",
            "Rat", 
            "Ox", 
            "Tiger", 
            "Rabbit", 
            "Dragon", 
            "Snake", 
            "Horse", 
            "Goat", 

        };
        private static string[] s_elements = {
            "Metal",
            "Water",
            "Wood", 
            "Fire", 
            "Earth" 
        };


        public static async Task<string> GetSign(DateTime birthday)
        {
            int birthdayYear = birthday.Year;
            await Task.Delay(20);
            string resString = new String(s_elements[((birthdayYear%10)/2)%5]);
            resString += " "+ new String(s_Signs[birthdayYear % 12]);
            return resString;
        }
        

    }
}
