using Api.Contexts;
using Api.Entities;
using Api.Interfaces.Repositories;
using Api.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests
{
    public static class Mocker
    {
        public static Mock<DbSet<T>> GetMockSet<T>(this IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns<T>(x => data.GetEnumerator());

            return mockSet;
        }

        public static Mock<CnabContext> GetCnabContextMock()
        {
            var result = new Mock<CnabContext>();

            result.Setup(m => m.CnabEntries).Returns(GenerateCnabEntries(10).AsQueryable().GetMockSet().Object);
            result.Setup(m => m.CnabFiles).Returns(new List<CnabFile>() { GenerateCnabFile(true) }.AsQueryable().GetMockSet().Object);
            result.Setup(m => m.SaveChanges()).Returns(1);

            return result;
        }

        public static CnabFile CnabFileMocked => new CnabFile()
        {
            Id = CnabFileMockedId,
            Name = "CnabFile1",
            UploadDate = CnabFileMockedUploadDate
        };

        public static Guid CnabFileMockedId => Guid.Parse("26b2a2c4-ebe1-4220-8e32-5c06a4a45cec");
        public static DateTime CnabFileMockedUploadDate => DateTime.Parse("2023-06-25T10:10:54Z");

        public static Mock<IFormFile> GetFormFileMock()
        {
            var result = new Mock<IFormFile>();

            result.Setup(m => m.FileName).Returns("CnabFile1");
            result.Setup(m => m.OpenReadStream()).Returns(Stream.Null);

            return result;
        }

        public static List<CnabEntry> GenerateCnabEntries(int ammount)
        {
            var result = new List<CnabEntry>();

            for (int i = 0; i < ammount; i++)
            {
                result.Add(new CnabEntry()
                {
                    Card = i.ToString(),
                    Date = CnabFileMockedUploadDate,
                    Document = i.ToString(),
                    StoreName = i.ToString(),
                    StoreOwner = i.ToString(),
                    Symbol = i % 2 == 0 ? "+" : "-",
                    Type = i.ToString(),
                    Value = i
                });
            }

            return result;
        }

        public static CnabFile GenerateCnabFile(bool withEntries)
        {
            var result = CnabFileMocked;

            if (withEntries)
                result.CnabEntries = GenerateCnabEntries(10);

            return result;
        }

        public static Mock<ICnabRepository> GetCnabRepositoryMock()
        {
            var mock = new Mock<ICnabRepository>();

            mock.Setup(m => m.Get(It.IsAny<string>())).Returns(new List<CnabFile>() { GenerateCnabFile(true) });
            mock.Setup(m => m.List()).Returns(new List<CnabFile>() { GenerateCnabFile(false) });
            mock.Setup(m => m.AddCnab(GenerateCnabFile(true)));

            return mock;
        }

        public static Mock<ICnabService> GetCnabServiceMock()
        {
            var mock = new Mock<ICnabService>();

            mock.Setup(m => m.Get(It.IsAny<string>())).Returns(new List<CnabFile>() { GenerateCnabFile(true) });
            mock.Setup(m => m.List()).Returns(new List<CnabFile>() { GenerateCnabFile(false) });
            mock.Setup(m => m.ImportFile(It.IsAny<IFormFile>())).Returns(GenerateCnabFile(true));

            return mock;
        }
    }
}
