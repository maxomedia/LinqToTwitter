﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LinqToTwitter
{
    /// <summary>
    /// Geographical coordinates
    /// </summary>
    public class Coordinate
    {
        public const int LatitudePos = 0;
        public const int LongitudePos = 1;

        /// <summary>
        /// Converts XML to a Coordinate
        /// </summary>
        /// <param name="coordinate">XML to convert</param>
        /// <returns>Coordinate holding info from XML</returns>
        public Coordinate CreateCoordinate(XElement coordinate)
        {
            List<XElement> coords = coordinate.Elements("item").ToList();

            return new Coordinate
            {
                Latitude = decimal.Parse(coords[LatitudePos].Value),
                Longitude = decimal.Parse(coords[LongitudePos].Value)
            };
        }

        /// <summary>
        /// Latitude
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public decimal Longitude { get; set; }
    }
}
