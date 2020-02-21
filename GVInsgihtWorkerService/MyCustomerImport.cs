using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using core;

namespace GVInsgihtWorkerService
{
    class MyCustomerImport
    {

        
             public List<ApiCustomerProperty> getCustomerProperties(string ourCustomerSite, string apiUserName, string apiPassword)
        {
            var client = new HttpClient();
            try
            {
                 AuthToken token = getUserToken(ourCustomerSite, apiUserName, apiPassword);

                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token.Access_Token);
              //  ourCustomerSite = "http://localhost:59717";
                var importReadString = client.GetAsync(ourCustomerSite + "/api/property/GetProperties").Result.Content.ReadAsStringAsync().Result;
                var customerModulesList = JsonConvert.DeserializeObject<List<ApiCustomerProperty>>(importReadString);
                return customerModulesList;

            }
            catch (Exception ex)
            {
                return new List<ApiCustomerProperty>();
            }
            finally
            {
                client.Dispose();
            }
        }
        public List<ApiCustomerModule> GetCustomerModule(string ourCustomerSite, string apiUserName, string apiPassword) {
            var client = new HttpClient();
            try
            {
                //AuthToken token = getUserToken(ourCustomerSite, apiUserName, apiPassword);
                               
               // client.DefaultRequestHeaders.Add("Authorization", "bearer " + token.Access_Token);
                ourCustomerSite = "http://localhost:59717";
                var importReadString = client.GetAsync(ourCustomerSite + "/api/User/GetUserModules").Result.Content.ReadAsStringAsync().Result;
                var customerModulesList = JsonConvert.DeserializeObject<List<ApiCustomerModule>>(importReadString);
                return customerModulesList;

            }
            catch (Exception ex)
            {
                return new List<ApiCustomerModule>();
            }
            finally
            {
                client.Dispose();
            }
        }

        public List<ApiActiveUser>  GetUserHistoriesForToday(string ourCustomerSite, string apiUserName, string apiPassword) {
           // return new List<ApiActiveUser>();
            var client = new HttpClient();
            try
            {
                AuthToken token = getUserToken(ourCustomerSite, apiUserName, apiPassword);



                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token.Access_Token);

                var importReadString = client.GetAsync(ourCustomerSite + "/api/User/GetUserHistories").Result.Content.ReadAsStringAsync().Result;
                var activeUserList = JsonConvert.DeserializeObject<List<ApiActiveUser>>(importReadString);

                var finishedInspectiontoday = getActiveUser(activeUserList);
                return finishedInspectiontoday;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                client.Dispose();
            }
        }

        public List<ApiCustomerImport> CallToCustomerImportApiUrl(string ourCustomerSite,string apiUserName,string apiPassword) {
            var client = new HttpClient();
            try
            {
                AuthToken token = getUserToken(ourCustomerSite, apiUserName, apiPassword);

                
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token.Access_Token);

                var importReadString = client.GetAsync(ourCustomerSite + "/api/Import/GetImports").Result.Content.ReadAsStringAsync().Result;
                var importList = JsonConvert.DeserializeObject<List<ApiCustomerImport>>(importReadString);
                return importList;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally {
                client.Dispose();
            }
           


        }

        public List<ApiCustomerInspections> CallToCustomerInspectionUrl(string ourCustomerSite, string apiUserName, string apiPassword)
        {
            var client = new HttpClient();
            try
            {
                AuthToken token = getUserToken(ourCustomerSite, apiUserName, apiPassword);



                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token.Access_Token);

                var importReadString = client.GetAsync(ourCustomerSite + "/api/Inspection/GetLatest").Result.Content.ReadAsStringAsync().Result;
                var importList = JsonConvert.DeserializeObject<List<ApiCustomerInspections>>(importReadString);
               
                var finishedInspectiontoday = getFinishedinspection(importList);
                return finishedInspectiontoday;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally {
                client.Dispose();
            }



        }


        private List<ApiActiveUser> getActiveUser(List<ApiActiveUser> iList) {
            try
            {
                return iList.FindAll(DateconverterForActiveUser);
            }

            catch (ArgumentNullException ex)
            {
                return null;

            }
        }

        private List<ApiCustomerInspections> getFinishedinspection(List<ApiCustomerInspections> iList)
        {
            try
            {
                return iList.FindAll(Dateconverter);
            }

            catch (ArgumentNullException ex) {
                return null;
            
            }
           
        }

        private Boolean DateconverterForActiveUser(ApiActiveUser cActiveUser) {
           var aDate = DateTime.Now;
           var todaydate = aDate.Date;

           return DateTime.Compare(cActiveUser.Date.Date, todaydate) == 0;
           }

        private Boolean Dateconverter(ApiCustomerInspections cInspection)  {
            var aDate = DateTime.Now;
            var todaydate = aDate.Date;
            
         
            if (cInspection.InspectedDate.HasValue)
           
                return DateTime.Compare(cInspection.InspectedDate.Value.Date, todaydate) == 0;
            else
                return false;

            // return DateTime.Compare(cInspection.InspectedDate.Value.Date, Convert.ToDateTime("2018-10-19").Date) == 0;

        }
        public DateTime CalcNextImportDate(DateTime fromDate, string whenType) {

            if (whenType == "EveryDay")
                return fromDate.AddDays(1);
            else if (whenType == "EveryHour")
                return fromDate.AddHours(1);

            return DateTime.MinValue;

            
        }
        private AuthToken getUserToken(string ourCustomerSite, string apiUserName, string apiPassword) {
            var request = WebRequest.Create(ourCustomerSite + "/api/GetAccessToken") as HttpWebRequest;

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var authCredentials = "grant_type=password&username=" + apiUserName + "&password=" + apiPassword;

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(authCredentials);
            request.ContentLength = bytes.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            var response = request.GetResponse();

            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            var tokenResult = reader.ReadToEnd();



            return JsonConvert.DeserializeObject<AuthToken>(tokenResult);
          
        }
    }
}

//FROM API
//ApiModule
//{
//    Title,
//    Name, 
//    Enabled
//}

//Module{
//   Id
//   Name
//}
//CustomerModule
//{
//    Id
//    ModuleId
//    CustomerId
//    Title
//}

