using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorBootstrap;
using CV.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Components;

namespace CV.Services
{
    public class ResumeClient : IResumeClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ResumeClient> _logger;
        public ResumeClient(HttpClient httpClient, ILogger<ResumeClient> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(System.Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:5178");
        }
        public async Task<(Resume? Resume, string? ErrorMessage)> GetResumeAsync()
        {

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/resume");
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadFromJsonAsync<Resume>();
                    return (res, null);
                }
                else
                {
                    return (null, "Unknown error");
                }

            }
            catch (Exception ex)
            {

                _logger.LogCritical(ex.Message);
                return (null, ex.Message);
            }
        }

        public async Task<string?> UpsertResumeAsync(Resume resume)
        {
            try
            {
                _logger.LogCritical(JsonSerializer.Serialize(resume));
                var request = new HttpRequestMessage(HttpMethod.Post, "api/resume");
                _logger.LogCritical(request.ToString());
                request.Content = new StringContent(JsonSerializer.Serialize(resume), Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(request);

                _logger.LogCritical(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogCritical("success");
                    return null;
                }
                else
                {
                    _logger.LogCritical("failure");
                    return "Unknown error";
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return ex.Message;

            }
        }

        public async Task<string?> DeleteResumeAsync(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"api/resume/{id}");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return "Unknown error";
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return ex.Message;
            }
        }

        public async Task<string?> SaveResumeAsync(Resume resume)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/resume");
                request.Content = new StringContent(JsonSerializer.Serialize(resume), Encoding.UTF8, "application/json");
                var response = _httpClient.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return "Unknown error";
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return ex.Message;
            }
        }

        public async Task<string?> SaveResumeAsHTMLAsync(string html)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/resume/html");
                request.Content = new StringContent(JsonSerializer.Serialize(html), Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return "Unknown error";
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return ex.Message;
            }
        }
    }
}