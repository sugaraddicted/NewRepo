namespace Walk_Tracker.Models
{
    public class Walk
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<TrackLocation> Locations { get; set; }

        public Walk()
        {
            Locations = new List<TrackLocation>();
        }

        public double GetDistance()
        {
            double distance = 0;

            for (int i = 0; i < Locations.Count - 1; i++)
            {
                distance += GetDistance(Locations[i], Locations[i + 1]);
            }

            return distance;
        }

        public TimeSpan GetDuration()
        {
            return EndTime - StartTime;
        }

        private double GetDistance(TrackLocation location1, TrackLocation location2)
        {
            double lat1 = (double)location1.Latitude;
            double lon1 = (double)location1.Longitude;
            double lat2 = (double)location2.Latitude;
            double lon2 = (double)location2.Longitude;

            const double R = 6371; // Earth radius in km

            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);
            double lat1r = ToRadians(lat1);
            double lat2r = ToRadians(lat2);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1r) * Math.Cos(lat2r);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;

            return distance;
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}