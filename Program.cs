using System;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Exercise_Lambda_LINQ2.Entities;

namespace Exercise_Lambda_LINQ2 {
    class Program {
        static void Main(string[] args) {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            List<Employee> list = new List<Employee>();

            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();



            try {
                using (StreamReader sr = File.OpenText(path)) {
                    while (!sr.EndOfStream) {
                        string[] v = sr.ReadLine().Split(',');
                        string name = v[0];
                        string email = v[1];
                        double salary = double.Parse(v[2]);

                        list.Add(new Employee(name, email, salary));
                    }
                }

                Console.Write("Enter salary: ");
                double sal = double.Parse(Console.ReadLine());

                Console.WriteLine($"Email of people whose salary is more than {sal.ToString("F2")}: ");

                var emails = list.Where(obj => obj.Salary > sal).OrderBy(obj => obj.Email).Select(obj => obj.Email);
                var sum = list.Where(obj => obj.Name.StartsWith("M")).Sum(obj => obj.Salary);

                foreach (var item in emails) {
                    Console.WriteLine(item);
                }

                Console.WriteLine($"Sum of salary of people whose name starts with 'M': {sum.ToString("F2")}");

            } catch (IOException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
