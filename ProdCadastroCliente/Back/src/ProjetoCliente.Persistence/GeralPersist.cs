
using System.Threading.Tasks;
using ProjetoCliente.Persistence.Contextos;
using ProjetoCliente.Persistence.Contratos;

namespace ProjetoCliente.Persistence
{
    public class GeralPersist : IGeralPersist
    {

        public ProjetoClientesContext _context;

        public GeralPersist(ProjetoClientesContext context)
        {
            _context = context;
            
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
           _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArry) where T : class
        {
           _context.RemoveRange(entityArry);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0; 
        }
    }
}