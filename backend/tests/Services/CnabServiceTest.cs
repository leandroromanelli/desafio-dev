using Api.Entities;
using Api.Services;

namespace Tests
{
    public class CnabServiceTest
    {
        [Fact]
        public void Test1()
        {
            var cnabService = new CnabService(Mocker.GetCnabRepositoryMock().Object);

            Assert.IsAssignableFrom<IEnumerable<CnabFile>>(cnabService.List());
            Assert.IsAssignableFrom<IEnumerable<CnabFile>>(cnabService.Get(""));
            Assert.IsAssignableFrom<CnabFile>(cnabService.ImportFile(Mocker.GetFormFileMock().Object));
        }
    }
}