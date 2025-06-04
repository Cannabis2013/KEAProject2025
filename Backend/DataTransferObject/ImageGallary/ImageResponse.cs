using ALBackend.Entities.ImageGallary;

namespace ALBackend.DataTransferObject.ImageGallary;

public class ImageResponse
{
    public static ImageResponse Instance(Image image) => new(image);

    private ImageResponse(Image image)
    {
        Base64 = image.Base64;
        Width = image.Width;
        Height = image.Height;
    }
    public string Base64 { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
}