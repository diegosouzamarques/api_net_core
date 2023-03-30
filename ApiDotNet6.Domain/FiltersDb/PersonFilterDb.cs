﻿using ApiDotNet6.Domain.Repositories;

namespace ApiDotNet6.Domain.FiltersDb
{
    public class PersonFilterDb: PagedBaseRequest
    {
        public string? Name { get; set; } 
    }
}
