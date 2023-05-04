using Microsoft.EntityFrameworkCore;
using Walk_Tracker.Data;
using Walk_Tracker.Interfaces;
using Walk_Tracker.Models;

namespace Walk_Tracker.Repository
{
    public class TrackLocationRepository : ITrackLocationRepository
    {
        private readonly WalkTrackerContext _context;
        public TrackLocationRepository(WalkTrackerContext context)
        {
            _context = context;
        }
        public List<Walk> GetWalks()
        {
            var locations = GetTrackLocations();

            return TrackLocation.GroupWalks(locations);
        }

        public List<TrackLocation> GetTrackLocations()
        {
            return _context.TrackLocation.ToList();
        }
    }
}
