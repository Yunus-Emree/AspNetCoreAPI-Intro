﻿using AspNetCoreAPI_Intro.Data;
using AspNetCoreAPI_Intro.Entities;
using AspNetCoreAPI_Intro.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace AspNetCoreAPI_Intro.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CityDbContext _context;
        

        public CountryRepository(CityDbContext cityDbContext)
        {
            _context = cityDbContext;
        }

        public async Task<List<Country>> GetAll()
        {
            return await _context.Countries.Include("Cities").ToListAsync(); //Eager Loading
        }

        //Eager Loading (default)  İlişkili tablolardaki dataları istediğimizde (include) yüklüyor.
        //Lazy Loading Baştan tüm ilişkili dataları yüklüyor. Çalışması için proxy ayarları yapmak gerekiyor.
    }
}
