using System;

namespace DataAccessDemo.Data.Dto
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
