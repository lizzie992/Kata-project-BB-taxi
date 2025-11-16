using BaxiWebApp.Components.Pages;
using BaxiWebApp.Components.Pages.Res;
using BB;
using GoogleApi.Entities.Search.Video.Common;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Net;
using System.Net.Mail;



namespace BaxiWebApp.Data
{
    public class DataService
    {

        private IStringLocalizer<Resources> _localizer;

        private readonly IDbContextFactory<BaxiWebAppContext> _dbcFactory;


        public DataService(IDbContextFactory<BaxiWebAppContext> dbcFactory, IStringLocalizer<Resources> localizer) //dependency injection of the DbContextFactory and the localizer
        {
            _dbcFactory = dbcFactory;
            _localizer = localizer;
        }



        public event EventHandler<CultureInfo>? CultureChanged;

        /// <summary>
        /// Gets Culture input from UI, changes the current culture to this choice, and if the user is logged in, changes the user's culture property to this value
        /// </summary>
        /// <param name="e">Value from UI input</param>
        /// <param name="user">Logged in user</param>
        public void ChangeCulture(ChangeEventArgs e, User? user)
        {
            var selectedCulture = e.Value?.ToString();
            if (selectedCulture is not null)
            {
                CultureChanged?.Invoke(this, CultureInfo.CurrentUICulture);
                if (user is not null)
                {
                    setCulture(selectedCulture, user);
                    using var context = _dbcFactory.CreateDbContext();
                    context.Update(user);
                    user.Culture = selectedCulture.ToString();
                    context.SaveChanges();

                }
            }
        }

        /// <summary>
        /// Changes the current culture to a specific value
        /// </summary>
        /// <param name="culture">string</param>
        public void setCulture(string culture, User user)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultures(CultureTypes.AllCultures)
                           .First(c => c.Name == culture);
            CultureInfo.CurrentUICulture = CultureInfo.GetCultures(CultureTypes.AllCultures)
            .First(c => c.Name == culture);
            if (user is not null)
            {
                using var context = _dbcFactory.CreateDbContext();
                context.Update(user);
                user.Culture = culture.ToString();
                context.SaveChanges();

            }
        }

        /// <summary>
        /// Takes the culture property of a given user (if the user is not null) and changes the current culture to this value
        /// </summary>
        /// <param name="user">User</param>
        public void loadCulture(User? user)
        {
            if (user is not null)
            {
                var selectedCulture = user.Culture.ToString();
                setCulture(selectedCulture, user);
            }
        }


        int minDistanceLimit = 3000;
        int maxDistanceLimit = 200000;

        /// <summary>
        /// check if a distance is under the limit
        /// </summary>
        /// <param name="distance">Distance</param>
        /// <returns>bool</returns>
        public bool IsItTooCloseToBB(int distance)
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
        public bool IsItTooFarFromBB(int distance)
        {
            if (distance >= maxDistanceLimit)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Takes the coordinates of 2 ads, calculates the routes to Baierbrunn and checks if there is any match there
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
        /// Compares the new Ad that was just created by the user with the existing ads, based on pick up time and the distance between the pickup / dropoff locations
        /// If they are close enough in time and in geographical location, it sends an email notification to both ad owners, which email will contain the details of the other ad
        /// </summary>
        /// <param name="newAd">Ad</param>
        /// <param name="adList"><List of Ads/param>
        /// <returns>bool</returns>
        public async Task CheckDistanceAndSendNotification(Ad newAd, List<Ad> adList)
        {
            foreach (Ad oldAd in adList)
            {
                if (await CheckDistanceBetweenAds(newAd, oldAd) && oldAd.AdOwner.Email is not null && newAd.AdOwner.Email is not null)
                {
                    if (oldAd.AdOwner.AreNotificationsOn == true)
                    {
                        string messageNewAd = _localizer["This_ad_might_be_interesting_for_you_ad_owner_{0}_ad_type_{1}_ad_direction_{2}_address_{3}_pick_up_date_and_time_{4}_number_of_seats_{5}_specific_requests_{6}_open_the_ad_here_http_localhost_5049_ShowAd_{7}", newAd.AdOwner, newAd.AdType, newAd.AdDirection, newAd.PickUpDropOffLocation, newAd.PickUpDateAndTime, newAd.NumberOfSeats, newAd.SpecificRequests, newAd.ID];
                        string emailAddress = oldAd.AdOwner.Email.ToString();
                        string subject = _localizer["Check_out_this_ad"];
                        sendEmail(emailAddress, subject, messageNewAd);
                    }
                    if (newAd.AdOwner.AreNotificationsOn == true)
                    {
                        string messageOldAd = _localizer["This_ad_might_be_interesting_for_you_ad_owner_{0}_ad_type_{1}_ad_direction_{2}_address_{3}_pick_up_date_and_time_{4}_number_of_seats_{5}_specific_requests_{6}_open_the_ad_here_http_localhost_5049_ShowAd_{7}", oldAd.AdOwner, oldAd.AdType, oldAd.AdDirection, oldAd.PickUpDropOffLocation, oldAd.PickUpDateAndTime, oldAd.NumberOfSeats, oldAd.SpecificRequests, oldAd.ID];
                        string emailAddress = newAd.AdOwner.Email.ToString();
                        string subject = _localizer["Check_out_this_ad"];
                        sendEmail(emailAddress, subject, messageOldAd);
                    }
                }
                else if (await CheckMatchingRoutes(newAd.Latitude, newAd.Longitude, oldAd.Latitude, oldAd.Longitude) && oldAd.AdOwner.Email is not null && newAd.AdOwner.Email is not null)
                {
                    if (oldAd.AdOwner.AreNotificationsOn == true)
                    {
                        string messageNewAd = _localizer["This_ad_might_be_interesting_for_you_ad_owner_{0}_ad_type_{1}_ad_direction_{2}_address_{3}_pick_up_date_and_time_{4}_number_of_seats_{5}_specific_requests_{6}_open_the_ad_here_http_localhost_5049_ShowAd_{7}", newAd.AdOwner, newAd.AdType, newAd.AdDirection, newAd.PickUpDropOffLocation, newAd.PickUpDateAndTime, newAd.NumberOfSeats, newAd.SpecificRequests, newAd.ID];
                        string emailAddress = oldAd.AdOwner.Email.ToString();
                        string subject = _localizer["Check_out_this_ad"];
                        sendEmail(emailAddress, subject, messageNewAd);
                    }
                    if (newAd.AdOwner.AreNotificationsOn == true)
                    {
                        string messageOldAd = _localizer["This_ad_might_be_interesting_for_you_ad_owner_{0}_ad_type_{1}_ad_direction_{2}_address_{3}_pick_up_date_and_time_{4}_number_of_seats_{5}_specific_requests_{6}_open_the_ad_here_http_localhost_5049_ShowAd_{7}", oldAd.AdOwner, oldAd.AdType, oldAd.AdDirection, oldAd.PickUpDropOffLocation, oldAd.PickUpDateAndTime, oldAd.NumberOfSeats, oldAd.SpecificRequests, oldAd.ID];
                        string emailAddress = newAd.AdOwner.Email.ToString();
                        string subject = _localizer["Check_out_this_ad"];
                        sendEmail(emailAddress, subject, messageOldAd);
                    }
                }
            }
        }


        /// <summary>
        /// Compares the new Ad that was just created by the user with the existing ads, based on ad type (drivers against passengers) and Direction (TO or FROM Baierbrunn)
        /// If there is a match, it triggers the CheckDistanceAndSendNotification method for further checks and possible email notifications
        /// </summary>
        /// <param name="newAd">Ad</param>
        /// <returns>bool</returns>
        public async Task CheckAdsForNotification(Ad? newAd) //newAd is the ad that was just added, the oldAd is the ad that already existed - both will be subject of notifications, so if there is a match, both adowners will be notified
        {
            using var context = _dbcFactory.CreateDbContext();

            var adList = context.Ads.Include(u => u.AdOwner).ToList<Ad>().AsEnumerable();

            //TO Baierbrunn, passenger looking for drivers
            if (newAd?.AdDirection == AdDirection.ToBaierbrunn && newAd.AdType == AdType.Passenger)
            {
                adList = adList.Where(oldAd => oldAd.AdDirection == AdDirection.ToBaierbrunn && oldAd.AdType == AdType.Driver && oldAd.NumberOfSeats >= newAd.NumberOfSeats && Math.Abs((int)Math.Round((oldAd.PickUpDateAndTime - newAd.PickUpDateAndTime).TotalHours)) <= Constants.TIME_LIMIT_FOR_MATCHING_ROUTE);
                await CheckDistanceAndSendNotification(newAd, adList.ToList());
            }

            //TO Baierbrunn, driver looking for passengers
            if (newAd?.AdDirection == AdDirection.ToBaierbrunn && newAd.AdType == AdType.Driver)
            {
                adList = adList.Where(oldAd => oldAd.AdDirection == AdDirection.ToBaierbrunn && oldAd.AdType == AdType.Passenger && oldAd.NumberOfSeats <= newAd.NumberOfSeats && Math.Abs((int)Math.Round((oldAd.PickUpDateAndTime - newAd.PickUpDateAndTime).TotalHours)) <= Constants.TIME_LIMIT_FOR_MATCHING_ROUTE);
                await CheckDistanceAndSendNotification(newAd, adList.ToList());
            }


            //FROM Baierbrunn, passenger looking for drivers
            if (newAd?.AdDirection == AdDirection.FromBaierbrunn && newAd.AdType == AdType.Passenger)
            {
                adList = adList.Where(oldAd => oldAd.AdDirection == AdDirection.FromBaierbrunn && oldAd.AdType == AdType.Driver && oldAd.NumberOfSeats >= newAd.NumberOfSeats && Math.Abs((int)Math.Round((oldAd.PickUpDateAndTime - newAd.PickUpDateAndTime).TotalHours)) <= Constants.TIME_LIMIT_FOR_MATCHING_ROUTE);
                await CheckDistanceAndSendNotification(newAd, adList.ToList());
            }

            //FROM Baierbrunn, driver looking for passengers
            if (newAd?.AdDirection == AdDirection.FromBaierbrunn && newAd.AdType == AdType.Driver)
            {
                adList = adList.Where(oldAd => oldAd.AdDirection == AdDirection.FromBaierbrunn && oldAd.AdType == AdType.Passenger && oldAd.NumberOfSeats <= newAd.NumberOfSeats && Math.Abs((int)Math.Round((oldAd.PickUpDateAndTime - newAd.PickUpDateAndTime).TotalHours)) <= Constants.TIME_LIMIT_FOR_MATCHING_ROUTE);
                await CheckDistanceAndSendNotification(newAd, adList.ToList());
            }

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

            Location? location = result.Results.FirstOrDefault()?.Geometry?.Location;

            if (location == null)
            {
                return (null, null);
            }

            string coordinates = $"{location.Latitude}, {location.Longitude}";

            return (location.Latitude, location.Longitude);
        }



        /// <summary>
        /// Checks the selected ad against the currently existing ads, if any of them has the same legs in the route, and collects them in a list of ads
        /// </summary>
        /// <param name="ad">Ad</param>
        /// <returns>List of ads</returns>
        public async Task<List<Ad>> collectRecommendedAds(Ad? ad, User? currentlyLoggedInUser)
        {
            using var context = _dbcFactory.CreateDbContext();

            List<Ad> adList = new List<Ad>();
            var result = context.Ads.Include(u => u.AdOwner).ToList<Ad>().AsEnumerable();
            result = result.Where(oldAd => oldAd.PickUpDateAndTime >= DateTime.Now && oldAd.AdOwner.Id != currentlyLoggedInUser.Id);

            //TO Baierbrunn, passenger looking for drivers
            if (ad?.AdDirection == AdDirection.ToBaierbrunn && ad.AdType == AdType.Passenger)
            {
                result = result.Where(oldAd => oldAd.AdDirection == AdDirection.ToBaierbrunn && oldAd.AdType == AdType.Driver && oldAd.NumberOfSeats >= ad.NumberOfSeats);
            }
            //TO Baierbrunn, driver looking for passengers
            if (ad?.AdDirection == AdDirection.ToBaierbrunn && ad.AdType == AdType.Driver)
            {
                result = result.Where(oldAd => oldAd.AdDirection == AdDirection.ToBaierbrunn && oldAd.AdType == AdType.Passenger && oldAd.NumberOfSeats <= ad.NumberOfSeats);
            }
            //FROM Baierbrunn, passenger looking for drivers
            if (ad?.AdDirection == AdDirection.FromBaierbrunn && ad.AdType == AdType.Passenger)
            {
                result = result.Where(oldAd => oldAd.AdDirection == AdDirection.FromBaierbrunn && oldAd.AdType == AdType.Driver && oldAd.NumberOfSeats >= ad.NumberOfSeats);
            }
            //FROM Baierbrunn, driver looking for passengers
            if (ad?.AdDirection == AdDirection.FromBaierbrunn && ad.AdType == AdType.Driver)
            {
                result = result.Where(oldAd => oldAd.AdDirection == AdDirection.FromBaierbrunn && oldAd.AdType == AdType.Passenger && oldAd.NumberOfSeats <= ad.NumberOfSeats);
            }

            foreach (var v in result)
            {
                if (v == ad)
                {
                    continue;
                }
                if (ad is not null)
                {
                    int timeDifference = Math.Abs((int)Math.Round((ad.PickUpDateAndTime - v.PickUpDateAndTime).TotalHours));
                    if (timeDifference <= Constants.TIME_LIMIT_FOR_MATCHING_ROUTE)
                    {
                        if (await CheckMatchingRoutes(ad.Latitude, ad.Longitude, v.Latitude, v.Longitude))
                        {
                            adList.Add(v);
                        }
                    }
                }

            }
            return adList;


        }


        /// <summary>
        /// Finds out if there is an existing conversation for this ad and for this logged in user
        /// </summary>
        /// <param name="ad">Ad</param>
        /// <param name="currentlyLoggedInUser">User</param>
        /// <returns>List of Conversations</returns>
        public List<Conversation> findPreviousConvo(Ad ad, User? currentlyLoggedInUser)
        {
            using var context = _dbcFactory.CreateDbContext();

            //check current ad if matching conversation exists:
            var previousConversations = context.Conversations.Include(C => C.contactingUser).AsQueryable();
            previousConversations = previousConversations.Where(C => C.AdID == ad.ID);
            previousConversations = previousConversations.Where(Conversation => Conversation.contactingUser.Id == currentlyLoggedInUser.Id);
            return previousConversations.ToList();

        }


        /// <summary>
        /// Creates new conversation for a specific ad, for a logged in User, with a starting message
        /// </summary>
        /// <param name="ad">Ad</param>
        /// <param name="currentlyLoggedInUser">User</param>
        /// <param name="message">String</param>
        /// <returns>Conversation</returns>
        public Conversation createConvo(Ad ad, User currentlyLoggedInUser, string message)
        {
            using var context = _dbcFactory.CreateDbContext();

            context.Attach(ad);
            context.Attach(currentlyLoggedInUser);
            Conversation conversation = new Conversation();
            conversation.adOwnerUser = ad.AdOwner;
            conversation.contactingUser = currentlyLoggedInUser;
            conversation.messages = new List<Message>();
            ad.adConversations.Add(conversation);
            context.SaveChanges();
            User? fromUser = getOtherUser(conversation, currentlyLoggedInUser);
            SendChatMessage(conversation, currentlyLoggedInUser, message, conversation.ID);
            return conversation;

        }

        /// <summary>
        /// defines who is the other participant in the conversation
        /// </summary>
        /// <param name="C">Conversation</param>
        /// <returns>User</returns>
        public User? getOtherUser(Conversation C, User? currentlyLoggedInUser)
        {
            User? otherUser = new User();
            if (C.contactingUser is null)
            {
                return null;
            }
            if (currentlyLoggedInUser.Id == C.contactingUser?.Id)
            {
                otherUser = C.adOwnerUser;
            }
            if (currentlyLoggedInUser.Id == C.adOwnerUser?.Id)
            {
                otherUser = C.contactingUser;
            }
            return otherUser;
        }


        public event EventHandler<int>? ChatChanged;

        /// <summary>
        /// Sends a message in a given chat conversation and also fires a ChatChanged event
        /// </summary>
        /// <param name="C">Conversation</param>
        /// <param name="currentlyLoggedInUser">User that is sending the message</param>
        /// <param name="TextMessage">String</param>
        /// <param name="ID">ID of the conversation</param>
        public void SendChatMessage(Conversation C, User currentlyLoggedInUser, string TextMessage, int ID)
        {
            using var context = _dbcFactory.CreateDbContext();
            Conversation? CurrentConversation = context.Conversations.Include(c => c.adOwnerUser).Include(c => c.contactingUser).Include(c => c.messages).FirstOrDefault(conversation => conversation.ID == ID);
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
            CurrentConversation.messages.Add(message);
            context.SaveChanges();
            string emailMessage = _localizer["You_just_received_a_new_message_from_{0}_Go_and_check_it_out_http_localhost_5049_Chat_{1}", showUserNameWithStatus(message.fromUser), CurrentConversation.ID];
            string emailAddress = message.toUser.Email.ToString();
            string subject = _localizer["You_received_a_new_message"];
            sendEmail(emailAddress, subject, emailMessage);
            ChatChanged?.Invoke(this, CurrentConversation.ID);


        }






        /// <summary>
        /// Finds the conversation from the database based on the ID
        /// </summary>
        /// <param name="ID">Int - ID number</param>
        /// <returns>Conversation object</returns>
        public Conversation? GetCurrentConversation(int ID)
        {
            using var context = _dbcFactory.CreateDbContext();

            var result = context.Conversations.Include(c => c.adOwnerUser).Include(c => c.contactingUser).Include(c => c.messages).FirstOrDefault(conversation => conversation.ID == ID);
            return result;
        }


        /// <summary>
        /// Removes a conversation and all messages from the database
        /// </summary>
        /// <param name="C">Conversation object</param>
        public void DeleteConversation(Conversation C)
        {
            using var context = _dbcFactory.CreateDbContext();

            //first delete the messages that have the ID of this current conversation as long as the current conversation exists
            IEnumerable<Message> messagesToDelete = context.Messages.ToList<Message>().AsEnumerable();
            messagesToDelete = messagesToDelete.Where(m => m.ConversationID == C.ID);
            context.Messages.RemoveRange(messagesToDelete);
            //once the messages are removed I can delete the conversation
            context.Conversations.Remove(C);
            context.SaveChanges();

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
        /// Shows the user's full name with Inactive or Deleted status depennsing on the given parameters
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>String</returns>
        public string showUserNameWithStatus(User? user)
        {
            string name = "";
            if (user != null)
            {
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
            }
            if (user is null)
            {
                name = "Deleted user";
            }
            return name;
        }

        /// <summary>
        /// Sets name and other identifying parameters of the user to "Deleted user", sets the isDeleted parameter to true and inactivates the user as well so they cannot use the app anymore
        /// </summary>
        /// <param name="user">User object</param>
        public void deleteUser(User? user)
        {
            if (user is not null)
            {
                using var context = _dbcFactory.CreateDbContext();
                context.Attach(user);

                //ads
                var ads = context.Ads.Where(A => A.AdOwner.Id == user.Id);
                foreach (Ad a in ads)
                {
                    deleteAd(a);
                }

                //Conversations nr1
                var convos = context.Conversations.Include(C => C.adOwnerUser).Include(C => C.contactingUser).Where(C => C.contactingUser.Id == user.Id || C.adOwnerUser.Id == user.Id);
                foreach (Conversation C in convos)
                {
                    if (C.contactingUser?.Id == user.Id)
                    {
                        C.contactingUser = null;
                    }
                    if (C.adOwnerUser?.Id == user.Id)
                    {
                        C.adOwnerUser = null;
                    }
                }



                //messages nr1
                var messages = context.Messages.Where(M => M.fromUser.Id == user.Id || M.toUser.Id == user.Id);
                foreach (Message M in messages)
                {
                    if (M.fromUser?.Id == user.Id)
                    {
                        M.fromUser = null;
                    }
                    if (M.toUser?.Id == user.Id)
                    {
                        M.toUser = null;
                    }
                }

               
                context.Users.Remove(user);
                context.SaveChanges();

   

                //user.isDeleted = true;
                //user.isActive = false;
                //user.FirstName = "Deleted user";
                //user.LastName = "Deleted user";
                //user.Contact = "Deleted user";
                //user.Department = Department.SelectAll;
                //user.PreferredLanguage = PreferredLanguage.SelectAll;
                //user.Rating = 0;
                //user.NumberOfWarnings = 0;
                //user.LockoutEnd = DateTime.MaxValue;
                //context.Update(user);
                //context.SaveChanges();
            }
        }

        public void deleteAd(Ad ad)
        {
            if (ad is not null)
            {
                using var context = _dbcFactory.CreateDbContext();
                context.Attach(ad);
                var C = context.Conversations.Where(C => C.AdID == ad.ID);
                foreach (Conversation convo in C)
                {
                    convo.AdID = null;
                }
                context.Ads.Remove(ad);
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
            using var context = _dbcFactory.CreateDbContext();

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
