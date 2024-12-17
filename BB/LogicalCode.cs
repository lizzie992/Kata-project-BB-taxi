using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BB
{
    public class LogicalCode
    {



        public static XmlSerializer serializerAd = new XmlSerializer(typeof(List<Ad>));

        public static void SaveAdToFile(List<Ad> Adlist)
        {
            using (FileStream file = File.Create(Constants.FILE_PATH_AD))
            {
                serializerAd.Serialize(file, Adlist);
            }
        }

        public static List<Ad> LoadAdListFromFile()
        {
            List<Ad> UserList = new List<Ad>();
            using (FileStream file = File.OpenRead(Constants.FILE_PATH_AD))
            {
                UserList = serializerAd.Deserialize(file) as List<Ad>;
                return UserList;
            }
        }





        public static XmlSerializer serializerUser = new XmlSerializer(typeof(List<User>));

        public static void SaveUserToFile(List<User> UserList)
        {
            using (FileStream file = File.Create(Constants.FILE_PATH_USERDATA))
            {
                serializerUser.Serialize(file, UserList);
            }
        }

        public static List<User> LoadUserListFromFile()
        {
            List<User> UserList = new List<User>();
            using (FileStream file = File.OpenRead(Constants.FILE_PATH_USERDATA))
            {
                UserList = serializerUser.Deserialize(file) as List<User>;
                return UserList;
            }
        }



        public static bool IsCompanyEmailValid(string companyEmail)
        {
            if (companyEmail.Contains(Constants.COMPANY_NAME))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
