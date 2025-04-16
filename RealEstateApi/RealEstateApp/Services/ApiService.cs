using Newtonsoft.Json;
using RealEstateApp.Models;
using RealEstateApp.Settings;
using System.Net.Http.Json;

namespace RealEstateApp.Services
{
    public static class ApiService
    {
        #region Users
        public static async Task<bool> RegisterUser(string name, string email, string phone, string password)
        {
            var register = new RegisterAndLogin()
            {
                Email = email,
                Phone = phone,
                Password = password,
                Name = name
            };

            using (var httpClient = new HttpClient(GetHandler()))
            {
                var httpResponse = await httpClient.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    Content = JsonContent.Create(register),
                    RequestUri = new Uri(string.Concat(AppSettings.ApiUrl, "api/Users/Register"))
                });

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;

            }
        }

        private static HttpClientHandler GetHandler()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            return handler;
        }

        public static async Task<bool> Login(string email, string password)
        {
            var login = new RegisterAndLogin()
            {
                Email = email,
                Password = password
            };

            var urlAddress = string.Concat(AppSettings.ApiUrl, "api/Users/Login");

            var httpResponse = await CommonService.SendRequest(new Dto.SendHttpRequestModel
            {
                FinalUrlAddress = urlAddress,
                authenticationHeaderValue = null,
                Body = JsonContent.Create(login),
                HttpMethod = HttpMethod.Post,
            });



            if (!httpResponse.IsSuccessStatusCode)
            {
                return false;
            }

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var resultToken = JsonConvert.DeserializeObject<Token>(jsonResponse);

            Preferences.Set(nameof(Token.AccessToken), resultToken.AccessToken);
            Preferences.Set(nameof(Token.UserId), resultToken.UserId);
            Preferences.Set(nameof(Token.UserName), resultToken.UserName);
            return true;
        }

            #endregion


            #region Category

            public async static Task<List<Category>> GetCategories()
            {
                using (var httpClient = new HttpClient(GetHandler()))
                {

                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(AppSettings.ApiUrl, "api/Categories"))
                    };

                    httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get(nameof(Token.AccessToken), string.Empty));

                    var httpResponse = await httpClient.SendAsync(httpRequest);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("خطا در دریافت اطلاعات");
                    }

                    var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Category>>(jsonResponse);
                    return result;

                }
            }

            #endregion


            #region TrandingProperty
            public async static Task<List<TrandingProperty>> GetTrandingProperties()
            {
                using (var httpClient = new HttpClient())
                {

                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(AppSettings.ApiUrl, "api/Properties/TrendingProperties"))
                    };

                    httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get(nameof(Token.AccessToken), string.Empty));

                    var httpResponse = await httpClient.SendAsync(httpRequest);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("خطا در دریافت اطلاعات");
                    }

                    var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<TrandingProperty>>(jsonResponse);
                    return result;

                }
            }
            #endregion


            #region Search
            public async static Task<List<SearchProperty>> SearchProperties(string addressSearch)
            {
                using (var httpClient = new HttpClient())
                {

                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(AppSettings.ApiUrl, $"api/Properties/SearchProperties?address={addressSearch}"))
                    };

                    httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get(nameof(Token.AccessToken), string.Empty));

                    var httpResponse = await httpClient.SendAsync(httpRequest);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("خطا در دریافت اطلاعات");
                    }

                    var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<SearchProperty>>(jsonResponse);
                    return result;

                }
            }
            #endregion

            #region Property
            public async static Task<List<PropertyByCategory>> GetPropertyListByCategory(int categoryId)
            {
                if (categoryId <= 0)
                {
                    throw new Exception("شناسه وارد شده نامعتبر است");
                }
                using (var httpClient = new HttpClient())
                {

                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(AppSettings.ApiUrl, $"api/Properties/PropertyList?categoryId={categoryId}"))
                    };

                    httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get(nameof(Token.AccessToken), string.Empty));

                    var httpResponse = await httpClient.SendAsync(httpRequest);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("خطا در دریافت اطلاعات");
                    }

                    var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<PropertyByCategory>>(jsonResponse);
                    return result;

                }
            }
            public async static Task<PropertyDetail> GetPropertyDetail(int propertyId)
            {
                if (propertyId <= 0)
                {
                    throw new Exception("شناسه وارد شده نامعتبر است");
                }
                using (var httpClient = new HttpClient())
                {

                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(AppSettings.ApiUrl, $"api/Properties/PropertyDetail?id=={propertyId}"))
                    };

                    httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get(nameof(Token.AccessToken), string.Empty));

                    var httpResponse = await httpClient.SendAsync(httpRequest);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("خطا در دریافت اطلاعات");
                    }

                    var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PropertyDetail>(jsonResponse);
                    return result;

                }
            }
            #endregion


            #region Bookmark
            public async static Task<List<BookmarkList>> GetBookmarkList()
            {
                using (var httpClient = new HttpClient())
                {

                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(AppSettings.ApiUrl, $"api/Bookmarks"))
                    };

                    httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get(nameof(Token.AccessToken), string.Empty));

                    var httpResponse = await httpClient.SendAsync(httpRequest);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("خطا در دریافت اطلاعات");
                    }

                    var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<BookmarkList>>(jsonResponse);
                    return result;

                }
            }

            public async static Task<bool> AddBookmark(AddBookmark addBookmark)
            {
                using (var httpClient = new HttpClient())
                {


                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(string.Concat(AppSettings.ApiUrl, $"api/Bookmarks")),
                        Content = JsonContent.Create(addBookmark)
                    };

                    httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get(nameof(Token.AccessToken), string.Empty));

                    var httpResponse = await httpClient.SendAsync(httpRequest);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("خطا در دریافت اطلاعات");
                    }

                    //var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    //var result = JsonConvert.DeserializeObject<List<BookmarkList>>(jsonResponse);
                    return true;

                }
            }

            public async static Task<bool> DeleteBookmark(int bookmarkId)
            {
                using (var httpClient = new HttpClient())
                {
                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(string.Concat(AppSettings.ApiUrl, $"api/Bookmarks/{bookmarkId}"))
                    };

                    httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get(nameof(Token.AccessToken), string.Empty));

                    var httpResponse = await httpClient.SendAsync(httpRequest);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("خطا در دریافت اطلاعات");
                    }

                    //var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    //var result = JsonConvert.DeserializeObject<List<BookmarkList>>(jsonResponse);
                    return true;

                }
            }
            #endregion
        }
    }
