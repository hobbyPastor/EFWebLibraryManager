using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class ComicBookArtistsRepository : BaseRepository<ComicBookArtist>
    {
        public ComicBookArtistsRepository(Context context) : base(context)
        {

        }

        public override IList<ComicBookArtist> GetList()
        {
            throw new NotImplementedException();
        }

        public override ComicBookArtist Get(int id, bool includeRelatedEntities = true)
        {
            var comicBookArtists = Context.ComicBookArtists.AsQueryable();

            if (includeRelatedEntities)
            {
                comicBookArtists = comicBookArtists
                    .Include(cba => cba.ComicBook.Series)
                    .Include(cba => cba.Artist)
                    .Include(cba => cba.Role);
            }

            return comicBookArtists
                .Where(cba => cba.Id == (int)id)
                .SingleOrDefault();
        }
    }
}
