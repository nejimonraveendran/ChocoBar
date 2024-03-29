using System;
using System.Drawing;
using System.IO;

namespace ChocoBar
{
    internal static class ImageOps
    {
        public static byte[] ImageFileToBytes(string imageFilePath)
        {
            return File.ReadAllBytes(imageFilePath);
        }

        public static string ImageFileToBase64(string imageFilePath)
        {
            byte[] imageBytes = ImageFileToBytes(imageFilePath);
            return Convert.ToBase64String(imageBytes);
        }

        public static byte[] Base64ToImageBytes(string base64)
        {
            return Convert.FromBase64String(base64);
        }

        public static string ImageBytesToBase64(byte[] imageBytes)
        {
            return Convert.ToBase64String(imageBytes);
        }

        public static Image Base64ToImage(string base64)
        {
            byte[] imageBytes = Base64ToImageBytes(base64);
            return Image.FromStream(new MemoryStream(imageBytes));

        }

        public static Image ImageFileToImage(string imageFilePath)
        {
            var bytes = ImageFileToBytes(imageFilePath);
            return Image.FromStream(new MemoryStream(bytes));
        }

        public static string ImageToBase64(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return Convert.ToBase64String(ms.ToArray());
            }
        }


    }
}
