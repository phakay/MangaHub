﻿using MangaHub.Core.Models;
using MangaHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MangaHub.Persistence.Repositories
{
    public class MangaRepository : Repository<Manga>, IMangaRepository
    {
        private readonly IApplicationDbContext _context;

        public MangaRepository(IApplicationDbContext context)
            : base(context.Mangas)
        {
            _context = context;
        }

        public Manga GetManga(int id)
        {
            return _context.Mangas
                            .Include(g => g.Genre)
                            .Include(m => m.Artist)
                            .Include(m => m.Chapters)
                            .SingleOrDefault(m => m.Id == id);
        }

        public Manga GetMangaWithReaders(int id)
        {
            return _context.Mangas
                            .Include(g => g.Genre)
                            .Include(m => m.Artist)
                            .Include(m => m.Chapters)
                            .Include(m => m.Readings.Select(r => r.User))
                            .SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<Manga> GetMangasWithChapters(string userId = null)
        {
            var query = _context.Mangas
                            .Include(m => m.Artist)
                            .Include(m => m.Genre)
                            .Include(m => m.Chapters);

            if (userId != null)
                query = query.Where(m => m.ArtistId == userId);

            return query.ToList();
        }

        public IEnumerable<Manga> GetMangasReadByUser(string userId)
        {
            return _context.Readings
                .Where(r => r.UserId == userId)
                .Select(r => r.Manga)
                    .Include(m => m.Artist)
                    .Include(m => m.Genre)
                    .Include(m => m.Chapters)
                    .ToList();
        }
    }
}