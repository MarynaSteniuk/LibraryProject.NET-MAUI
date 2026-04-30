using System.Net.Http.Json;
using LibraryProject.MAUI.Models;
using System.Net.Http.Headers;

namespace LibraryProject.MAUI.Services;

public class LibraryApiService : ILibraryApiService
{
    private readonly HttpClient _httpClient;

    public LibraryApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<BookModel>> GetAllAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<BookModel>>("api/Books");
            return result ?? new List<BookModel>();
        }
        catch (HttpRequestException)
        {
            return new List<BookModel>();
        }
        catch (Exception)
        {
            return new List<BookModel>();
        }
    }

    public async Task<BookModel?> GetByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<BookModel>($"api/Books/{id}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<BookModel?> CreateAsync(BookModel item)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Books", item);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BookModel>();
            }
            return null;
        }
        catch (HttpRequestException)
        {
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task UpdateAsync(BookModel item)
    {
        try
        {
            await _httpClient.PutAsJsonAsync($"api/Books/{item.Id}", item);
        }
        catch (HttpRequestException)
        {

        }
        catch (Exception)
        {
           
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _httpClient.DeleteAsync($"api/Books/{id}");
        }
        catch (HttpRequestException)
        {
            
        }
        catch (Exception)
        {
            
        }
    }
    public async Task<IEnumerable<AuthorModel>> GetAuthorsAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<AuthorModel>>("api/Authors");
            return result ?? new List<AuthorModel>();
        }
        catch (Exception)
        {
            return new List<AuthorModel>();
        }
    }
    public async Task<AuthorModel?> GetAuthorByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<AuthorModel>($"api/Authors/{id}");
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<AuthorModel?> CreateAuthorAsync(AuthorModel item)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authors", item);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<AuthorModel>();
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task UpdateAuthorAsync(AuthorModel item)
    {
        try
        {
            await _httpClient.PutAsJsonAsync($"api/Authors/{item.Id}", item);
        }
        catch (Exception) { }
    }

    public async Task DeleteAuthorAsync(int id)
    {
        try
        {
            await _httpClient.DeleteAsync($"api/Authors/{id}");
        }
        catch (Exception) { }
    }

    public async Task InitializeAuthAsync()
    {
        // Дістаємо токен із захищеного сховища телефону
        var token = await SecureStorage.Default.GetAsync("auth_token");

        // Якщо токен є, вішаємо його на наш HttpClient як "перепустку"
        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", new { Email = email, Password = password });

            if (response.IsSuccessStatusCode)
            {
                var authData = await response.Content.ReadFromJsonAsync<AuthResponse>();
                var token = authData?.GetToken();

                if (!string.IsNullOrWhiteSpace(token))
                {
                    await SecureStorage.Default.SetAsync("auth_token", token);

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return true;
                }
            }
            return false; 
        }
        catch (Exception)
        {
            return false;
        }
    }
    public async Task<bool> RegisterAsync(string email, string password)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", new { Email = email, Password = password });
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
