﻿using MangaHub.Core.Models;
using MangaHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MangaHub.Persistence.Repositories
{
    public class ReadingRepository : Repository<Reading>, IReadingRepository
    {
        private readonly IApplicationDbContext _context;

        public ReadingRepository(IApplicationDbContext context)
            : base(context.Readings)
        {
            _context = context;
        }

        public Reading GetReadingForManga(int mangaId, string userId)
        {
            return _context.Readings
                            .SingleOrDefault(r => r.MangaId == mangaId &&
                            r.UserId == userId);
        }

        public IEnumerable<Reading> GetReadingsForUser(string userId)
        {
            return _context.Readings
                .Where(r => r.UserId == userId).ToList();
        }
    }
}