using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace LinqToSqlDemo
{
    [Table(Name ="users")]
    public class User
    {
        [Column(Name ="id", IsPrimaryKey = true, IsDbGenerated =true)]
        public int Id { get; set; }
        [Column(Name ="firstname")]
        public string FirstName { get; set; }
        [Column(Name ="lastname")]
        public string LastName { get; set; }
    }
}
