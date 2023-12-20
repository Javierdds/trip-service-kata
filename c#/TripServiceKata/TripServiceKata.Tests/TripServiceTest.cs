using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using TripServiceKata.User;

namespace TripServiceKata.Tests
{
    public class TripServiceTest
    {
        private TripService _tripService;

        [SetUp]
        public void SetUp()
        {
            _tripService = new TripService();
        }

        [TearDown]
        public void TearDown()
        {
            _tripService = null;
        }
        
        [Test]
        public void GivenGuestUser_ByDefinition_AnExceptionIsThrown()
        {
            var user = new User.User();
            _tripService = new TripServiceForGuestUser();
            
            Assert.Throws<UserNotLoggedInException>(
                () => _tripService.GetTripsByUser(user));
        }

        [Test]
        public void GivenLoggedUserWithNoFriends_SearchUserTrips_GetsEmptyTripsAsResult()
        {
            var user = new User.User();
            var expectedValue = new List<Trip.Trip>();
            _tripService = new TripServiceForLoggedUser();

            var tripsByUser = _tripService.GetTripsByUser(user);
            
            Assert.AreEqual(tripsByUser, expectedValue);
        }

        [Test]
        public void GivenLoggedUser_GetNullUserTrips_AnExceptionIsThrown()
        {
            _tripService = new TripServiceForLoggedUser();
            
            Assert.Throws<System.NullReferenceException>(
                () => _tripService.GetTripsByUser(null));
        }
        
        [Test]
        public void GivenLoggedUserWithFriends_SearchUnfriendlyUserTrips_GetsEmptyTripsAsResult()
        {
            var user = new User.User();
            var expectedValue = new List<Trip.Trip>();
            _tripService = new TripServiceForLoggedUserWithFriends();

            var tripsByUser = _tripService.GetTripsByUser(user);
            
            Assert.AreEqual(tripsByUser, expectedValue);
        }
        
        [Test]
        public void GivenLoggedUserWithFriends_SearchFriendlyUserTrips_GetsUserTripsAsResult()
        {
            
        }
        
        private class TripServiceForLoggedUserWithFriends : TripService
        {
            private readonly User.User _friendlyUser = new User.User();
            
            protected override User.User GetLoggedUser()
            {
                User.User userWithFriends = new User.User();
                userWithFriends.AddFriend(_friendlyUser);
                
                return userWithFriends;
            }
        }
        
        private class TripServiceForLoggedUser : TripService
        {
            protected override User.User GetLoggedUser()
            {
                return new User.User();
            }
        }
        
        private class TripServiceForGuestUser : TripService
        {
            protected override User.User GetLoggedUser()
            {
                return null;
            }
        }
        
        // private void AssertAmount(int expected, string failMessage)
        // {
        //     if (health.Amount != expected)
        //     {
        //         var message = new StringBuilder();
        //
        //         message.AppendLine(failMessage);
        //         message.AppendLine($"  Expected: {expected}");
        //         message.AppendLine($"  But was: {whatever}");
        //
        //         Assert.Fail(message.ToString());
        //     }
        // }
    }
}