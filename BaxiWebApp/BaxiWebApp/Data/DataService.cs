using BB;
using GoogleMapsApi.Entities.Geocoding.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.StaticMaps;
using GoogleMapsApi.StaticMaps.Entities;
using System.Globalization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Timers;


namespace BaxiWebApp.Data
{
    public class DataService
    {

        int minDistanceLimit = 3000;
        int maxDistanceLimit = 200000;
        public async Task<bool> IsItTooCloseToBB(int distance)
        {
            if (distance <= minDistanceLimit)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsItTooFarFromBB(int distance)
        {
            if (distance >= maxDistanceLimit)
            {
                return true;
            }
            return false;
        }



        public async Task<int> GetDistance(double? coordinate1, double? coordinate2, double? coordinate3, double? coordinate4)
        {
            if (coordinate1 == null || coordinate2 == null || coordinate3 == null || coordinate4 == null)
            {
                return Constants.NULL_COORDINATES;
            }

            var request = new DirectionsRequest
            {
                Origin = $"{coordinate1?.ToString("F8", CultureInfo.InvariantCulture)}, {coordinate2?.ToString("F8", CultureInfo.InvariantCulture)}",
                Destination = $"{coordinate3?.ToString("F8", CultureInfo.InvariantCulture)}, {coordinate4?.ToString("F8", CultureInfo.InvariantCulture)}",
                TravelMode = TravelMode.Driving,
                ApiKey = "AIzaSyANS3CV3B_21cbYSCLWxTr0gOZpJSPmnvk"
            };
            var result = await GoogleMaps.Directions.QueryAsync(request);
            if (result.Routes.Count() == 0)
            {
                return Constants.INVALID_ROUTE;
            }
            var legs = result.Routes.First().Legs;
            int totalDistanceInMeters = legs.Sum(leg => leg.Distance.Value);
            return totalDistanceInMeters; //output is in METERS!!!
        }




        /// <summary>
        ///
        /// </summary>
        /// <param name="address"></param>
        /// <returns>tuple with lat and long</returns>
        public async Task<(double?, double?)> GetCoordinatesFromAddress(string address)
        {

            if (address == "")
            {
                return (null, null);
            }

            var request = new GeocodingRequest
            {
                ApiKey = "AIzaSyANS3CV3B_21cbYSCLWxTr0gOZpJSPmnvk",
                Address = address
            };

            var result = await GoogleMaps.Geocode.QueryAsync(request);

            Location location = result.Results.FirstOrDefault()?.Geometry?.Location;

            if (location == null)
            {
                return (null, null);
            }

            string coordinates = $"{location.Latitude}, {location.Longitude}";

            return (location.Latitude, location.Longitude);
        }

        IDbContextFactory<BaxiWebAppContext> _dbcFactory;
        public DataService(IDbContextFactory<BaxiWebAppContext> dbcFactory) //dependency injection of the DbContextFactory
        {
            _dbcFactory = dbcFactory;
        }

        public void SendChatMessage(Conversation C, User currentlyLoggedInUser, string TextMessage, int ID)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                Conversation CurrentConversation = context.Conversations.Include(c => c.adOwnerUser).Include(c => c.contactingUser).Include(c => c.messages).FirstOrDefault(conversation => conversation.ID == ID);
                Message message = new Message();
                message.fromUser = context.Users.Find(currentlyLoggedInUser.Id);
                if (currentlyLoggedInUser.Id == CurrentConversation.contactingUser.Id)
                {
                    message.toUser = CurrentConversation.adOwnerUser;
                }
                else if (currentlyLoggedInUser.Id == CurrentConversation.adOwnerUser.Id)
                {
                    message.toUser = CurrentConversation.contactingUser;
                }
                message.messageText = TextMessage;
                message.timeStamp = DateTime.Now;
                //  context.Entry(message.toUser).State = EntityState.Unchanged;
                //    context.Entry(message.fromUser).State = EntityState.Unchanged;
                CurrentConversation.messages.Add(message);
                //        context.Update(CurrentConversation);
                context.SaveChanges();
            }

        }

        public Conversation? GetCurrentConversation(int ID)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                var result = context.Conversations.Include(c => c.adOwnerUser).Include(c => c.contactingUser).Include(c => c.messages).FirstOrDefault(conversation => conversation.ID == ID);
                return result;
            }
        }



        public void DeleteConversation(Conversation C)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                //first delete the messages that have the ID of this current conversation as long as the current conversation exists
                IEnumerable<Message> messagesToDelete = context.Messages.ToList<Message>().AsEnumerable();
                messagesToDelete = messagesToDelete.Where(m => m.ConversationID == C.ID);
                context.Messages.RemoveRange(messagesToDelete);
                //once the messages are removed I can delete the conversation
                context.Conversations.Remove(C);
                context.SaveChanges();
            }
        }


        public void sendEmail(string emailAddress, string subject, string message)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            string passw = Environment.GetEnvironmentVariable("BBtaxi_email_password");
            client.Credentials = new NetworkCredential("baierbrunntaxi@gmail.com", passw);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("baierbrunntaxi@gmail.com");
            mailMessage.To.Add(emailAddress);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            client.Send(mailMessage);
        }

        public void ReportConversation(Conversation C, User currentlyLoggedInUser, string reasonForReporting)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                IEnumerable<Message> messagesToReport = context.Messages.Include(m => m.fromUser).Include(m => m.toUser).ToList<Message>().AsEnumerable();
                messagesToReport = messagesToReport.Where(m => m.ConversationID == C.ID).OrderBy(c => c.timeStamp); ;
                StringBuilder message = new StringBuilder($"Messages: \r\n\r\n");
                foreach (Message m in messagesToReport)
                {
                    message.Append($"From: {showUserNameWithStatus(m.fromUser)}\r\n");
                    message.Append($"To: {showUserNameWithStatus(m.toUser)}\r\n");
                    message.Append($"Message: {m.messageText.ToString()}\r\n");
                    message.Append($"Timestamp: {m.timeStamp.ToString()}\r\n\r\n");
                }
                string output = message.ToString();

                string emailAddress = "gulyaskata99@gmail.com";
                string subject= "A conversation was just reported to you";
                string text = $"Please check out the following conversation below that {currentlyLoggedInUser} reported to you, with the following reason: {reasonForReporting}\r\n{message}";

                sendEmail(emailAddress, subject, text);

            }
        }

        public void reportAd(Ad ad, User currentlyLoggedInUser, string reasonForReporting)
        {
            string adDetails = $"Ad owner: {showUserNameWithStatus(ad.AdOwner)}\r\nAd type: {ad.AdType}\r\nAd Direction: {ad.AdDirection}\r\nAddress: {ad.PickUpDropOffLocation}\r\nPick up date and time: {ad.PickUpDateAndTime}\r\nNumber of seats: {ad.NumberOfSeats}\r\nSpecific requests: {ad.SpecificRequests}\r\n";

            string emailAddress = "gulyaskata99@gmail.com";
            string subject = "An ad was just reported to you";
            string text = $"Please check out the following ad below that {showUserNameWithStatus(currentlyLoggedInUser)} reported to you: \r\n{adDetails}\r\nThe reason for reporting is: {reasonForReporting}\r\nOpen the ad here: LINK";
            sendEmail(emailAddress, subject, text);

        }


        int maxNumberOfWarnings = 3;
        public void giveUserAWarning(User user, string reasonForWarning)
        {

            using (var context = _dbcFactory.CreateDbContext())
            {
                user.NumberOfWarnings++;
                context.Update(user);
                context.SaveChanges();
                string emailAddress = "gulyaskata99@gmail.com";
                string subject = "You received a warning on the Baxi app!";
                string text = $"Dear {showUserNameWithStatus(user)}\r\nYou have received a warning from one of our admins.\r\nReason for the warning is: {reasonForWarning}\r\nYour current number of warnings is: {user.NumberOfWarnings}\r\nPlease be aware that as soon as you reach {maxNumberOfWarnings} warnings your account will be automatically inactivated!";
                sendEmail(emailAddress, subject, text);
                if (user.NumberOfWarnings == maxNumberOfWarnings)
                {
                    string reason = "You have reached 3 warnings!";
                    deactivateUser(user, reason);
                    context.SaveChanges();
                }
            }
        }




        public void deactivateUser(User user, string reasonForDeactivating)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                user.isActive = false;
                context.Update(user);
                context.SaveChanges();
                string text = $"Dear {showUserNameWithStatus(user)}\r\nYour account has been deactivated!\r\nThe reason is: {reasonForDeactivating}\r\nPlease contact the admin team about the reasons behind it and the possible reactivation!";
                string emailAddress = "gulyaskata99@gmail.com";
                string subject = "Your Baxi accout has been deactivated!";
                sendEmail(emailAddress, subject, text);
            }
        }

        public void reactivateUser(User user)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                user.isActive = true;
                user.NumberOfWarnings = 0;
                context.Update(user);
                context.SaveChanges();
                string text = $"Dear {showUserNameWithStatus(user)}\r\nYour account has been reactivated!\r\nYou can now use all functions again.\r\nPlease bring some flowers/chocolate to the admin team to show your gratitude ;)";
                string subject = "Your Baxi accout has been reactivated!";
                string emailAddress = "gulyaskata99@gmail.com";
                sendEmail(emailAddress, subject, text);
            }
        }


        public string showUserNameWithStatus(User user)
        {
            string name = "";
            if (user.isDeleted == true)
            {
                name = "Deleted user";
            }
            if (user.isActive == true)
            {
                name = $"{user.FirstName} {user.LastName}";
            }
            if (user.isActive == false)
            {
                name = $"{user.FirstName} {user.LastName} - INACTIVATED";
            }
            return name;
        }


        public void deleteUser(User user)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                user.isDeleted = true;
                user.isActive = false;
                user.FirstName = "Deleted user";
                user.LastName = "Deleted user";
                user.Contact = "Deleted user";
                user.Department = Department.SelectAll;
                user.PreferredLanguage = PreferredLanguage.SelectAll;
                user.Rating = 0;
                user.NumberOfWarnings = 0;
                user.LockoutEnd = DateTime.MaxValue;
                context.Update(user);
                context.SaveChanges();
            }
        }

        public bool isUserDeleted(string emailAddress)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                foreach (User u in context.Users.ToList<User>())
                {
                    if (u.Email == emailAddress && u.isDeleted == true)
                    {
                        return true;
                    }
                }
                return false;
            }
        }


    }
}
