using System.Text;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

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

        [Test]
        public void GivenNullUser_ByDefinition_AnExceptionIsThrown()
        {
            Assert.Throws<UserNotLoggedInException>(
                () => _tripService.GetTripsByUser(null));
        }
        
        [Test]
        public void GivenGuestUser_ByDefinition_AnExceptionIsThrown()
        {
            User.User guestUser = new User.User();
            
            Assert.Throws<UserNotLoggedInException>(
                () => _tripService.GetTripsByUser(guestUser));
        }

        [Test]
        public void GivenLoggedUser_WithNoFriends_GetsEmptyTripsAsResult()
        {
            
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