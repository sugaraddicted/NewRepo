using Microsoft.AspNetCore.Mvc;
using Walk_Tracker.Repository;
using Walk_Tracker.Interfaces;

namespace Walk_Tracker.Controllers
{
    public class TrackLocationController : Controller
    {
        private readonly IWalkRepository _walkRepository;

        public TrackLocationController(IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository;
        }

        [HttpGet("{imei}/total_info")]
        public IActionResult GetWalksByImei(string imei)
        {
            var walks = _walkRepository.GetWalksByImei(imei);

            if (!walks.Any())
            {
                return NotFound();
            }

            var totalWalks = walks.Count();
            var totalDistance = walks.Sum(w => w.GetDistance());
            var totalDuration = TimeSpan.FromSeconds(walks.Sum(w => w.GetDuration().TotalSeconds));

            var result = new
            {
                TotalWalks = totalWalks,
                TotalDistance = totalDistance,
                TotalDuration = totalDuration,
               
            };
            return Ok(result);
        }

        [HttpGet("{imei}/top10")]
        public IActionResult GetTop10WalksByImei(string imei)
        {
            var walks = _walkRepository.GetWalksByImei(imei);

            if (!walks.Any())
            {
                return NotFound();
            }

            var topWalks = walks.OrderByDescending(w => w.GetDistance()).Take(10);

            var result = new
            {
                TopWalks = topWalks.Select(w => new
                {
                    Distance = w.GetDistance(),
                    Duration = w.GetDuration()
                })
            };
            return Ok(result);
        }


    }
}
