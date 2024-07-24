using ErrorOr;

namespace BusinessLogic.ErrorHandling;

public static partial class Errors
{
    public static class Image
    {
        //NoImagesFound
        public static Error NoImagesFound => Error.NotFound(
                       "Image.NoImagesFound",
                                  "No images found"
                   );
        public static Error ImageNotFound => Error.NotFound(
            "Image.ImageNotFound",
            "Image not found"
        );

        public static Error FailedToUploadImage => Error.Failure(
            "Image.FailedToUploadImage",
            "Failed to upload image"
        );

        //invalid image
        public static Error InvalidImage => Error.Validation(
            "Image.InvalidImage",
            "Invalid image"
        );
    }
}