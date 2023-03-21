using BlazorApp.Pages.Productos;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp;

public class ProductoServicio : IProductoServicio{

    private readonly HttpClient client;
    private readonly JsonSerializerOptions options;
    public ProductoServicio (HttpClient httpClient){
        client = httpClient;
        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<Producto>?> Get(){
        var response = await client.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Producto>>(content, options);
    }

    public async Task<Producto> FindById(int productoId){
        var response = await client.GetAsync($"v1/products/{productoId}");
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<Producto>(content, options); 
    }

    public async Task Add(Producto producto){
        var response = await client.PostAsync("v1/products", JsonContent.Create(producto));
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }
    }

    public async Task Update(Producto producto)
    {
        var response = await client.PutAsync($"v1/products/{producto.Id}",JsonContent.Create(producto));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);

        }
    }

    public async Task Delete(int productoId){
        var response = await client.DeleteAsync($"v1/products/{productoId}");
        var content = await response.Content.ReadAsStringAsync();

         if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }
    }
}

public interface IProductoServicio{
    Task<List<Producto>?> Get();
    Task Add(Producto producto);
    Task Delete(int productoId);
    Task Update(Producto producto);
    Task<Producto> FindById(int productoId);

}