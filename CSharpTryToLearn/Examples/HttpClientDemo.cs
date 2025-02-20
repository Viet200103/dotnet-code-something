namespace CSharpTryToLearn.Examples;

public class HttpClientDemo
{

    public async Task Run()
    {
        HttpClient client = new HttpClient();
        
        string uri = "http://www.contoso.com";
        try
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }
}