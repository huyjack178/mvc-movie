using MvcMovie.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace MvcMovie.Controllers
{
    public class ImageHandler
    {
        private static string _currentImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        public static void CreateImageFrom(Movie movie)
        {
            Bitmap img = DownloadImageFrom(movie.ImageLink);
            string imgPath = Path.Combine(_currentImagePath, "movie" + movie.Id + ".jpg");
            SaveImage(img, imgPath);
        }

        private static Bitmap DownloadImageFrom(string url)
        {
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(url))
                {
                    return new Bitmap(stream);
                }
            }
        }

        private static void SaveImage(Bitmap img, string path)
        {
            img.Save(path, ImageFormat.Jpeg);
        }
    }
}