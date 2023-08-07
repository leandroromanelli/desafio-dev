using Api.Entities;
using Newtonsoft.Json;

namespace Api.Models
{
    public class CnabEntryModel
    {
        public CnabEntryModel(CnabEntry cnab)
        {
            Type = cnab.Type;
            Symbol = cnab.Symbol;
            Date = cnab.Date;
            Value = cnab.Value;
            Document = cnab.Document;
            Card = cnab.Card;
            StoreOwner = cnab.StoreOwner;
            StoreName = cnab.StoreName;
        }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("card")]
        public string Card { get; set; }

        [JsonProperty("storeOwner")]
        public string StoreOwner { get; set; }

        [JsonProperty("storeName")]
        public string StoreName { get; set; }

        public CnabEntry ToEntity()
        {
            return new CnabEntry()
            {
                Type = Type,
                Symbol = Symbol,
                Date = Date,
                Value = Value,
                Document = Document,
                Card = Card,
                StoreOwner = StoreOwner,
                StoreName = StoreName
            };
        }

    }
}
