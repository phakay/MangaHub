﻿using FluentAssertions;
using MangaHub.Core.Models;
using MangaHub.Persistence;
using MangaHub.Persistence.Repositories;
using NUnit.Framework;
using System.Linq;

namespace MangaHub.IntegrationTests.Repositories
{
    [TestFixture]
    public class MangaRepositoryTests
    {
        private MangaRepository _repo;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _repo = new MangaRepository(_context);

        }

        [Test, Isolated]
        public void GetManga_ValidMangaId_MangaShouldBeReturned()
        {
            var manga = new Manga
            {
                Artist = _context.Users.First(),
                Title = "-",
                Description = "",
                Genre = _context.Genres.First(),
                Picture = new byte[0]
            };

            _context.Mangas.Add(manga);
            _context.SaveChanges();

            _repo.GetManga(manga.Id)
                .Should().Be(manga);
        }

        [Test, Isolated]
        public void Add_ValidData_MangaShouldBeAdded()
        {
            var manga = new Manga
            {
                Artist = _context.Users.First(),
                Title = "-",
                Description = "",
                Genre = _context.Genres.First(),
                Picture = new byte[0]
            };

            _repo.Add(manga);
            _context.SaveChanges();

            _context.Mangas
                .SingleOrDefault(m => m.Id == manga.Id)
                .Should().Be(manga);
        }
    }
}
