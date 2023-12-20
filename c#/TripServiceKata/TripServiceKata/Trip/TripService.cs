using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            var loggedUser = GetLoggedUser();
            var userTrips = FindTripsByUser(user);
            return GetTripsByUser(user, loggedUser, userTrips);
        }


        private List<Trip> GetTripsByUser(User.User user, User.User loggedUser, List<Trip> userTrips)
        {
            List<Trip> tripList = new List<Trip>();
            if (loggedUser != null)
            {
                var isFriend = IsFriend(loggedUser, user);

                if (isFriend)
                {
                    tripList = userTrips;
                }

                return tripList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }

        protected virtual bool IsFriend(User.User loggedUser, User.User user)
        {
            bool isFriend = false;
            foreach (User.User friend in user.GetFriends())
            {
                if (friend.Equals(loggedUser))
                {
                    isFriend = true;
                    break;
                }
            }

            return isFriend;
        }

        protected virtual User.User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
        
        protected virtual List<Trip> FindTripsByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}
