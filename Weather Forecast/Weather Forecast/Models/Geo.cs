using System;
using System.Collections.Generic;
using System.Text;

namespace Weather_Forecast.Models
{
    internal class Geo
    {
        public class Administrative
        {
            public string name { get; set; }
            public string description { get; set; }
            public string isoName { get; set; }
            public int order { get; set; }
            public int adminLevel { get; set; }
            public string isoCode { get; set; }
            public string wikidataId { get; set; }
            public int geonameId { get; set; }
        }

        public class Informative
        {
            public string name { get; set; }
            public string description { get; set; }
            public int order { get; set; }
            public string isoCode { get; set; }
            public string wikidataId { get; set; }
            public int geonameId { get; set; }
        }

        public class LocalityInfo
        {
            public List<Administrative> administrative { get; set; }
            public List<Informative> informative { get; set; }
        }

        public class Root
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string continent { get; set; }
            public string lookupSource { get; set; }
            public string continentCode { get; set; }
            public string localityLanguageRequested { get; set; }
            public string city { get; set; }
            public string countryName { get; set; }
            public string postcode { get; set; }
            public string countryCode { get; set; }
            public string principalSubdivision { get; set; }
            public string principalSubdivisionCode { get; set; }
            public string plusCode { get; set; }
            public string locality { get; set; }
            public LocalityInfo localityInfo { get; set; }
        }
    }
}
