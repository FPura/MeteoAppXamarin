﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoAppXamarin
{
    public class Location
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Weather Weather = null;
       
    }
}
