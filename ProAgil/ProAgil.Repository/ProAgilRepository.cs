using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext context;
        public ProAgilRepository(ProAgilContext _context)
        {
            context = _context;
            //Desativar tracking global
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            context.Set<T>().Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            context.Set<T>().Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
            .Include(i => i.Lotes)
            .Include(i => i.RedesSociais);
            if (includePalestrantes)
            {
                query = query
                .Include(i => i.PalestrantesEventos)
                .ThenInclude(i => i.Palestrante);
            }
            query = query.OrderByDescending(o => o.DataEvento);

            return await query.AsTracking().ToArrayAsync();
        }

        public async Task<Evento> GetAllEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
            .Include(i => i.Lotes)
            .Include(i => i.RedesSociais);
            if (includePalestrantes)
            {
                query = query
                .Include(i => i.PalestrantesEventos)
                .ThenInclude(i => i.Palestrante);
            }
            query = query.OrderByDescending(o => o.DataEvento)
            .Where(w => w.Id == eventoId);

            return await query.AsTracking().FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventoByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
            .Include(i => i.Lotes)
            .Include(i => i.RedesSociais);
            if (includePalestrantes)
            {
                query = query
                .Include(i => i.PalestrantesEventos)
                .ThenInclude(i => i.Palestrante);
            }
            query = query
            .OrderByDescending(o => o.DataEvento)
            .Where(w => w.Tema.ToLower().Contains(tema.ToLower()));

            return await query.AsTracking().ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
            .Include(i => i.RedesSociais);
            if (includeEventos)
            {
                query = query
                .Include(i => i.PalestrantesEventos)
                .ThenInclude(i => i.Evento);
            }
            query = query.OrderByDescending(o => o.Nome);

            return await query.AsTracking().FirstOrDefaultAsync(m => m.Id == palestranteId);
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
            .Include(i => i.RedesSociais);
            if (includeEventos)
            {
                query = query
                .Include(i => i.PalestrantesEventos)
                .ThenInclude(i => i.Evento);
            }
            query = query.OrderByDescending(o => o.Nome);

            return await query.AsTracking().ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNameAsync(string name, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
            .Include(i => i.RedesSociais);
            if (includeEventos)
            {
                query = query
                .Include(i => i.PalestrantesEventos)
                .ThenInclude(i => i.Evento);
            }
            query = query
            .OrderByDescending(o => o.Nome)
            .Where(w => w.Nome.ToLower().Contains(name.ToLower()));

            return await query.AsTracking().ToArrayAsync();
        }

    }
}