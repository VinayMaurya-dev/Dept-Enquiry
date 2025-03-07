using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourtCase.Controllers
{
    public class CaptchaController : Controller
    {
        // GET: Captcha
        public ActionResult GetCaptcha()
        {
            var captchaText = GenerateCaptchaText();
            Session["CaptchaCode"] = captchaText;
            var captchaImage = GenerateCaptchaImage(captchaText);
            return File(captchaImage, "image/png");
        }

        private string GenerateCaptchaText()
        {
            return new Random().Next(1000, 9999).ToString();
        }


        private byte[] GenerateCaptchaImage(string captchaText)
        {
            using (var bitmap = new Bitmap(150, 40))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Set the background color to a lighter shade
                graphics.Clear(Color.FromArgb(128, 200, 200, 250)); // Light blue background

                // Use a different font for a more modern look
                var font = new Font("Comic Sans MS", 18, FontStyle.Bold);
                var random = new Random();

                for (int i = 0; i < captchaText.Length; i++)
                {
                    int alpha = 255 - (i * 30);
                    var brush = new SolidBrush(Color.FromArgb(alpha, Color.DarkBlue)); // Dark blue text
                    float angle = (float)(random.NextDouble() * 20 - 10); // Wider rotation range
                    float x = (bitmap.Width / captchaText.Length) * i + 10;
                    float y = bitmap.Height / 2;

                    graphics.TranslateTransform(x, y);
                    graphics.RotateTransform(angle);
                    graphics.DrawString(captchaText[i].ToString().ToUpper(), font, brush, new PointF(0, -font.Size / 2));
                    graphics.RotateTransform(-angle);
                    graphics.TranslateTransform(-x, -y);
                }

                DrawEnhancedNoise(graphics, bitmap.Width, bitmap.Height);

                using (var ms = new System.IO.MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        private void DrawEnhancedNoise(Graphics graphics, int width, int height)
        {
            var random = new Random();
            using (var noiseBrush = new SolidBrush(Color.FromArgb(100, 255, 0, 0))) // Semi-transparent red
            {
                for (int i = 0; i < 150; i++) // Increased number of noise elements
                {
                    int x = random.Next(0, width);
                    int y = random.Next(0, height);
                    graphics.FillEllipse(noiseBrush, x, y, 2 + random.Next(0, 5), 2 + random.Next(0, 5)); // Variable size for noise
                }
            }
        }
         
    }
}