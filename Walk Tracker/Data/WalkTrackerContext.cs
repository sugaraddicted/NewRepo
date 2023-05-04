using Microsoft.EntityFrameworkCore;
using Walk_Tracker.Models;

namespace Walk_Tracker.Data
{
    public class WalkTrackerContext : DbContext
    {
        public WalkTrackerContext(DbContextOptions<WalkTrackerContext> options) : base(options)
        {
        }

        public DbSet<TrackLocation> TrackLocation { get; set; }
    }
}
