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
        public static char SELECT_REGISTER = 'R';
        public static char SELECT_LOGIN = 'L';
        public static char SELECT_EXIT = 'X';

        public static char SELECT_CREATE_AD = 'C';
        public static char SELECT_CHECK_ADS = 'D';

        public static char SELECT_PROFILE = 'P';
        public static string SELECT_CHANGE_PROFILE_DATA = "CP";
        public static string CHANGE_FIRST_NAME = "FN";
        public static string CHANGE_LAST_NAME = "LN";
        public static char CHANGE_DEPARTMENT = 'D';
        public static string CHANGE_PREFERRED_LANGUAGE = "PL";
        public static char CHANGE_LOCATION = 'L';
        public static char CHANGE_CONTACT = 'C';

        public static string SELECT_CHECK_MY_OWN_ADS = "CM";
        public static string SELECT_CHANGE_AD = "CA";
        public static char CHANGE_AD = 'C';
        public static string CHANGE_AD_TYPE = "AP";
        public static string CHANGE_ROUTE = "CR";
        public static string CHANGE_DATE_TIME = "CT";
        public static string CHANGE_NUMBER_OF_SEATS = "CN";
        public static string CHANGE_SPECIFIC_REQUESTS = "CQ";
        public static char DELETE_AD = 'D';


        public static string FILE_PATH_USERDATA = @"..\..\UserData.txt";
        public static string FILE_PATH_AD = @"..\..\Ads.txt";
        public static string COMPANY_NAME = "company";
    }
}
