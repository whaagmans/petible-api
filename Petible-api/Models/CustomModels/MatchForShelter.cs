﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petible_api.Models.CustomModels
{
    public class MatchForShelter
    {
        public virtual string id { get; set; }
        public virtual string name { get; set; }
        public virtual int status { get; set; }
        public virtual string match_id { get; set; }

    }
}
