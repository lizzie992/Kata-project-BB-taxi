using BB;
using GoogleApi.Entities.Search.Common;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.StaticMaps;
using GoogleMapsApi.StaticMaps.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;


namespace BaxiWebApp.Data
{
    public class DataService
    {

        int minDistanceLimit = 3000;
        int maxDistanceLimit = 200000;

        /// <summary>
        /// check if a distance is under the limit
        /// </summary>
        /// <param name="distance">Distance</param>
        /// <returns>bool</returns>
        public async Task<bool> IsItTooCloseToBB(int distance)
        {
            if (distance <= minDistanceLimit)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// check if a distance is above the limit
        /// </summary>
        /// <param name="distance">Distance</param>
        /// <returns>bool</returns>
        public async Task<bool> IsItTooFarFromBB(int distance)
        {
            if (distance >= maxDistanceLimit)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Takes the coordinates of 2 ads, calculates the routes to Baiebrunn and checks if there is any match there
        /// </summary>
        /// <param name="coordinate1"></param>
        /// <param name="coordinate2"></param>
        /// <param name="coordinate3"></param>
        /// <param name="coordinate4"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckMatchingRoutes(double? coordinate1, double? coordinate2, double? coordinate3, double? coordinate4)
        {
            if (coordinate1 == null || coordinate2 == null || coordinate3 == null || coordinate4 == null)
            {
                return false;
            }

            //calculate the steps for the first ad:
            var request1 = new DirectionsRequest
            {
                Origin = $"{coordinate1?.ToString("F8", CultureInfo.InvariantCulture)}, {coordinate2?.ToString("F8", CultureInfo.InvariantCulture)}",
                Destination = $"Baierbrunn, Germany",
                TravelMode = TravelMode.Driving,
                ApiKey = "AIzaSyANS3CV3B_21cbYSCLWxTr0gOZpJSPmnvk"
            };
            var result1 = await GoogleMaps.Directions.QueryAsync(request1);
            if (result1.Routes.Count() == 0)
            {
                return false;
            }
            // var legs = result.Routes.First().Legs;
            var route1 = result1.Routes.First();
            var leg1 = route1.Legs.First();
            var steps1 = leg1.Steps;

            //calculate the steps for the second ad:
            var request2 = new DirectionsRequest
            {
                Origin = $"{coordinate3?.ToString("F8", CultureInfo.InvariantCulture)}, {coordinate4?.ToString("F8", CultureInfo.InvariantCulture)}",
                Destination = $"Baierbrunn, Germany",
                TravelMode = TravelMode.Driving,
                ApiKey = "AIzaSyANS3CV3B_21cbYSCLWxTr0gOZpJSPmnvk"
            };
            var result2 = await GoogleMaps.Directions.QueryAsync(request2);
            if (result2.Routes.Count() == 0)
            {
                return false;
            }
            var route2 = result2.Routes.First();
            var leg2 = route2.Legs.First();
            var steps2 = leg2.Steps;

            //these values represent a 1km distance in coordinates:
            double latitudeDifference = 0.008983;
            double longitudeDifference = 0.01342;


            foreach (Step step1 in steps1)
            {
                foreach (Step step2 in steps2)
                {
                    double dif1 = Math.Abs((double)((step1.StartLocation.Latitude - step2.StartLocation.Latitude)));
                    double dif2 = Math.Abs((double)((step1.StartLocation.Longitude - step2.StartLocation.Longitude)));
                    if (dif1 <= latitudeDifference &&
                        dif2 <= longitudeDifference)
                    {
                        return true;
                    }
                }
            }

            //foreach (Step step1 in steps1)
            //{
            //    foreach (Step step2 in steps2)
            //    {
            //        if (step1.StartLocation.Latitude == step2.StartLocation.Latitude &&
            //        step1.StartLocation.Longitude == step2.StartLocation.Longitude &&
            //        step1.EndLocation.Latitude == step2.EndLocation.Latitude &&
            //        step1.EndLocation.Longitude == step2.EndLocation.Longitude)
            //        {
            //            return true;
            //        }
            //    }
            //}

            return false;
        }


        /// <summary>
        /// Calculates the distance between 2 locations based on coordinates using Google API
        /// </summary>
        /// <param name="coordinate1">First location latitudes</param>
        /// <param name="coordinate2">First location longitudes</param>
        /// <param name="coordinate3">Second location latitudes</param>
        /// <param name="coordinate4">Second location longitudes</param>
        /// <returns>int -> in meters!!!</returns>
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
        /// Checks if the distance between the pick up location of 2 ads is under the limit
        /// </summary>
        /// <param name="ad1"></param>
        /// <param name="ad2"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckDistanceBetweenAds(Ad ad1, Ad ad2)
        {
            int timeDifference = Math.Abs((int)Math.Round((ad2.PickUpDateAndTime - ad1.PickUpDateAndTime).TotalHours));
            if (timeDifference <= Constants.TIME_LIMIT_FOR_NOTIFICATIONS)
            {
                int distance = await GetDistance(ad1.Latitude, ad1.Longitude, ad2.Latitude, ad2.Longitude);
                if (distance == Constants.NULL_COORDINATES)
                {
                    return false;
                }
                else if (distance == Constants.INVALID_ROUTE)
                {
                    return false;
                }
                else if (distance <= Constants.DISTANCE_LIMIT_FOR_NOTIFICATIONS)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///Calculates the coordinates from a given address using Google API
        /// </summary>
        /// <param name="address">Address</param>
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


        /// <summary>
        /// Checks the selected ad against the currently existing ads, if any of them has the same legs in the route, and collects them in a list of ads
        /// </summary>
        /// <param name="ad">Ad</param>
        /// <returns>List of ads</returns>
        public async Task<List<Ad>> collectRecommendedAds(Ad ad, User currentlyLoggedInUser)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                List<Ad> adList = new List<Ad>();
                var result = context.Ads.Include(u => u.AdOwner).ToList<Ad>().AsEnumerable();
                result = result.Where(oldAd => oldAd.PickUpDateAndTime >= DateTime.Now && oldAd.AdOwner != currentlyLoggedInUser);

                //TO Baierbrunn, passenger looking for drivers
                if (ad.AdDirection == AdDirection.ToBaierbrunn && ad.AdType == AdType.Passenger)
                {
                    result = result.Where(oldAd => oldAd.AdDirection == AdDirection.ToBaierbrunn && oldAd.AdType == AdType.Driver && oldAd.NumberOfSeats >= ad.NumberOfSeats);
                }
                //TO Baierbrunn, driver looking for passengers
                if (ad.AdDirection == AdDirection.ToBaierbrunn && ad.AdType == AdType.Driver)
                {
                    result = result.Where(oldAd => oldAd.AdDirection == AdDirection.ToBaierbrunn && oldAd.AdType == AdType.Passenger && oldAd.NumberOfSeats <= ad.NumberOfSeats);
                }
                //FROM Baierbrunn, passenger looking for drivers
                if (ad.AdDirection == AdDirection.FromBaierbrunn && ad.AdType == AdType.Passenger)
                {
                    result = result.Where(oldAd => oldAd.AdDirection == AdDirection.FromBaierbrunn && oldAd.AdType == AdType.Driver && oldAd.NumberOfSeats >= ad.NumberOfSeats);
                }
                //FROM Baierbrunn, driver looking for passengers
                if (ad.AdDirection == AdDirection.FromBaierbrunn && ad.AdType == AdType.Driver)
                {
                    result = result.Where(oldAd => oldAd.AdDirection == AdDirection.FromBaierbrunn && oldAd.AdType == AdType.Passenger && oldAd.NumberOfSeats <= ad.NumberOfSeats);
                }

                foreach (var v in result)
                {
                    if (v == ad)
                    {
                        continue;
                    }
                    int timeDifference = Math.Abs((int)Math.Round((ad.PickUpDateAndTime - v.PickUpDateAndTime).TotalHours));
                    if (timeDifference <= Constants.TIME_LIMIT_FOR_MATCHING_ROUTE)
                    {
                        if (await CheckMatchingRoutes(ad.Latitude, ad.Longitude, v.Latitude, v.Longitude))
                        {
                            adList.Add(v);
                        }
                    }
                }
                return adList;
            }
               
        }



        public event EventHandler<int> ChatChanged;

        /// <summary>
        /// Sends a message in a given chat conversation and also fires a ChatChanged event
        /// </summary>
        /// <param name="C">Conversation</param>
        /// <param name="currentlyLoggedInUser">User that is sending the message</param>
        /// <param name="TextMessage">String</param>
        /// <param name="ID">ID of the conversation</param>
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
                ChatChanged?.Invoke(this, CurrentConversation.ID);
            }

        }

        /// <summary>
        /// Finds the conversation from the database based on the ID
        /// </summary>
        /// <param name="ID">Int - ID number</param>
        /// <returns>Conversation object</returns>
        public Conversation? GetCurrentConversation(int ID)
        {
            using (var context = _dbcFactory.CreateDbContext())
            {
                var result = context.Conversations.Include(c => c.adOwnerUser).Include(c => c.contactingUser).Include(c => c.messages).FirstOrDefault(conversation => conversation.ID == ID);
                return result;
            }
        }


        /// <summary>
        /// Removes a conversation and all messages from the database
        /// </summary>
        /// <param name="C">Conversation object</param>
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

        /// <summary>
        /// Sends email message from the BaierbrunnTaxi email account using gmail client
        /// </summary>
        /// <param name="emailAddress">String</param>
        /// <param name="subject">String</param>
        /// <param name="message">String</param>
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

        /// <summary>
        /// Sends the whole conversation including messages, users and timestamps to the email address of the admins
        /// </summary>
        /// <param name="C">Conversation object</param>
        /// <param name="currentlyLoggedInUser">User who is reporting the conversation</param>
        /// <param name="reasonForReporting">String</param>
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
                string subject = "A conversation was just reported to you";
                string text = $"Please check out the following conversation below that {currentlyLoggedInUser} reported to you, with the following reason: {reasonForReporting}\r\n{message}";

                sendEmail(emailAddress, subject, text);

            }
        }

        /// <summary>
        /// Sends the ad with all parameters in an email to the admins
        /// </summary>
        /// <param name="ad">Ad object</param>
        /// <param name="currentlyLoggedInUser">User who is reporting the conversation</param>
        /// <param name="reasonForReporting">String</param>
        public void reportAd(Ad ad, User currentlyLoggedInUser, string reasonForReporting)
        {
            string adDetails = $"Ad owner: {showUserNameWithStatus(ad.AdOwner)}\r\nAd type: {ad.AdType}\r\nAd Direction: {ad.AdDirection}\r\nAddress: {ad.PickUpDropOffLocation}\r\nPick up date and time: {ad.PickUpDateAndTime}\r\nNumber of seats: {ad.NumberOfSeats}\r\nSpecific requests: {ad.SpecificRequests}\r\n";

            string emailAddress = "gulyaskata99@gmail.com";
            string subject = "An ad was just reported to you";
            string text = $"Please check out the following ad below that {showUserNameWithStatus(currentlyLoggedInUser)} reported to you: \r\n{adDetails}\r\nThe reason for reporting is: {reasonForReporting}\r\nOpen the ad here: LINK";
            sendEmail(emailAddress, subject, text);

        }


        int maxNumberOfWarnings = 3;
        /// <summary>
        /// Increases the number of warnings for the given user, sends an email notification about the warning to the affected user - reason included - if max number of warnings reached, also triggers the user deletion method
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="reasonForWarning">String</param>
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



        /// <summary>
        /// Sets user's "isActive" parameter to false and sends an email notification (including reason) about it to the user who will not be able to use the app anymore
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="reasonForDeactivating">String</param>
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

        /// <summary>
        /// Sets user's "isActive" parameter to true and sends an email notification about it to the user who will be able to use the app again
        /// </summary>
        /// <param name="user">User object</param>
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

        /// <summary>
        /// Shows the user's full name with Inactive or Deleted status depennsing on the given parameters
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>String</returns>
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

        /// <summary>
        /// Sets name and other identifying parameters of the user to "Deleted user", sets the isDeleted parameter to true and inactivates the user as well so they cannot use the app anymore
        /// </summary>
        /// <param name="user">User object</param>
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

        /// <summary>
        /// Checks if the given user is deleted or not based on the isDeleted parameter
        /// </summary>
        /// <param name="emailAddress">string</param>
        /// <returns>bool</returns>
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
