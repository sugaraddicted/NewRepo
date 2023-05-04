using Walk_Tracker.Models;
using Walk_Tracker.Interfaces;
using Walk_Tracker.Data;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;

namespace Walk_Tracker.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly List<Walk> _walks;

        public WalkRepository(ITrackLocationRepository trackLocationRepository)
        {
            _walks = trackLocationRepository.GetWalks();
        }
        public void AddWalk(Walk walk)
        {
            _walks.Add(walk);
        }

        public List<Walk> GetWalksByImei(string imei)
        {
            return _walks.Where(w => w.Locations.Any() && w.Locations[0].IMEI == imei)
                         .OrderByDescending(w => w.GetDistance())
                         .Take(10)
                         .ToList();
        }
    }
}
