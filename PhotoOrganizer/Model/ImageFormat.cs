using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganizer.Model
{
    public static class ImageFormat
    {
        public const string Png = "PNG Portable Network Graphics (*.png)|" + "*.png";
        public const string Jpg = "JPEG File Interchange Format (*.jpg *.jpeg *jfif)|" + "*.jpg;*.jpeg;*.jfif";
        public const string Bmp = "BMP Windows Bitmap (*.bmp)|" + "*.bmp";
        public const string Tif = "TIF Tagged Imaged File Format (*.tif *.tiff)|" + "*.tif;*.tiff";
        public const string Gif = "GIF Graphics Interchange Format (*.gif)|" + "*.gif";
        public const string CR2 = "Canon photo format (*.cr2)|" + "*.cr2";
        public const string AllImages = "Image file|" + "*.png; *.jpg; *.jpeg; *.jfif; *.bmp;*.tif; *.tiff; *.gif";
        public const string AllFiles = "All files (*.*)" + "|*.*";
        public static readonly string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".jfif", ".bmp", ".tif", ".tiff", ".gif", ".cr2" };
        public static readonly List<string> dialogFilterImageTypes;

        static ImageFormat()
        {
            dialogFilterImageTypes = new List<string>();
            dialogFilterImageTypes.Add(Png);
            dialogFilterImageTypes.Add(Jpg);
            dialogFilterImageTypes.Add(Bmp);
            dialogFilterImageTypes.Add(Tif);
            dialogFilterImageTypes.Add(Gif);
            dialogFilterImageTypes.Add(CR2);
            dialogFilterImageTypes.Add(AllImages);
        }
    }
}
