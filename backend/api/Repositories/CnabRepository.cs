using Api.Contexts;
using Api.Entities;
using Api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class CnabRepository : ICnabRepository
    {
        private readonly CnabContext _context;

        public CnabRepository(CnabContext context)
        {
            _context = context;
        }

        public void AddCnab(CnabFile cnabFile)
        {
            _context.CnabFiles.Add(cnabFile);
            _context.SaveChanges();
        }

        public IEnumerable<CnabFile> List()
        {
            return _context.CnabFiles;
        }

        public IEnumerable<CnabFile> Get(string fileName)
        {
            return _context.CnabFiles
                .Where(c => c.Name == fileName)
                .Include(c => c.CnabEntries);
        }
    }
}
