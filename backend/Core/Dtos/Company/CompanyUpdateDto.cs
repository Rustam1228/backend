using backend.Core.Enums;

namespace backend.Core.Dtos.Company
{
    public class CompanyUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CompanySize Size { get; set; }
    }
}
