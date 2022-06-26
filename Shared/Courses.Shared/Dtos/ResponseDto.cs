using System.Text.Json.Serialization;

namespace Courses.Shared.Dtos
{
    public class ResponseDto<T>
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
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };
        }
        public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDto<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }
        public static ResponseDto<T> Fail(string errors, int statusCode)
        {
            return new ResponseDto<T> { Errors = new List<string> { errors }, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
