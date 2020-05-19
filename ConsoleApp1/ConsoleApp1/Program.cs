using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            FetchData();
        }

        private static void FetchData()
        {
            Configuration.ApiKey["access"] = "7f5ae620e9e43dab7e2a7ffc7ea5ae433e0eecb6";
            Configuration.ApiKeyPrefix["access"] = "Bearer";
            ApiClient client = new ApiClient();
            ClubsApi clubsApi = new ClubsApi(client);
            DetailedClub club = clubsApi.GetClubById(661551);

            string existingData = File.ReadAllText(c_dataPath);
            Dictionary<string, Activity> stored = JsonConvert.DeserializeObject<Activity[]>(existingData)
                .ToDictionary(a => a.Id);

            List<SummaryActivity> activities = clubsApi.GetClubActivitiesById(club.Id, 1, 100);
            foreach (SummaryActivity summary in activities)
            {
                if (summary.Name == "Bacon cheeseburger not a winning pre-workout meal" && summary.Athlete.FirstName == "Matthew" && summary.ElapsedTime == 1685)
                {
                    break;
                }

                Activity activity = new Activity(summary);
                stored[activity.Id] = activity;
            }

            // Sanity Check
            if (stored.Values.Any(s => s.Summary.Athlete.FirstName == "Alex" && s.Summary.Distance == 7964.3 && s.Summary.MovingTime == 3048))
            {
                throw new Exception("Found bad value, can't trust the sort");
            }

            string data = JsonConvert.SerializeObject(stored.Values);
            File.WriteAllText(c_dataPath, data);
        }

        private const string c_dataPath = "../../../data.json";
    }

    public class Activity
    {
        public Activity(
            SummaryActivity summary)
        {
            Id = $"{summary.Athlete.FirstName}_{summary.Athlete.LastName}_{summary.Name}" +
                $"_{summary.Distance}_{summary.Type}_{summary.MovingTime}";
            Summary = summary;
        }

        public string Id { get; }

        public SummaryActivity Summary { get; }
    }

    public class UserSummary
    {
        public string User { get; set; }

        public int Count { get; set; }

        public int TotalTime { get; set; }

        public decimal DistanceRan { get; set; }

        public decimal DistanceBiked { get; set; }
    }
}