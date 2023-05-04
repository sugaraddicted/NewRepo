using Walk_Tracker.Models;

namespace Walk_Tracker.Interfaces
{
    public interface IWalkRepository
    {
        List<Walk> GetWalksByImei(string imei);
        void AddWalk(Walk walk);
    }
}
