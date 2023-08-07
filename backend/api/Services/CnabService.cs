using Api.Entities;
using Api.Interfaces.Repositories;
using Api.Interfaces.Services;

namespace Api.Services
{
    public class CnabService : ICnabService
    {
        private readonly ICnabRepository _cnabRepository;

        public CnabService(ICnabRepository cnabRepository)
        {
            _cnabRepository = cnabRepository;
        }

        private static CnabEntry FromText(string cnabLine)
        {
            if(cnabLine.Length < 80)
            {
                throw new Exception("invalid cnab file");
            }

            var strType = cnabLine[..1].Trim();

            if (!int.TryParse(strType, out var intType))
            {
                throw new Exception("invalid cnab file");
            }

            string? type;
            string? symbol;

            switch (intType)
            {
                case 1:
                    type = "Débito - Entrada";
                    symbol = "+";
                    break;
                case 2:
                    type = "Boleto - Saída";
                    symbol = "-";
                    break;
                case 3:
                    type = "Financiamento - Saída";
                    symbol = "-";
                    break;
                case 4:
                    type = "Crédito - Entrada";
                    symbol = "+";
                    break;
                case 5:
                    type
                        = "Recebimento Empréstimo - Entrada";
                    symbol = "+";
                    break;
                case 6:
                    type = "Vendas - Entrada";
                    symbol = "+";
                    break;
                case 7:
                    type
                        = "Recebimento TED - Entrada";
                    symbol = "+";
                    break;
                case 8:
                    type
                        = "Recebimento DOC - Entrada";
                    symbol = "+";
                    break;
                case 9:
                    type = "Aluguel - Saída";
                    symbol = "-";
                    break;
                default:
                    throw new Exception("invalid cnab file");

            }

            var strDate = cnabLine.Substring(1, 8).Trim();
            var strTime = cnabLine.Substring(42, 6).Trim();

            if (!int.TryParse(strDate, out _) || !int.TryParse(strTime, out _))
            {
                throw new Exception("invalid cnab file");
            }

            var year = int.Parse(strDate[..4]); 
            var month = int.Parse(strDate.Substring(4,2)); 
            var day = int.Parse(strDate.Substring(6,2)); 
            var hour = int.Parse(strTime[..2]); 
            var minute = int.Parse(strTime.Substring(2,2));
            var second = int.Parse(strTime.Substring(4, 2));

            var date = new DateTime(year, month, day, hour, minute, second);

            var strValue = cnabLine.Substring(9, 10).Trim();

            if(!decimal.TryParse(strValue, out var value))
            {
                throw new Exception("invalid cnab file");
            }

            var document = cnabLine.Substring(19, 11).Trim();
            var card = cnabLine.Substring(30, 12).Trim();
            var storeOwner = cnabLine.Substring(48, 14).Trim();
            var storeName = cnabLine.Substring(62, 18).Trim();

            var result = new CnabEntry(type, symbol, date, value, document, card, storeOwner, storeName);

            return result;
        }

        public CnabFile ImportFile(IFormFile file)
        {
            var cnabFile = new CnabFile()
            {
                CnabEntries = new List<CnabEntry>(),
                Name = file.FileName,
                UploadDate = DateTime.Now
            };

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var text = reader.ReadLine();

                    if (text == null || text.Trim().Length == 0)
                    {
                        continue;
                    }

                    cnabFile.CnabEntries.Add(FromText(text));
                }
            }

            _cnabRepository.AddCnab(cnabFile);

            return cnabFile;
        }

        public IEnumerable<CnabFile> List()
        {
            return _cnabRepository.List();
        }


        public IEnumerable<CnabFile> Get(string fileName)
        {
            return _cnabRepository.Get(fileName);
        }

    }
}