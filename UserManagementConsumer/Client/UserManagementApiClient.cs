using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AuthTokenService;
using Newtonsoft.Json;
using UserManagementConsumer.Models;

namespace UserManagementConsumer.Client
{
    /// <summary>
    /// Class for consuming User Management API
    /// </summary>
    public class UserManagementApiClient : IDisposable
    {
        /// <summary>
        /// User Management Base address
        /// </summary>
        private readonly string _userApiBaseAddress;

        /// <summary>
        /// Http client for consuming User Management API
        /// </summary>
        private readonly HttpClient _userApiHttpClient;

        /// <summary>
        /// Http client for register requests
        /// </summary>
        private readonly HttpClient _registerClient;

        /// <summary>
        /// Access token
        /// </summary>
        private string _accessToken;

        /// <summary>
        /// Creates new instance of <see cref="UserManagementApiClient"/>
        /// </summary>
        /// <param name="userApiAddress">User Management API address</param>
        public UserManagementApiClient(string userApiAddress)
        {
            // setting fields
            this._userApiBaseAddress = userApiAddress;

            // constructing register client
            this._registerClient = new HttpClient();
            this._registerClient.BaseAddress = new Uri(this._userApiBaseAddress);
            this._registerClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // constructing user API client
            this._userApiHttpClient = new HttpClient();
            this._userApiHttpClient.BaseAddress = new Uri(this._userApiBaseAddress);
            this._userApiHttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="userRegisterInfo">User registration data</param>
        /// <returns>response</returns>
        public async Task<Response<HttpResponseMessage>> RegisterAsync(UserRegisterInfo userRegisterInfo)
        {
            // getting response
            var response = await this._registerClient.PostAsync("api/register",this.ConstructContent(userRegisterInfo));

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse(Status.Error, response);

            // reading response
            var responseString = await response.Content.ReadAsStringAsync();

            // returning response
            return this.ConstructResponse(Status.Ok, response);
        }

        /// <summary>
        /// Verifies user
        /// </summary>
        /// <param name="userVerificationInfo">User verification information</param>
        /// <returns>response</returns>
        public async Task<Response<HttpResponseMessage>> VerifyAsync(UserVerificationInfo userVerificationInfo)
        {
            // getting response
            var response = await this._registerClient.PutAsync("api/register/verify",this.ConstructContent(userVerificationInfo));

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse(Status.Error, response);

            // reading content
            var content = await response.Content.ReadAsStringAsync();

            // returning response
            return this.ConstructResponse(Status.Ok, response);
        }

        /// <summary>
        /// Gets users public info
        /// </summary>
        /// <returns>result</returns>
        public async Task<Response<IEnumerable<UserPublicInfo>>> GetUsersAsync()
        {
            // getting response
            var response = await this._userApiHttpClient.GetAsync("api/users");

            // if error occured inform about that
            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<IEnumerable<UserPublicInfo>>(Status.Error ,null);

            // reading response
            var content = await response.Content.ReadAsStringAsync();

            // content is empty inform about that
            if (string.IsNullOrEmpty(content))
                return this.ConstructResponse<IEnumerable<UserPublicInfo>>(Status.Error, null);

            // returning result
            return this.ConstructResponse(
                Status.Ok, JsonConvert.DeserializeObject<IEnumerable<UserPublicInfo>>(content));
        }

        /// <summary>
        /// Gets user public info by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Response<UserPublicInfo>> GetUserByUsernameAsync(string username)
        {
            // getting response
            var response = await this._userApiHttpClient.GetAsync($"api/users/{username}");

            // if error occured inform about that
            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<UserPublicInfo>(Status.Error, null);

            // reading content
            var content = await response.Content.ReadAsStringAsync();

            // if no content inform about that
            if (string.IsNullOrEmpty(content))
                return this.ConstructResponse<UserPublicInfo>(Status.Error, null);

            // returning result
            return this.ConstructResponse(
                Status.Ok ,JsonConvert.DeserializeObject<UserPublicInfo>(content));
        }

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User personal info</returns>
        public async Task<Response<UserPersonalInfo>> GetUserAsync(int id)
        {
            // getting response
            var response = await this._userApiHttpClient.GetAsync($"api/users/{id}");

            // if error occured inform about that
            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<UserPersonalInfo>(Status.Error, null);

            // reading content
            var content = await response.Content.ReadAsStringAsync();

            // if no content inform about that
            if (string.IsNullOrEmpty(content))
                return this.ConstructResponse<UserPersonalInfo>(Status.Error, null);

            // returning result
            return this.ConstructResponse(
                Status.Ok, JsonConvert.DeserializeObject<UserPersonalInfo>(content));
        }

        /// <summary>
        /// Gets user profiles by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>profiles</returns>
        public async Task<Response<IEnumerable<Profile>>> GetUserProfilesAsync(int id)
        {
            // getting response
            var response = await this._userApiHttpClient.GetAsync($"api/profiles/{id}");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<IEnumerable<Profile>>(Status.Error, null);

            // reading content
            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content))
                return this.ConstructResponse<IEnumerable<Profile>>(Status.Ok, null);

            // returning result
            return this.ConstructResponse(
                Status.Ok, JsonConvert.DeserializeObject<IEnumerable<Profile>>(content));

        }

        /// <summary>
        /// Gets user profiles by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>action result</returns>
        public async Task<Response<IEnumerable<Profile>>> GetUserProfilesAsync(string username)
        {
            // getting response
            var response = await this._userApiHttpClient.GetAsync($"api/profiles/{username}");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<IEnumerable<Profile>>(Status.Error, null);

            // reading content
            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content))
                return this.ConstructResponse<IEnumerable<Profile>>(Status.Ok, null);

            // returning result
            return this.ConstructResponse(
                Status.Ok, JsonConvert.DeserializeObject<IEnumerable<Profile>>(content));
        }

        /// <summary>
        /// Gets unapproved profiles by type
        /// </summary>
        /// <param name="type">Profile type.Should be in lowletters</param>
        /// <returns>enumerable of profiles</returns>
        public async Task<Response<IEnumerable<Profile>>> GetUnapprovedProfilesByTypeAsync(string type)
        {
            // getting response
            var response = await this._userApiHttpClient.GetAsync($"api/profiles/unapproved/{type}");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<IEnumerable<Profile>>(Status.Error, null);

            // reading content
            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content))
                return this.ConstructResponse<IEnumerable<Profile>>(Status.Ok, null);

            // returning result
            return this.ConstructResponse(
                Status.Ok, JsonConvert.DeserializeObject<IEnumerable<Profile>>(content));
        }

        /// <summary>
        /// Updates password
        /// </summary>
        /// <param name="passwordUpdateInfo">password update info</param>
        /// <returns>response</returns>
        public async Task<Response<string>> UpdatePasswordAsync(PasswordUpdateInfo passwordUpdateInfo)
        {
            var response = await this._userApiHttpClient.PutAsync("api/users/password", this.ConstructContent(passwordUpdateInfo));

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse(Status.Error, response.ReasonPhrase);

            return this.ConstructResponse(Status.Ok, await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Updates current profile
        /// </summary>
        /// <param name="profileUpdateInfo">Profile update info</param>
        /// <returns>response</returns>
        public async Task<Response<string>> UpdateCurrentProfileAsync(ProfileUpdateInfo profileUpdateInfo)
        {
            var response = await this._userApiHttpClient.PutAsync("api/users/profile", this.ConstructContent(profileUpdateInfo));

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse(Status.Error, response.ReasonPhrase);            

            return this.ConstructResponse(Status.Ok, await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Deletes user by id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>response</returns>
        public async Task<Response<string>> DeleteUserAsync(int id)
        {
            var response = await this._userApiHttpClient.DeleteAsync($"api/users/{id}");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse(Status.Error, response.ReasonPhrase);

            return this.ConstructResponse(Status.Ok, await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Gets doctors
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<IEnumerable<DoctorPublicInfo>>> GetDoctorsAsync()
        {
            return await this.GetProfilesByTypeAsync<DoctorPublicInfo>("doctors");
        }

        /// <summary>
        /// Gets doctor profile by user id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>response</returns>
        public async Task<Response<Doctor>> GetDoctorByIdAsync(int id)
        {
            return await this.GetProfileByTypeAndIdAsync<Doctor>("doctors", id);
        }

        /// <summary>
        /// Gets patients
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<IEnumerable<Patient>>> GetPatientsAsync()
        {
            return await this.GetProfilesByTypeAsync<Patient>("patients");
        }

        /// <summary>
        /// Gets patient profile by user id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>response</returns>
        public async Task<Response<Patient>> GetPatientByIdAsync(int id)
        {
            return await this.GetProfileByTypeAndIdAsync<Patient>("patients", id);
        }

        /// <summary>
        /// Gets hospital directors
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<IEnumerable<HospitalDirector>>> GetHospitalDirectorsAsync()
        {
            return await this.GetProfilesByTypeAsync<HospitalDirector>("hospital-directors");
        }

        /// <summary>
        /// Gets hospital director profile by user id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>response</returns>
        public async Task<Response<HospitalDirector>> GetHospitalDirectorByIdAsync(int id)
        {
            return await this.GetProfileByTypeAndIdAsync<HospitalDirector>("hospital-directors", id);
        }

        /// <summary>
        /// Gets pharmacy admin profile by user id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>response</returns>
        public async Task<Response<PharmacyAdmin>> GetPharmacyAdminByIdAsync(int id)
        {
            return await this.GetProfileByTypeAndIdAsync<PharmacyAdmin>("pharmacy-admins", id);
        }

        /// <summary>
        /// Gets ministry worker
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<IEnumerable<MinistryWorker>>> GetMinistryWorkersAsync()
        {
            return await this.GetProfilesByTypeAsync<MinistryWorker>("ministry-workers");
        }

        /// <summary>
        /// Gets ministry worker profile by user id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>response</returns>
        public async Task<Response<MinistryWorker>> GetMinistryWorkerByIdAsync(int id)
        {
            return await this.GetProfileByTypeAndIdAsync<MinistryWorker>("ministry-workers", id);
        }

        /// <summary>
        /// Gets pharmacists
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<IEnumerable<PharmacistFullInfo>>> GetPharmacistsAsync()
        {
            return await this.GetProfilesByTypeAsync<PharmacistFullInfo>("pharmacists");
        }

        /// <summary>
        /// Gets pharmacist profile by user id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>response</returns>
        public async Task<Response<PharmacistFullInfo>> GetPharmacistByIdAsync(int id)
        {
            return await this.GetProfileByTypeAndIdAsync<PharmacistFullInfo>("pharmacists", id);
        }

        
        /// <summary>
        /// Posts pharmacy admin profile
        /// </summary>
        /// <param name="pharmacyAdmin">Pharmacy Admin</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PostPharmacyAdmin(PharmacyAdmin pharmacyAdmin)
        {
            return await this.PostProfileAsync("pharmacy-admins", pharmacyAdmin);
        }

        /// <summary>
        /// Posts doctor
        /// </summary>
        /// <param name="doctor">doctor</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PostDoctorAsync(Doctor doctor)
        {
            return await this.PostProfileAsync("doctors", doctor);
        }

        /// <summary>
        /// Posts hospital director
        /// </summary>
        /// <param name="hospitalDirector">hospital director</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PostHospitalDirectorAsync(HospitalDirector hospitalDirector)
        {
            return await this.PostProfileAsync("hospital-directors", hospitalDirector);
        }

        /// <summary>
        /// Posts ministry worker
        /// </summary>
        /// <param name="ministryWorker">ministry worker</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PostMinistryWorkerAsync(MinistryWorker ministryWorker)
        {
            return await this.PostProfileAsync("ministry-workers", ministryWorker);
        }

        /// <summary>
        /// Posts patient
        /// </summary>
        /// <param name="patient">patient</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PostPatientAsync(Patient patient)
        {
            return await this.PostProfileAsync("patients", patient);
        }

        /// <summary>
        /// Posts pharmacist
        /// </summary>
        /// <param name="pharmacist">pharmacist</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PostPharmacistAsync(PharmacistFullInfo pharmacistFullInfo)
        {
            return await this.PostProfileAsync("pharmacists", pharmacistFullInfo);
        }

        /// <summary>
        /// Puts doctor
        /// </summary>
        /// <param name="doctorUpdateInfo">doctor update info</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PutDoctorAsync(DoctorUpdateInfo doctorUpdateInfo)
        {
            return await this.PutProfileAsync("doctors", doctorUpdateInfo);
        }

        /// <summary>
        /// Puts hospital director
        /// </summary>
        /// <param name="hospitalDirector">hospital director</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PutHospitalDirectorAsync(HospitalDirector hospitalDirector)
        {
            return await this.PutProfileAsync("hospital-directors", hospitalDirector);
        }

        /// <summary>
        /// Puts ministry worker
        /// </summary>
        /// <param name="ministryWorker">ministry worker</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PutMinistryWorkerAsync(MinistryWorker ministryWorker)
        {
            return await this.PutProfileAsync("ministry-workers", ministryWorker);
        }

        /// <summary>
        /// Puts patient
        /// </summary>
        /// <param name="patient">patient</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PutPatientAsync(Patient patient)
        {
            return await this.PutProfileAsync("patients", patient);
        }

        /// <summary>
        /// Puts pharmacist
        /// </summary>
        /// <param name="pharmacist">pharmacist</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PutPharmacistAsync(PharmacistFullInfo pharmacistFullInfo)
        {
            return await this.PutProfileAsync("pharmacists", pharmacistFullInfo);
        }

        /// <summary>
        /// Deletes doctor profile
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<string>> DeleteDoctorAsync()
        {
            return await this.DeleteProfileAsync("doctors");
        }

        /// <summary>
        /// Deletes hospital director profile
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<string>> DeleteHospitalDirectorAsync()
        {
            return await this.DeleteProfileAsync("hospital-directors");
        }

        /// <summary>
        /// Deletes miinstry worker profile
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<string>> DeleteMinistryWorkerAsync()
        {
            return await this.DeleteProfileAsync("ministry-workers");
        }

        /// <summary>
        /// Deletes patient profile
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<string>> DeletePatientAsync()
        {
            return await this.DeleteProfileAsync("patients");
        }

        /// <summary>
        /// Deletes pharmacist profile
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<string>> DeletePharmacistAsync()
        {
            return await this.DeleteProfileAsync("pharmacists");
        }

        /// <summary>
        /// Deletes pharmacy admin
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<string>> DeletePharmacyAdmin()
        {
            return await this.DeleteProfileAsync("pharmacy-admins");
        }

        /// <summary>
        /// Deletes current profile
        /// </summary>
        /// <param name="currentProfileType">Current profile type</param>
        /// <returns>response</returns>
        public async Task<Response<string>> DeleteCurrentProfile(string currentProfileType)
        {
            var builder = new StringBuilder();

            builder.Append(currentProfileType)
                   .Append("s")
                   .Replace('_', '-');

            return await this.DeleteProfileAsync(builder.ToString());
        }

        /// <summary>
        /// Approves doctor profile of the user.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>response</returns>
        public async Task<Response<string>> ApproveDoctorAsync(int userId)
        {
            return await this.ApproveProfileAsync(userId, "doctor", "doctors");
        }

        /// <summary>
        /// Approves pharmacist profile of the user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>response</returns>
        public async Task<Response<string>> ApprovePharmacistAsync(int userId)
        {
            return await this.ApproveProfileAsync(userId, "pharmacist", "pharmacists");
        }

        /// <summary>
        /// Approves hospital admin profile of the user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>response</returns>
        public async Task<Response<string>> ApproveHospitalAdminAsync(int userId)
        {
            return await this.ApproveProfileAsync(userId, "hospital_director", "hospital-directors");
        }

        /// <summary>
        /// Approves pharmacy admin profile of the user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>response</returns>
        public async Task<Response<string>> ApprovePharmacyAdminAsync(int userId)
        {
            return await this.ApproveProfileAsync(userId, "pharmacy_admin", "pharmacy-admins");
        }

        /// <summary>
        /// Gets unapproved doctors
        /// </summary>
        /// <param name="hospital">Hospital name.</param>
        /// <returns>unapproved doctors</returns>
        public async Task<Response<IEnumerable<UnapprovedDoctor>>> GetUnapprovedDoctors(string hospital)
        {
            var response = await this._userApiHttpClient.GetAsync($"api/doctors/{hospital}");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<IEnumerable<UnapprovedDoctor>>(Status.Error, null);

            var content = await response.Content.ReadAsStringAsync();

            var unapprovedDoctors = JsonConvert.DeserializeObject<IEnumerable<UnapprovedDoctor>>(content);

            return this.ConstructResponse(Status.Ok, unapprovedDoctors);
        }

        /// <summary>
        /// Gets unapproved pharmacists
        /// </summary>
        /// <param name="pharmacy">Pharmacy name.</param>
        /// <returns>unapproved pharmacists</returns>
        public async Task<Response<IEnumerable<UnapprovedPharmacist>>> GetUnapprovedPharmacists(string pharmacy)
        {
            var response = await this._userApiHttpClient.GetAsync($"api/pharmacists/{pharmacy}");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<IEnumerable<UnapprovedPharmacist>>(Status.Error, null);

            var content = await response.Content.ReadAsStringAsync();

            var unapprovedDoctors = JsonConvert.DeserializeObject<IEnumerable<UnapprovedPharmacist>>(content);

            return this.ConstructResponse(Status.Ok, unapprovedDoctors);
        }

        /// <summary>
        /// Gets unapproved hospital admins.
        /// </summary>
        /// <returns>unapproved hospital admins</returns>
        public async Task<Response<IEnumerable<UnapprovedHospitalAdmin>>> GetUnapprovedHospitalAdmins()
        {
            var response = await this._userApiHttpClient.GetAsync($"api/hospital-directors?isApproved=false");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<IEnumerable<UnapprovedHospitalAdmin>>(Status.Error, null);

            var content = await response.Content.ReadAsStringAsync();

            var unapprovedDoctors = JsonConvert.DeserializeObject<IEnumerable<UnapprovedHospitalAdmin>>(content);

            return this.ConstructResponse(Status.Ok, unapprovedDoctors);
        }

        /// <summary>
        /// Gets unapproved hospital admins.
        /// </summary>
        /// <returns>unapproved hospital admins</returns>
        public async Task<Response<IEnumerable<UnapprovedPharmacyAdmin>>> GetUnapprovedPharmacyAdmins()
        {
            var response = await this._userApiHttpClient.GetAsync($"api/pharmacy-admins?isApproved=false");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<IEnumerable<UnapprovedPharmacyAdmin>>(Status.Error, null);

            var content = await response.Content.ReadAsStringAsync();

            var unapprovedDoctors = JsonConvert.DeserializeObject<IEnumerable<UnapprovedPharmacyAdmin>>(content);

            return this.ConstructResponse(Status.Ok, unapprovedDoctors);
        }

        /// <summary>
        /// Event handler for TokenProvider TokenUpdated class
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        public void UpdateToken(object sender,TokenEventArgs e)
        {
            this._accessToken = e.AccessToken;
            this._userApiHttpClient.SetBearerToken(this._accessToken);
        }

        /// <summary>
        /// Disposes User Management API client
        /// </summary>
        public void Dispose()
        {
            this._registerClient.Dispose();
            this._userApiHttpClient.Dispose();
        }

        /// <summary>
        /// Puts profile
        /// </summary>
        /// <typeparam name="T">Type of profile object</typeparam>
        /// <param name="type">Type of profile</param>
        /// <param name="profile">profile object</param>
        /// <returns>response</returns>
        private async Task<Response<string>> PutProfileAsync<T>(string type,T profile)
        {
            var response = await this._userApiHttpClient.PutAsync($"api/{type}", this.ConstructContent(profile));

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse(Status.Error, response.ReasonPhrase);

            return this.ConstructResponse(Status.Ok, "Profile updated");
        }

        /// <summary>
        /// Posts profile
        /// </summary>
        /// <typeparam name="T">Type of profile object</typeparam>
        /// <param name="type">Type of profile</param>
        /// <param name="profile">profile object</param>
        /// <returns>response</returns>
        private async Task<Response<string>> PostProfileAsync<T>(string type,T profile)
        {
            var response = await this._userApiHttpClient.PostAsync($"api/{type}", this.ConstructContent(profile));

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse(Status.Error, response.ReasonPhrase);

            return this.ConstructResponse(Status.Ok, $"Profile of type {type} added");
        }

        /// <summary>
        /// Gets profiles by type
        /// </summary>
        /// <typeparam name="T">Type of result</typeparam>
        /// <param name="type">Type of profile.</param>
        /// <returns>response</returns>
        private async Task<Response<IEnumerable<T>>> GetProfilesByTypeAsync<T>(string type) where T:class
        {
            var response = await this._userApiHttpClient.GetAsync($"api/{type}");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<IEnumerable<T>>(Status.Error, null);

            var content = JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync());

            return this.ConstructResponse(Status.Ok, content);
        }

        /// <summary>
        /// Gets profile by type and id
        /// </summary>
        /// <typeparam name="T">Type of result</typeparam>
        /// <param name="type">Type of profile</param>
        /// <param name="id">id</param>
        /// <returns>result</returns>
        private async Task<Response<T>> GetProfileByTypeAndIdAsync<T>(string type,int id) where T:class
        {
            var response = await this._userApiHttpClient.GetAsync($"api/{type}/{id}");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse<T>(Status.Error, null);

            var content = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

            return this.ConstructResponse(Status.Ok, content);
        }

        /// <summary>
        /// Deletes profile
        /// </summary>
        /// <param name="type">Type of profile</param>
        /// <returns>response</returns>
        private async Task<Response<string>> DeleteProfileAsync(string type)
        {
            var response = await this._userApiHttpClient.DeleteAsync($"api/{type}");

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse(Status.Error, response.ReasonPhrase);

            return this.ConstructResponse(Status.Ok, "Profile is deleted");
        }

        /// <summary>
        /// Approves profile by tyep
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="type">type</param>
        /// <param name="uri">Uri</param>
        /// <returns>response</returns>
        private async Task<Response<string>> ApproveProfileAsync(int userId, string type, string uri)
        {
            // constructing approval information
            var approval = new Approval
            {
                UserId = userId,
                Type = type
            };

            // serializing approval information
            var serialized = this.ConstructContent(approval);

            // getting response
            var response = await this._userApiHttpClient.PutAsync(
                $"api/approvals/{uri}", serialized);

            if (!response.IsSuccessStatusCode)
                return this.ConstructResponse(Status.Error, response.ReasonPhrase);

            // returning result
            return this.ConstructResponse(Status.Ok, "Profile is approved");
        }

        /// <summary>
        /// Constructs reponse
        /// </summary>
        /// <typeparam name="T">Type of result</typeparam>
        /// <param name="message">Message</param>
        /// <param name="result">Result</param>
        /// <returns>Response</returns>
        private Response<T> ConstructResponse<T>(Status status, T result)
        {
            // returning response
            return new Response<T>
            {
                Status = status,
                Result = result
            };
        }

        /// <summary>
        /// Constructs content
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entity">entity</param>
        /// <returns>string content</returns>
        private StringContent ConstructContent<T>(T entity)
        {
            var serialized = JsonConvert.SerializeObject(entity);

            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }
    }
}
