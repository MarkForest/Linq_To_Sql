using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Linq;
using System.Linq;

namespace LinqToSqlDemo
{
    class Program
    {
        static string connectionString = @"Data Source=CR4-00\SQLEXPRESS;Initial Catalog=Library;Integrated Security=true;";
        static void Main(string[] args)
        {
            DataContext db = new DataContext(connectionString);

            //Get table users
            Console.WriteLine("\nGet table users");
            Table<User> users = db.GetTable<User>();
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id}, {user.FirstName}, {user.LastName}");

            }

            Console.WriteLine("\nSorted");
            var query = from u in users
                        where u.LastName == "Messi"
                        orderby u.FirstName
                        select u;
            var query2 = db.GetTable<User>()
                .Where(u => u.LastName == "Messi")
                .OrderBy(u => u.FirstName);

            Console.WriteLine("\nGrouping");
            //var groups = db.GetTable<User>().GroupBy(u => u.Age);
            //foreach (var group in groups)
            //{
            //    Console.WriteLine($"{group.Key}");
            //    foreach (var item in group)
            //    {
            //        Console.WriteLine(group.FirstName);
            //    }
            //    Console.WriteLine();
            //}

            foreach (var item in db.GetTable<User>())
            {
                Console.WriteLine($"{item.FirstName} {item.LastName}");
            }

            User userOne = db.GetTable<User>().FirstOrDefault();
            userOne.FirstName = "Grisha";
            userOne.LastName = "Pixel";
            db.SubmitChanges();

            Console.WriteLine();
            foreach (var item in db.GetTable<User>())
            {
                Console.WriteLine($"{item.FirstName} {item.LastName}");
            }


            //User userTwo = new User { LastName = "Jora", FirstName = "Zeleznie" };
            //db.GetTable<User>().InsertOnSubmit(userTwo);
            //db.SubmitChanges();



            var userJora = (from u in db.GetTable<User>()
                            where u.LastName == "Jora"
                            select u).FirstOrDefault();
            if(userJora != null)
            {
                db.GetTable<User>().DeleteOnSubmit(userJora);
                db.SubmitChanges();
            }


            db.ExecuteCommand("delete from users where lastname = {0}", "Messi");

            foreach (var item in db.ExecuteQuery<User>("select * from users"))
            {
                Console.WriteLine($"{item.FirstName} {item.LastName}");
            }



            Console.Read();
        }
    }
}
