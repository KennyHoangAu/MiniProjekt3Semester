using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;
using TodoListBlazor;
using System.Net.Http.Json;

using Data;
namespace Service;

public class ClientService
{
	private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public ClientService(HttpClient http, IConfiguration configuration) {
        this.http = http;
        this.configuration = configuration;
        // Denne konfiguration læses fra filen "appsettings.json". Se mere i opgave 5.
        this.baseAPI = configuration["base_api"];
    }

    public async Task<QuestionData[]> GetAllQuestions()
    {
        var url = $"{baseAPI}questions";
        //string url = "https://krdo-todo.azurewebsites.net/api/tasks/";
        return await http.GetFromJsonAsync<QuestionData[]>(url);
    }
}