public class TaskService : ITaskService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://your-java-backend-url/api/tasks";

    public TaskService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Task>> GetTasksAsync()
    {
        var response = await _httpClient.GetAsync(BaseUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Task>>();
    }

    public async Task<Task> AddTaskAsync(Task task)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, task);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Task>();
    }

    public async Task<Task> UpdateTaskAsync(Task task)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{task.Id}", task);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Task>();
    }

    public async Task<bool> DeleteTaskAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        return response.IsSuccessStatusCode;
    }
}
