using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BB
{
    public class LogicalCode
    {


        public static XmlSerializer serializer = new XmlSerializer(typeof(List<Ad>));

        public static void SaveAdToFile(List<Ad> Adlist, string path)
        {
            using (FileStream file = File.Create(path))
            {
                serializer.Serialize(file, Adlist);
            }
        }

        public static List<Ad> LoadAdListFromFile(string path)
        {
            List<Ad> UserList = new List<Ad>();
            using (FileStream file = File.OpenRead(path))
            {
                UserList = serializer.Deserialize(file) as List<Ad>;
                return UserList;
            }
        }





        public static XmlSerializer serializer2 = new XmlSerializer(typeof(List<User>));

        public static void SaveUserToFile(List<User> UserList, string path)
        {
            using (FileStream file = File.Create(path))
            {
                serializer2.Serialize(file, UserList);
            }
        }

        public static List<User> LoadUserListFromFile(string path)
        {
            List<User> UserList = new List<User>();
            using (FileStream file = File.OpenRead(path))
            {
                UserList = serializer2.Deserialize(file) as List<User>;
                return UserList;
            }
        }

    }
}
