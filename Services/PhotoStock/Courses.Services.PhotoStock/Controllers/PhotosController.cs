using Courses.Services.PhotoStock.Dtos;
using Courses.Shared.ControllerBases;
using Courses.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            //cancellationToken alma amacımız foto süresi 20 sn
            //eğer endpointi çağıran yer işlemi sonlandırırsa burada da sonlansın.
            //güzelliği fotoyu seçti ve foto kaydı yapıyor diyelim.
            //Fakat tarayıcıyı kapattı photo eklemeyi de iptal ediyor sistem kapatmazsa eğer devam eder.
            //async başlayan bir işlemi hata fırlatarak sonlandırabiliriz. CancellationToken da hata fırlatarak sonlandırıyor.

            if (photo != null && photo.Length > 0)
            {
                //öncelikle path i alalım.
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);

                //example: http://www.photostock.api.com/photos/asd.jpg
                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo bulunamadı", 400));
        }

        [HttpGet]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);

            //böyle bir dosya olup olmadığına bakar
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("İlgili resim bulunamadı", 404));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}