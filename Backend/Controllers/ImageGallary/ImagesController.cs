using ALBackend.DataTransferObject.ImageGallary;
using ALBackend.Services.ImageGallary;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.ImageGallary;

[ApiController,Route("/images")]
public class ImagesController(Images images)
{
    [HttpGet]
    public async Task<JsonResult> Get()
    {
        var response = (await images.ManyAsync())
            .Select(ImageResponse.Instance)
            .ToList();
        return new(response);
    }
    
    [HttpGet("{count:int}")]
    public async Task<JsonResult> GetN(int count)
    {
        var response = (await images.ManyAsync(count))
            .Select(ImageResponse.Instance)
            .ToList();
        return new(response);
    }
    
    [HttpGet("randoms/{count:int}")]
    public async Task<JsonResult> GetRandom(int count)
    {
        var response = (await images.ManyRandomAsync(count))
            .Select(ImageResponse.Instance)
            .ToList();
        return new(response);
    }
}