using Api.Entities;

namespace Api.Interfaces.Repositories
{
    public interface ICnabRepository
    {
        public void AddCnab(CnabFile cnabFile);
        IEnumerable<CnabFile> List();
        IEnumerable<CnabFile> Get(string fileName);
    }
}
