using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RiskCheck.Domain;

namespace RiskCheck.Application
{
    public class PostCodeDistanceService : IPostCodeDistanceService
    {
        private Coordinate _start;
        private Coordinate _end;

        private MemoryCacheEntryOptions _cacheOptions;
        private IMemoryCache _cache;

        public PostCodeDistanceService(IMemoryCache memoryCache)
        {
            _cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(6000));
            _cache = memoryCache;
        }

        public async Task<int> GetDistance(string riskPostCode, string keptPostCode)
        {
            var result = 0;
            var key = new KeyValuePair<string, string>(riskPostCode, keptPostCode);

            if (_cache.TryGetValue(key, out result))
                return result;

            _start = await GetCoordinates(riskPostCode);
            _end = await GetCoordinates(keptPostCode);

            var distance = CoordinateDistanceChecker.Check(_start, _end);

            _cache.Set(key, distance, _cacheOptions);

            return distance;
        }

        private async Task<Coordinate> GetCoordinates(string postCode)
        {
            JObject obj;

            using (var httpClient = new HttpClient())
            {
                var json = await httpClient.GetStringAsync("http://uk-postcodes.com/postcode/" + postCode + ".json");
                obj =  JObject.Parse(json);
                
            }
            var lng = (string)obj["geo"]["lng"];
            var lat = (string)obj["geo"]["lat"];

            return new Coordinate(double.Parse(lat), double.Parse(lng));
        }
    }
}
