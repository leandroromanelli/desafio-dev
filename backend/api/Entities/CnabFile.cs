using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class CnabFile
    {
        public CnabFile()
        {
            CnabEntries = new List<CnabEntry>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime UploadDate { get; set; }
        public List<CnabEntry> CnabEntries { get; set; }
    }
}
