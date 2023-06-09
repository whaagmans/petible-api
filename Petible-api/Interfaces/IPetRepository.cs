﻿using Petible_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petible_api.Interfaces
{
    public interface IPetRepository : IRepository<Pet, string>
    {
        Task<List<Pet>> GetOnShelterId(string id);
    }
}
