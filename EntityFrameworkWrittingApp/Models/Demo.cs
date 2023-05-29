using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkWrittingApp.Models
{
    [Table("tblDemo")]

    public class Demo
    {
        public long Id { get; set; }
        public string DemoName { get; set; }
        public double Price { get; set; }
    }
}
