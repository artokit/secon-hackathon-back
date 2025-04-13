

namespace DataAccess.Models
{
    public class DbRequest
    {
        public Guid Id { get; set; }
        public Guid User_Id { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Extensions_Days { get; set; }
        public DateTime Fact_Date { get; set; }
        public Guid Reason_Id { get; set; }
        public string Comment { get; set; }
    }
}
