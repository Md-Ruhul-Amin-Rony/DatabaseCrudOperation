using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkShop16
{
    public class PostgresDataAccess
    {
        public static void SaveStudent(StudentModel student)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into mra_student (first_name, last_name, email) values (@first_name, @last_name, @email)", student);

            }
        }

        public static void SaveCourse(CourseModel course)
        {
            using (IDbConnection cnn= new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO mra_course (name, points, start_date, end_date) values (@name, @points, @start_date, @end_date)", course);
            }
        }


        public static List<StudentModel> LoadStudents()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                //cnn.Open();
                var output = cnn.Query<StudentModel>("select * from mra_student", new DynamicParameters());



                return output.ToList();
                //cnn.Close();
            }

        }
        public static List<CourseModel> LoadCourses()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<CourseModel>("SELECT * FROM mra_course", new DynamicParameters());
                return output.ToList();
            }
        }

        
        public static void ListStudents()
        {
            Console.Clear();
            Console.WriteLine("Selected option 1 - List students");
            List<StudentModel> students = LoadStudents();
            foreach (var student in students)
            {
                Console.WriteLine($"Welcome {student.first_name} {student.last_name} and your email address is {student.email} and you are {student.age} years older.");
            }

        }

        public static void ListCourses()
        {
            Console.Clear();
            Console.WriteLine("Selected option 2- List courses:");
            List<CourseModel> courses = LoadCourses();
            foreach (var course in courses)
            {
                Console.WriteLine($"Course name is {course.name} and course point is {course.points} and course start date is {course.StartDateShort} and end date is {course.EndDateShort} and total duration is {course.DurationInDays} days.");
            }
        }
       
        
        public static void CreateStudents()
        {
            Console.Clear();
            Console.WriteLine("Selected option 3- create students:");
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Open();
                    Console.WriteLine("Enter your First Name:");
                    string first_name = Console.ReadLine().ToLower();
                    Console.WriteLine("Enter your Last Name:");
                    string last_name = Console.ReadLine().ToLower();
                
                    
                        
                            Console.WriteLine("Enter your email:");
                            string email = Console.ReadLine();
                            Console.WriteLine("Enter your desired password:");
                            string password = Console.ReadLine();
                    Console.WriteLine(  "Enter your age:");
                    int age = int.Parse(Console.ReadLine());
                    // with crosscheck email
                            string check = "SELECT COUNT(*) FROM mra_student WHERE email = @email";
                            int count = cnn.ExecuteScalar<int>(check, new { email });
                            if (count > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("The email address is already in use.");
                                Console.ResetColor();
                                Console.WriteLine();
                                return;
                            }
                            string sql = "INSERT INTO mra_student (first_name, last_name, email, age, password) " +
                                         "VALUES (@first_name, @last_name, @email, @age, @password)";
                            cnn.Execute(sql, new { first_name, last_name, email, age, password });
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("New student created successfully!");
                            Console.WriteLine();
                            Console.ResetColor();
                            Console.WriteLine("Press enter to go to main");
                            Console.ReadKey();
                            Console.Clear();


                        
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input. Please enter a valid input");
                }
            }
        }


        public static void CreateCourses()
        {
            Console.Clear();
            Console.WriteLine("Selected option 4- create courses:");
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Open();
                    Console.WriteLine("Enter course Name:");
                    string name = Console.ReadLine().ToLower();
                    Console.WriteLine("Enter points the course consist of:");
                    int points = int.Parse(Console.ReadLine());



                    Console.WriteLine("Enter course start date (yyyy-MM-dd):");
                    string startDateStr = Console.ReadLine();
                    DateTime start_date = DateTime.Parse(startDateStr);

                    Console.WriteLine("Enter course start date (yyyy-MM-dd):");
                    string endDateStr = Console.ReadLine();
                    DateTime end_date = DateTime.Parse(endDateStr);


                    
                    // with crosscheck name
                    string check = "SELECT COUNT(*) FROM mra_course WHERE name = @name";
                    int count = cnn.ExecuteScalar<int>(check, new { name });
                    if (count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("The course name is already in use.");
                        Console.ResetColor();
                        Console.WriteLine();
                        return;
                    }
                    string sql = "INSERT INTO mra_course (name, points, start_date, end_date) " +
                                 "VALUES (@name, @points, @start_date, @end_date)";
                    cnn.Execute(sql, new { name, points, start_date, end_date });
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("New course created successfully!");
                    Console.WriteLine();
                    Console.ResetColor();
                    Console.WriteLine("Press enter to go to main");
                    Console.ReadKey();
                    Console.Clear();



                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input. Please enter a valid input");
                }
            }
        }
        public static void ChangePasswordByEmail()
        {
            Console.Clear();
            Console.WriteLine("Selected option 5- Update password:");
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Open();
                Console.WriteLine("Enter your email:");
                string email = Console.ReadLine().ToLower();
                Console.WriteLine("Enter your new password");
                string newPassword = Console.ReadLine();

                int num = cnn.Execute($"UPDATE mra_student SET password = '{newPassword}' WHERE email = '{email}'");

                if (num == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"No student found with email '{email}'.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Password updated successfully for student with email '{email}'.");
                    Console.ResetColor();
                }
            }
        }
        public static void EditCourse()
        {
            Console.Clear();
            Console.WriteLine("Selected option 6- Edit Course:");
            List<CourseModel> courses = LoadCourses();

            Console.WriteLine("Enter the course id you want to change:");
            int id = int.Parse(Console.ReadLine());

            // Find the course with the given id
            CourseModel course = courses.Find(c => c.id == id);

            if (course == null)
            {
                Console.WriteLine($"No course with id {id} was found.");
            }
            else
            {
                Console.WriteLine($"The course you want to edit is : {course.name} and course points is {course.points}, course startdate is {course.StartDateShort} and enddate is {course.EndDateShort}.");
              

                // Prompt the user for the new course name and points
                Console.WriteLine("Enter the new course name:");
                string newName = Console.ReadLine();
                Console.WriteLine("Enter the new course points:");
                int newPoints = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter course start date (yyyy-MM-dd):");
                string startDateStr = Console.ReadLine();
                DateTime start_date = DateTime.Parse(startDateStr);

                Console.WriteLine("Enter course start date (yyyy-MM-dd):");
                string endDateStr = Console.ReadLine();
                DateTime end_date = DateTime.Parse(endDateStr);

                // Update the course in the database
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    string query = "UPDATE mra_course SET name = @Name, points = @Points, start_date =@StartDate,end_date=@EndDate  WHERE id = @Id";

                    int rowsAffected = cnn.Execute(query, new
                    {
                        Name = newName,
                        Points = newPoints,
                        Id = id,
                        StartDate = start_date,
                        endDate = end_date
                    }) ;

                    if (rowsAffected == 0)
                    {
                        Console.WriteLine($"No course with id {id} was found.");
                    }
                    else
                    {
                        Console.WriteLine($"Course with id {id} was updated successfully.");
                    }
                }
            }
        }





        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
