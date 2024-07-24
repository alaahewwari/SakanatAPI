namespace BusinessLogic.Helpers
{
    public class CloudinaryUrlParser
    {
        public string ExtractPublicIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            // Normalize URL (optional, based on your need)
            url = url.Trim().ToLower();

            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                // Remove any query or fragment part for cleaner processing
                string path = uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
                string[] segments = path.Split('/');

                if (segments.Length > 0 && segments[^1].Contains('.'))
                {
                    string lastSegment = segments[^1]; // Using index from end syntax
                    int lastDotIndex = lastSegment.LastIndexOf('.');

                    if (lastDotIndex > 0) // Ensure there is actually a part before the dot
                    {
                        string publicId = lastSegment.Substring(0, lastDotIndex);
                        return publicId;
                    }
                    else
                    {
                        throw new InvalidOperationException("The file name in the URL must contain a name before the extension.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("URL must have a path with a file name containing a period.");
                }
            }
            else
            {
                throw new UriFormatException("Invalid URL format.");
            }
        }
    }
}
