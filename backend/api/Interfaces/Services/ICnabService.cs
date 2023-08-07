using Api.Entities;

namespace Api.Interfaces.Services
{
    public interface ICnabService
    {
        CnabFile ImportFile(IFormFile file);
        IEnumerable<CnabFile> List();
        IEnumerable<CnabFile> Get(string fileName);
    }
}
