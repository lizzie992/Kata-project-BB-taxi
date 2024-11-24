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

        User newUser = new User();
        public static XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
        public static void SaveToFile(List<User> UserList)
        {
            using (FileStream file = File.OpenWrite(Constants.FILE_PATH))
            {
                serializer.Serialize(file, UserList);
            }
        }

        public static List<User> LoadListFromFile()
        {
            List<User> UserList = new List<User>();
            using (FileStream file = File.OpenRead(Constants.FILE_PATH))
            {
                UserList = serializer.Deserialize(file) as List<User>;
                return UserList;
            }
        }

    }
}
