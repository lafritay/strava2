using System.Collections.Generic;
using Strava.Activities;
using Strava.Authentication;
using Strava.Clients;
using Strava.Clubs;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StaticAuthentication auth = new StaticAuthentication("1208e775c37887b243769cd0222992a1bed60727");
            StravaClient client = new StravaClient(auth);

            Club club = client.Clubs.GetClub("661551");
            List<ActivitySummary> activities = client.Clubs.GetLatestClubActivities(club.Id.ToString());
        }
    }
}