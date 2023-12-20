using System.Collections.Generic;
using System.Linq;
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
            if (loggedUser == null) throw new UserNotLoggedInException();

            return IsFriend(loggedUser, user) ? userTrips : new List<Trip>();
        }

        protected virtual bool IsFriend(User.User loggedUser, User.User user)
        {
            return Enumerable.Contains(user.GetFriends(), loggedUser);
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