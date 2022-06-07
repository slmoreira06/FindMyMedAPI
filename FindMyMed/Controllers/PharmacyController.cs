using AutoMapper;
using FindMyMed.DAL;
using FindMyMed.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : Controller
    {
        private readonly IPharmsRepository repository;
        private readonly IMapper mapper;

        public PharmacyController(IPharmsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<ReadPharmDTO>> GetPharms()
        {
            /*
             *****GPS*****
          
             GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
             watcher.TryStart(true, TimeSpan.FromMilliseconds(1000));
             Thread.Sleep(1000);
             GeoCoordinate coord = new GeoCoordinate(watcher.Position.Location.Latitude, watcher.Position.Location.Longitude);

             var lon1 = coord.Longitude;
             var lat1 = coord.Latitude;
             */

            /******IP-Address******/
            string accessKey = "86e80a53f3895bb489250a2aa1cd0a262e2175bb32edd6fff241350e";
            String address = "";
            WebRequest req = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = req.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }
            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);
            string url = "https://api.ipdata.co/" + address + "?api-key=" + accessKey;
            WebRequest request = WebRequest.Create(url);
            WebClient client = new WebClient();
            string jsonstring = client.DownloadString(url);
            dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);

            double lon1 = 0;
            double lat1 = 0;

            lon1 = dynObj.longitude;
            lat1 = dynObj.latitude;

            var pharms = repository.GetPharms();
            foreach (var pharm in pharms)
            {
                //IPCA coordinates
                //var lon1 = -8.627366221920695;
                //var lat1 = 41.53717385729081;
                var lon2 = pharm.Longitude;
                var lat2 = pharm.Latitude;

                double R = 6371;
                var lat = (lat2 - lat1).ToRadians();
                var lng = (lon2 - lon1).ToRadians();
                var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                              Math.Cos(lat1.ToRadians()) * Math.Cos(lat2.ToRadians()) *
                              Math.Sin(lng / 2) * Math.Sin(lng / 2);
                var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
                var d = R * h2;
                pharm.Distance = string.Format("{0}Km", Math.Round(Convert.ToSingle(d), 3));
            }
            return Ok(mapper.Map<IEnumerable<ReadPharmDTO>>(pharms));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<ReadPharmDTO> GetPharmById(int id)
        {
            var pharm = repository.GetPharmById(id);

            if (pharm is null)
                return NotFound();

            return Ok(mapper.Map<ReadPharmDTO>(pharm));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Pharm, Admin")]
        public ActionResult<ReadPharmDTO> UpdatePharmProfile(int id, UpdatePharmDTO pharm)
        {
            if (pharm is null)
                return NotFound();

            repository.UpdatePharmProfile(id, pharm);

            return NoContent();
        }
    }
    public static class NumericExtensions
    {
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
