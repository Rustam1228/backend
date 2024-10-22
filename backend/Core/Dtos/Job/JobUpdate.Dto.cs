using backend.Core.Enums;

namespace backend.Core.Dtos.Job
{
    public class JobUpdateDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public JobLevel Level { get; set; }
        public long CompanyId { get; set; }
    }
}

