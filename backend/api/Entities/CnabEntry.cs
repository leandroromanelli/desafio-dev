using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class CnabEntry
    {
        public CnabEntry()
        {
        }

        public CnabEntry(string type,
                         string symbol,
                         DateTime date,
                         decimal value,
                         string document,
                         string card,
                         string storeOwner,
                         string storeName) : this()
        {
            Type = type;
            Symbol = symbol;
            Date = date;
            Value = value;
            Document = document;
            Card = card;
            StoreOwner = storeOwner;
            StoreName = storeName;
        }

        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; } = "";

        public string Symbol { get; set; } = "";

        public DateTime Date { get; set; }

        public decimal Value { get; set; }

        public string Document { get; set; } = "";

        public string Card { get; set; } = "";

        public string StoreOwner { get; set; } = "";

        public string StoreName { get; set; } = "";

        public Guid CnabFileId { get; set; }
        public CnabFile CnabFile { get; set; }
    }
}
