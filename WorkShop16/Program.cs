//namespace WorkShop16
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            List<StudentModel> students = PostgresDataAccess.LoadStudents();

//            Console.WriteLine($"Number of students enrolled: {students.Count}");
//            foreach (StudentModel student in students)
//            {
//                Console.WriteLine($"Hello!! Wellcome {student.first_name} {student.last_name}");
//            }
//        }
//    }
//}


using System;
using WorkShop16;

class Menu
{
    static void Main()
    {
        // Set initial selection to option 1
        int selection = 1;

        // Loop until user selects "Terminate"
        while (selection != 0)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the database! Please select one of the options below:");
            Console.WriteLine();

            // Display menu options
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("1. List students");
            Console.WriteLine("2. List courses");
            Console.WriteLine("3. Create student");
            Console.WriteLine("4. Create course");
            Console.WriteLine("5. Change password");
            Console.WriteLine("6. Edit course");
            Console.WriteLine("7. Delete course");
            Console.WriteLine("A. Terminate");

            // Highlight selected option
            switch (selection)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("1. List students");
                    //Console.WriteLine("Here is student list");
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("2. List courses");
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("3. Create student");
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("4. Create course");
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("5. Change password");
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("6. Edit course");
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("7. Delete course");
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("A. Terminate");
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Gray;

            // Wait for user input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            // Update selection based on user input
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selection = (selection == 1) ? 8 : selection - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selection = (selection == 8) ? 1 : selection + 1;
                    break;
                case ConsoleKey.Enter:
                    switch (selection)
                    {
                        case 1:
                            
                            PostgresDataAccess.ListStudents();

                            break;
                        case 2:
                            PostgresDataAccess.ListCourses();
                            break;
                        case 3:
                            PostgresDataAccess.CreateStudents();
                            break;
                        case 4:
                            PostgresDataAccess.CreateCourses();
                           // Console.WriteLine("Selected option 4 - Create course");
                            break;
                        case 5:
                            Console.WriteLine("Selected option 5 - Change password");
                            break;
                        case 6:
                            Console.WriteLine("Selected option 6 - Edit course");
                            break;
                        case 7:
                            Console.WriteLine("Selected option 7 - Delete course");
                            break;
                        case 8:
                            selection = 0;
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Press any key to continue...");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    break;
            }
        }
    }
}
