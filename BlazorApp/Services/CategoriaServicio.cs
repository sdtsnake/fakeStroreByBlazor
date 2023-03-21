using System.Text.Json;
namespace BlazorApp;


public class CategoriaServicio : ICategoriaServicio{

    private readonly HttpClient client;

    private readonly JsonSerializerOptions options;

    public CategoriaServicio(HttpClient client){
        this.client = client;
        options = new JsonSerializerOptions{PropertyNameCaseInsensitive= true};
    
    }

    public async Task <List<Categoria>?> Get(){
        var response = await client.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<Categoria>>(content,options);
    }
}

public interface ICategoriaServicio{
    Task <List<Categoria>?> Get();
}
