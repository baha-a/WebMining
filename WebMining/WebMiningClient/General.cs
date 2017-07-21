using System;

namespace WebMiningClient
{
    public static class General
    {
        //0000014602 ofssobxxxmpdu1sr NONE 72.3.217.228 BM 'Opera' 'Mac' 01:47:53 21-12-2017 'PAGE1' 'PAGE2'

        static Random rand = new Random();
        static string leters = "qwertyuiopasdfghjklzxcvbnm1234567890";
        public static string getCookie(int length = 16)
        {
            string res = "";
            while (length-- > 0)
                res += leters[rand.Next(0, leters.Length)].ToString();
            return res;
        }

        public static string getID()
        {
            return "0000000000";
        }
        public static string getIPAndCountryCode(string country)
        {
            country = country.ToLower();
            if (country == "syria")
                return "0.0.0.0 SY";
            return "0.0.0.0 SY";
        }

        public static string getGender(string g)
        {
            if (g.ToLower() == "male")
                return "MALE";
            if (g.ToLower() == "female")
                return "FMLE";
            return "NONE";
        }
    }
}
