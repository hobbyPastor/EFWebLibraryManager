﻿using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Data
{
    public class ArtistsRepository : BaseRepository<Artist>
    {
        public ArtistsRepository(Context context) : base(context)
        {
        }

        public override Artist Get(int id, bool includeRelatedEntities = true)
        {
            var artists = Context.Artists.AsQueryable();

            if (includeRelatedEntities)
            {
                artists = artists
                    .Include(s => s.ComicBooks.Select(a => a.ComicBook.Series))
                    .Include(s => s.ComicBooks.Select(a => a.Role));
            }
            return artists
                .Where(a => a.Id == id)
                .SingleOrDefault();
        }

        public override IList<Artist> GetList()
        {
            return Context.Artists
                 .OrderBy(a => a.Name)
                 .ToList();
        }

        public bool HasArtist(int artistId, string artistName)
        {
            return Context.Artists
                    .Any(a => a.Id != artistId &&
                    a.Name == artistName);
        }
    }
}
