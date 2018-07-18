using IdentityModel.Client;
using System;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace UserManagementApiClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // creating client
            var client = new UserManagementApiClient("http://localhost:5700", "http://localhost:5800");

            // registering user
            var registerResponse = client.RegisterAsync(
                new UserRegisterInfo
                {
                    FirstName = "Sevak",
                    LastName = "Amirkhanian",
                    MiddleName = "John",
                    FullName = "Sevak John Amirkhanian",
                    Birthdate = "1999-02-13",
                    Email = "amirkhanyan.sevak@gmail.com",
                    Sex = "M",
                    Password = "password",
                    Phone = "24455400",
                    Username = "sev"
                }).Result;

            // verifying
            var key = Console.ReadLine();
            var verifyResponse = client.VerifyAsync(
                new UserVerificationInfo
                {
                    Username = "sev",
                    VerifyKey = key
                }).Result;

            // sign in
            client.SignInAsync("sev", "password");

            // getting user info
            var userInfoResponse = client.GetUserByUsernameAsync("sev").Result;

            // adding doctor profile
            var doctorProfileAddResponse = client.PostDoctorAsync(
                new Doctor
                {
                    UserId = userInfoResponse.Result.Id,
                    GraduatedYear = "2008",
                    WorkStartYear = "2008",
                    Specification = "Neurologist",
                    HospitalName = "Armenia",
                    License = "doctor_license"
                }).Result;

            // updating doctor profile
            var doctorProfileUpdateResponse = client.PutDoctorAsync(
                new DoctorUpdateInfo
                {
                    HospitalName = "Astghik",
                    Specification = "Neurologist",
                    License = "doctor_license",
                    UserId = userInfoResponse.Result.Id
                }).Result;

            // adding hospital director profile
            var hospitalDirectorProfileAddResponse = client.PostHospitalDirectorAsync(
                new HospitalDirector
                {
                    StartedWorking = "2007",
                    HospitalName = "Astghik",
                    Occupation = "Neurologist"
                }).Result;

            // updating hospital director profile
            var hospitalDirectorUpdateProfileResponse = client.PutHospitalDirectorAsync(
                new HospitalDirector
                {
                    StartedWorking = "2007",
                    HospitalName = "Armenia",
                    Occupation = "Neurologist"
                }).Result;

            // adding ministry worker profile
            var ministryWorkerAddProfileResponse = client.PostMinistryWorkerAsync(
                new MinistryWorker
                {
                    StartedWorking = "2005",
                    Position = "Director"
                }).Result;

            // updating ministry worker profile
            var ministryWorkerUpdateProfileResponse = client.PutMinistryWorkerAsync(
                new MinistryWorker
                {
                    StartedWorking = "2004",
                    Position = "Minister"

                }).Result;

            // adding patient profile
            var patientProfileAddResponse = client.PostPatientAsync(
                new Patient
                {
                    Address = "somewhere",
                    IsAlcoholic = false,
                    IsDrugAddicted = false,
                    Occupation = "Programmer",
                    RegionalDoctorName = "someone",
                    UserId = userInfoResponse.Result.Id
                }).Result;

            // updating patient profile
            var patientProfileUpdateResponse = client.PutPatientAsync(
                new Patient
                {
                    Address = "somewhere",
                    IsAlcoholic = false,
                    IsDrugAddicted = false,
                    Occupation = "Programmer",
                    RegionalDoctorName = "somebody",
                    UserId = userInfoResponse.Result.Id
                }).Result;

            // adding pharmacist profile
            var pharmacistProfileAddResponse = client.PostPharmacistAsync(
                new PharmacistFullInfo
                {
                    PharmacyId = 1,
                    PharmacyName = "Some_Pharmacy",
                    StartedWorking = "2007",
                    UserId = userInfoResponse.Result.Id
                }).Result;

            // updating pharmacist profile
            var pharmacistProfileUpdateResponse = client.PutPharmacistAsync(
                new PharmacistFullInfo
                {
                    PharmacyId = 5,
                    PharmacyName = "Another_pharmacy",
                    StartedWorking = "2005",
                    UserId = userInfoResponse.Result.Id
                }).Result;


            // updating profile
            var updateProfileResponse = client.UpdateCurrentProfileAsync(
                new ProfileUpdateInfo
                {
                    Id = userInfoResponse.Result.Id,
                    Profile = "doctor"
                }).Result;

            // updating user password
            var updatePasswordResponse = client.UpdatePasswordAsync(
                new PasswordUpdateInfo
                {
                    Id = userInfoResponse.Result.Id,
                    OldPassword = "password",
                    NewPassword = "newPassword"
                }).Result;

            // deleting profiles
            var deleteDoctorProfile = client.DeleteDoctorAsync().Result;
            var deletePatientProfile = client.DeletePatientAsync().Result;
            var deleteMinistryWorker = client.DeleteMinistryWorkerAsync().Result;
            var deleteHospitalDirector = client.DeleteHospitalDirectorAsync().Result;
            var deletePharmacist = client.DeletePharmacistAsync().Result;
        }
    }
}
