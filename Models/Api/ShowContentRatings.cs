﻿using System.Collections.Generic;
using Models.Api.Components;
using Newtonsoft.Json;

namespace Models.Api
{
    public class ShowContentRatings : IRoot
    {
        public List<ShowContentRatingsResults> results { get; set; }
        public int id { get; set; }
    }
}