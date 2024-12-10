using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB
{
    public class Constants
    {
        public static string SELECT_REGISTER = "R";
        public static string SELECT_LOGIN = "L";
        public static string SELECT_EXIT = "X";

        public static string SELECT_CREATE_AD = "C";
        public static string SELECT_CHECK_ADS = "D";

        public static string SELECT_PROFILE = "P";
        public static string SELECT_CHANGE_PROFILE_DATA = "CP";
        public static string SELECT_CHECK_MY_OWN_ADS = "CM";
        public static string SELECT_CHANGE_AD = "CA";

        public static string CHANGE_FIRST_NAME = "FN";
        public static string CHANGE_LAST_NAME = "LN";
        public static string CHANGE_DEPARTMENT = "D";
        public static string CHANGE_PREFERRED_LANGUAGE = "PL";
        public static string CHANGE_LOCATION = "L";
        public static string CHANGE_CONTACT = "C";


        public static string FILE_PATH_USERDATA = @"..\..\..\..\UserData.txt";
        public static string FILE_PATH_AD = @"..\..\..\..\Ads.txt";
        public static string COMPANY_NAME = "company";
    }
}
