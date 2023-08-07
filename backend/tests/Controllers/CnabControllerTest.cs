using Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class CnabControllerTest
    {
        [Fact]
        public void Test1()
        {
            var cnabController = new CnabController(Mocker.GetCnabServiceMock().Object);

            Assert.IsType<ContentResult>(cnabController.List().Result);
            Assert.IsType<ContentResult>(cnabController.Get("").Result);
            Assert.IsType<ContentResult>(cnabController.Upload(Mocker.GetFormFileMock().Object).Result);
        }
    }
}