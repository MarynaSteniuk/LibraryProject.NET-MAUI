using System.Net.Http.Json;
using LibraryProject.MAUI.Models;

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
}
