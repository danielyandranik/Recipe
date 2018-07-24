using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using UserManagementConsumer.Models;

namespace UserManagementConsumer.Client
{
    /// <summary>
    /// Class for consuming User Management API
    /// </summary>
    public class UserManagementApiClient
    {
        /// <summary>
        /// Username
        /// </summary>
        private string _username;

        /// <summary>
        /// Password
        /// </summary>
        private string _password;

        /// <summary>
        /// Auth API base address
        /// </summary>
        private string _authApiBaseAddress;

        /// <summary>
        /// User Management Base address
        /// </summary>
        private string _userApiBaseAddress;

        /// <summary>
        /// Token client for getting access token from Auth API
        /// </summary>
        private TokenClient _tokenClient;

        /// <summary>
        /// Http client for consuming User Management API
        /// </summary>
        private HttpClient _userApiHttpClient;

        /// <summary>
        /// Http client for register requests
        /// </summary>
        private HttpClient _registerClient;

        /// <summary>
        /// Access token
        /// </summary>
        private string _accessToken;

        /// <summary>
        /// Creates new instance of <see cref="UserManagementApiClient"/>
        /// </summary>
        /// <param name="authApiAddress">Auth API address</param>
        /// <param name="userApiAddress">User Management API address</param>
        public UserManagementApiClient(string authApiAddress, string userApiAddress)
        {
            // setting fields
            this._authApiBaseAddress = authApiAddress;
            this._userApiBaseAddress = userApiAddress;

            // constructing http clients
            this.ConstructRegisterClient();
            this.ConstructTokenClient();
        }

        /// <summary>
        /// Creates new instance of <see cref="UserManagementApiClient"/>
        /// </summary>
        public UserManagementApiClient():this("http://localhost:5700","http://localhost:5800") { }

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
                return this.ConstructResponse(response.ReasonPhrase, response);

            // reading response
            var responseString = await response.Content.ReadAsStringAsync();

            // returning response
            return this.ConstructResponse(response.ReasonPhrase, response);
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
                return this.ConstructResponse(response.ReasonPhrase, response);

            // reading content
            var content = await response.Content.ReadAsStringAsync();

            // returning response
            return this.ConstructResponse(response.ReasonPhrase, response);
        }

        /// <summary>
        /// Sign in for user
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns>response</returns>
        public async void SignInAsync(string username,string password)
        {
            this._username = username;
            this._password = password;
            this._accessToken = await this.GetAccessTokenAsync();
            this.ConstructUserApiHttpClient();
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
                return this.ConstructResponse<IEnumerable<UserPublicInfo>>(response.ReasonPhrase, null);

            // reading response
            var content = await response.Content.ReadAsStringAsync();

            // content is empty inform about that
            if (string.IsNullOrEmpty(content))
                return this.ConstructResponse<IEnumerable<UserPublicInfo>>("No content", null);

            // returning result
            return this.ConstructResponse(
                "Request is successfull", JsonConvert.DeserializeObject<IEnumerable<UserPublicInfo>>(content));
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
                return this.ConstructResponse<UserPublicInfo>(response.ReasonPhrase, null);

            // reading content
            var content = await response.Content.ReadAsStringAsync();

            // if no content inform about that
            if (string.IsNullOrEmpty(content))
                return this.ConstructResponse<UserPublicInfo>("No content", null);

            // returning result
            return this.ConstructResponse(
                "Request is successful", JsonConvert.DeserializeObject<UserPublicInfo>(content));
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
                return this.ConstructResponse("Error occured", response.ReasonPhrase);

            return this.ConstructResponse("Password is updated", await response.Content.ReadAsStringAsync());
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
                return this.ConstructResponse("Error", response.ReasonPhrase);

            this._accessToken = await this.GetAccessTokenAsync();

            return this.ConstructResponse("Current profile is updated", await response.Content.ReadAsStringAsync());
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
                return this.ConstructResponse("Error occured", response.ReasonPhrase);

            return this.ConstructResponse("User deleted", await response.Content.ReadAsStringAsync());
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
        public async Task<Response<DoctorPublicInfo>> GetDoctorByIdAsync(int id)
        {
            return await this.GetProfileByTypeAndIdAsync<DoctorPublicInfo>("doctors", id);
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
            return await this.GetProfilesByTypeAsync<HospitalDirector>("hospitaldirectors");
        }

        /// <summary>
        /// Gets hospital director profile by user id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>response</returns>
        public async Task<Response<HospitalDirector>> GetHospitalDirectorByIdAsync(int id)
        {
            return await this.GetProfileByTypeAndIdAsync<HospitalDirector>("patients", id);
        }

        /// <summary>
        /// Gets ministry worker
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<IEnumerable<MinistryWorker>>> GetMinistryWorkersAsync()
        {
            return await this.GetProfilesByTypeAsync<MinistryWorker>("ministryworkers");
        }

        /// <summary>
        /// Gets ministry worker profile by user id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>response</returns>
        public async Task<Response<MinistryWorker>> GetMinistryWorkerByIdAsync(int id)
        {
            return await this.GetProfileByTypeAndIdAsync<MinistryWorker>("ministryworkers", id);
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
            return await this.GetProfileByTypeAndIdAsync<PharmacistFullInfo>("patients", id);
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
            return await this.PostProfileAsync("hospitalDirectors", hospitalDirector);
        }

        /// <summary>
        /// Posts ministry worker
        /// </summary>
        /// <param name="ministryWorker">ministry worker</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PostMinistryWorkerAsync(MinistryWorker ministryWorker)
        {
            return await this.PostProfileAsync("ministryworkers", ministryWorker);
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
            return await this.PutProfileAsync("hospitaldirectors", hospitalDirector);
        }

        /// <summary>
        /// Puts ministry worker
        /// </summary>
        /// <param name="ministryWorker">ministry worker</param>
        /// <returns>response</returns>
        public async Task<Response<string>> PutMinistryWorkerAsync(MinistryWorker ministryWorker)
        {
            return await this.PutProfileAsync("ministryworkers", ministryWorker);
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
            return await this.DeleteProfileAsync("hospitaldirectors");
        }

        /// <summary>
        /// Deletes miinstry worker profile
        /// </summary>
        /// <returns>response</returns>
        public async Task<Response<string>> DeleteMinistryWorkerAsync()
        {
            return await this.DeleteProfileAsync("ministryworkers");
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
                return this.ConstructResponse("Error", response.ReasonPhrase);

            return this.ConstructResponse("Success", "Profile updated");
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
                return this.ConstructResponse("Error", response.ReasonPhrase);

            return this.ConstructResponse("Success", $"Profile of type {type} added");
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
                return this.ConstructResponse<IEnumerable<T>>("Error", null);

            var content = JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync());

            return this.ConstructResponse("Request is successful", content);
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
                return this.ConstructResponse<T>("Error", null);

            var content = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

            return this.ConstructResponse("Request is successfull", content);
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
                return this.ConstructResponse("Error", response.ReasonPhrase);

            return this.ConstructResponse("Success", "Profile is deleted");
        }

        /// <summary>
        /// Constructs Token client
        /// </summary>
        private void ConstructTokenClient()
        {
            // discovering
            var disco = DiscoveryClient.GetAsync(this._authApiBaseAddress).Result;

            if (disco.IsError)
                throw new Exception("Auth API error");

            // request token
            this._tokenClient = new TokenClient(disco.TokenEndpoint, "DefaultClient", "secret");
        }

        /// <summary>
        /// Constructs register client
        /// </summary>
        private void ConstructRegisterClient()
        {
            this._registerClient = new HttpClient();
            this._registerClient.BaseAddress = new Uri(this._userApiBaseAddress);
            this._registerClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Constructs the client for consuming User Management API
        /// </summary>
        private void ConstructUserApiHttpClient()
        {
            this._userApiHttpClient = new HttpClient();
            this._userApiHttpClient.BaseAddress = new Uri(this._userApiBaseAddress);
            this._userApiHttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            this._userApiHttpClient.SetBearerToken(this._accessToken);

        }

        /// <summary>
        /// Constructs reponse
        /// </summary>
        /// <typeparam name="T">Type of result</typeparam>
        /// <param name="message">Message</param>
        /// <param name="result">Result</param>
        /// <returns>Response</returns>
        private Response<T> ConstructResponse<T>(string message, T result)
        {
            // returning response
            return new Response<T>
            {
                Message = message,
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

        /// <summary>
        /// Gets access token
        /// </summary>
        /// <returns>access token</returns>
        private async Task<string> GetAccessTokenAsync()
        {
            // getting response from Auth API
            var response = await this._tokenClient.
                RequestResourceOwnerPasswordAsync(this._username, this._password, "UserManagementAPI");

            // if error occured inform about that
            if (response.IsError)
                throw new Exception("Invalid login or password");

            return response.AccessToken;
        }
    }
}
