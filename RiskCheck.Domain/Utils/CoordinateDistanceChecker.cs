using System;
using GeoCoordinatePortable;

namespace RiskCheck.Domain
{
    public static class CoordinateDistanceChecker
    {
        public static int Check(Coordinate startCoord, Coordinate endCoord)
        {
            var sCoord = new GeoCoordinate(startCoord.Latitude, startCoord.Longitude);
            var eCoord = new GeoCoordinate(endCoord.Latitude, endCoord.Longitude);
            var result = sCoord.GetDistanceTo(eCoord);
            return (int)result;
        }
    }
}
