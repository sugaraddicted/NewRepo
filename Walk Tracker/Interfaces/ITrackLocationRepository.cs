using Microsoft.EntityFrameworkCore;
using Walk_Tracker.Models;

namespace Walk_Tracker.Interfaces
{
    public interface ITrackLocationRepository
    {
        public List<Walk> GetWalks();
        public List<TrackLocation> GetTrackLocations();
       
    }
}
