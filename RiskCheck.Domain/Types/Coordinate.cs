using System;
using System.Collections.Generic;
using System.Text;

namespace RiskCheck.Domain
{
    public struct Coordinate : IEquatable<Coordinate>
    {
        private readonly double _latitude;
        private readonly double _longitude;

        public double Latitude { get { return _latitude; } }
        public double Longitude { get { return _longitude; } }

        public Coordinate(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        public bool Equals(Coordinate other)
        {
            if ((other.Latitude == this.Latitude) && (other.Longitude == this.Longitude))
                return true;
            return false;
        }
    }
}
