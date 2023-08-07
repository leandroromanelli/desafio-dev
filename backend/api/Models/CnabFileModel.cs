using Api.Entities;

namespace Api.Models
{
    public class CnabFileModel
    {
        public CnabFileModel(CnabFile cnabFile)
        {
            Name = cnabFile.Name;
            UploadDate = cnabFile.UploadDate;
            CnabEntries = cnabFile.CnabEntries.Select(c => new CnabEntryModel(c));
        }

        public string Name { get; set; }
        public DateTime UploadDate { get; set; }
        public IEnumerable<CnabEntryModel> CnabEntries { get; set; }
    }
}
