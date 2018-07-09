using System;

/**** This is test of UserManagementAPI requests ****/

namespace RecipeConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        { 

            Console.Title = "RecipeConsoleUI";

            var client = new UmApiClient();

            Console.WriteLine("Enter register or sign in");

            var input = Console.ReadLine();

            if(input == "register")
            {
                var userRegInfo = new UserRegisterInfo();
                Console.WriteLine("FistName");
                userRegInfo.FirstName = Console.ReadLine();
                Console.WriteLine("MiddleName");
                userRegInfo.MiddleName = Console.ReadLine();
                Console.WriteLine("LastName");
                userRegInfo.LastName = Console.ReadLine();
                Console.WriteLine("Username");
                userRegInfo.Username = Console.ReadLine();
                Console.WriteLine("Email");
                userRegInfo.Email = Console.ReadLine();
                Console.WriteLine("Sex");
                userRegInfo.Sex = Console.ReadLine();
                Console.WriteLine("Phone");
                userRegInfo.Phone = Console.ReadLine();
                Console.WriteLine("Birthdate");
                userRegInfo.Birthdate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Password");
                userRegInfo.Password = Console.ReadLine();
                client.RegisterUser(userRegInfo);
            }
            else
            {
                Console.WriteLine("Username");
                var username = Console.ReadLine();
                Console.WriteLine("Password");
                var password = Console.ReadLine();
                client.LogIn(username, password);
            }

            var list = client.GetUserPublicInfos();

            client.AddDoctorProfile(new Doctor
                {
                    HospitalId = 1,
                    License = "Doctor"
                }
                );
        }
    }
}
