﻿using System.Text.Json.Serialization;

namespace Courses.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; private set; }
        //kendi içimizde kullanmak için yapıyoruz
        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        //başarılı-başarısız durumunu kontrol edicez
        public bool IsSuccessful { get; private set; }
        public List<string> Errors { get; set; }
        //Static Factory Method
        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }
        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };
        }
        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }
        public static Response<T> Fail(string errors, int statusCode)
        {
            return new Response<T> { Errors = new List<string> { errors }, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
