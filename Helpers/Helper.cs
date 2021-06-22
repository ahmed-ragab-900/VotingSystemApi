using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace VotingSystemApi.Helpers
{
    public class Helper
    {
        public static string imagePath = AppDomain.CurrentDomain.BaseDirectory + "/images/";
        public static string serverPath;

        public string SaveBase64(string base64)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                Bitmap resizedImg = new Bitmap(150, 150);
                
                double ratioX = (double)resizedImg.Width / (double)image.Width;
                double ratioY = (double)resizedImg.Height / (double)image.Height;
                double ratio = ratioX < ratioY ? ratioX : ratioY;

                int newHeight = Convert.ToInt32(image.Height * ratio);
                int newWidth = Convert.ToInt32(image.Width * ratio);

                
                Bitmap ne = new Bitmap(newWidth, newHeight);
                using (Graphics g = Graphics.FromImage(ne))
                {
                    g.DrawImage(image, 0, 0, newWidth, newHeight);
                }
                

                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                string imageName = Guid.NewGuid().ToString() + ".jpg";
                ne.Save(imagePath + imageName);
                return imageName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool IsImageExist(string imageName)
        {
            try
            {
               return File.Exists(imagePath + imageName);
            }
            catch 
            {
                return false;
            }
        }

        public void deleteImage(string imageName)
        {
            try
            {
                File.Delete(imagePath + imageName);
            }
            catch { }
        }
    }
}
