using Courses.Services.Basket.Dtos;
using Courses.Shared.Dtos;

namespace Courses.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);

        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);

        Task<Response<bool>> DeleteBasket(string userId);
    }
}