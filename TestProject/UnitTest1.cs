using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;


public class Tests
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:8080");
    }

    [Test, Order(1)]
    public async Task ProductController_8080_PostProduct()
    {
        string uniqueId = Guid.NewGuid().ToString();

        string uniquename = $"abcd_{uniqueId}";

        string requestBody = $"{{\"name\": \"{uniquename}\", \"category\": \"Electronics\", \"price\": 10.23, \"stock\":10}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/products", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test, Order(2)]
    public async Task ProductController_8080_GetProducts()
    {
        HttpResponseMessage prodresponse = await _httpClient.GetAsync("/api/products");
        Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    }

    [Test, Order(3)]
public async Task ProductController_8080_PostProduct_and_GetProductById()
{
    string uniqueId = Guid.NewGuid().ToString();
    string uniquename = $"abcd_{uniqueId}";
    string requestBody = $"{{\"name\": \"{uniquename}\", \"category\": \"Electronics\", \"price\": 10.23, \"stock\":10}}";

    // Send POST request to create a new product
    HttpResponseMessage response = await _httpClient.PostAsync("/api/products", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    Console.WriteLine(response.StatusCode);
    string responseString = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseString);
    
    // Check if product creation was successful
    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

    // Parse the ID from the JSON response
    JObject jsonResponse = JObject.Parse(responseString);
    int createdProductId = jsonResponse["id"].Value<int>(); // Assuming the ID field in the response is named "id"
    Console.WriteLine(createdProductId);

    // Send GET request to retrieve the created product by ID
    HttpResponseMessage prodresponse = await _httpClient.GetAsync($"/api/products/{createdProductId}");
    Console.WriteLine(await prodresponse.Content.ReadAsStringAsync());

    // Check if GET request was successful
    Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
}


    [Test, Order(4)]
    public async Task ProductController_8080_Post_and_GetProductsByCategory()
    {
        string uniqueId = Guid.NewGuid().ToString();

        string uniquename = $"abcd_{uniqueId}";

        string requestBody = $"{{\"name\": \"{uniquename}\", \"category\": \"Woods\", \"price\": 10.23, \"stock\":10}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/products", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        string category = "Woods";

        HttpResponseMessage prodresponse = await _httpClient.GetAsync($"/api/Products/filter?category={category}");
        Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    }

    [Test]
    public void Product_Properties_Id_ReturnExpectedDataTypes_int()
    {
        string assemblyName = "dotnetapp";
        string typeName = "dotnetapp.Models.Product";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Id");
        Assert.IsNotNull(propertyInfo, "The property 'Id' was not found on the Product class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(int), propertyType, "The data type of 'Id' property is not as expected (int).");
    }

    [Test]
    public void Product_Properties_Price_ReturnExpectedDataTypes_decimal()
    {
        string assemblyName = "dotnetapp";
        string typeName = "dotnetapp.Models.Product";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Price");
        Assert.IsNotNull(propertyInfo, "The property 'Price' was not found on the Product class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(decimal), propertyType, "The data type of 'Price' property is not as expected (decimal).");
    }

    [Test]
    public void Product_Properties_Name_ReturnExpectedDataTypes_String()
    {
        string assemblyName = "dotnetapp";
        string typeName = "dotnetapp.Models.Product";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Name");
        Assert.IsNotNull(propertyInfo, "The property 'Name' was not found on the Product class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(string), propertyType, "The data type of 'Name' property is not as expected (string).");
    }

    [Test]
    public void Product_Properties_Stock_ReturnExpectedDataTypes_int()
    {
        string assemblyName = "dotnetapp";
        string typeName = "dotnetapp.Models.Product";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Stock");
        Assert.IsNotNull(propertyInfo, "The property 'Stock' was not found on the Product class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(int), propertyType, "The data type of 'Stock' property is not as expected (int).");
    }
}
