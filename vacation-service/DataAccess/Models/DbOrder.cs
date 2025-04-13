using Common;

namespace DataAccess.Models
{
    public class DbOrder
    {
        public Guid Id { get; set; }
        public Guid Creator_Id { get; set; }
        public Guid Departament_Id { get; set; }
        public DateTime Created_Data { get; set; }
        public OrderStatus Status { get; set; }
    }
}
