using System.ComponentModel.DataAnnotations.Schema;

namespace Walk_Tracker.Models
{
    public class TrackLocation
    {
        public int Id { get; set; }
        public string IMEI { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime DateEvent { get; set; }

        [Column("date_track")]
        public DateTime DateTrack { get; set; }
        public int TypeSource { get; set; }

        public static List<Walk> GroupWalks(List<TrackLocation> locations)
        {
            var groupedLocations = locations.GroupBy(l => l.IMEI);

            var walks = new List<Walk>();

            foreach (var group in groupedLocations)
            {
                var currentWalk = new Walk();

                foreach (var location in group.OrderBy(l => l.DateTrack))
                {
                    if (currentWalk.Locations.Count == 0)
                    {
                        currentWalk.StartTime = location.DateTrack;
                    }
                    else if ((location.DateTrack - currentWalk.EndTime).TotalMinutes >= 30)
                    {
                        currentWalk.EndTime = currentWalk.Locations.Last().DateTrack;
                        walks.Add(currentWalk);
                        currentWalk = new Walk();
                        currentWalk.StartTime = location.DateTrack;
                    }

                    currentWalk.EndTime = location.DateTrack;
                    currentWalk.Locations.Add(location);
                }

                if (currentWalk.Locations.Count > 0)
                {
                    walks.Add(currentWalk);
                }
            }

            return walks;
        }
    }
}
