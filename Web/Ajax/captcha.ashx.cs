using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Web;
using System.Web.SessionState;
using BankNet.Core;

namespace Web.Ajax
{
    /// <summary>
    /// Summary description for captcha
    /// </summary>
    public class captcha : IHttpHandler, IRequiresSessionState
    {
        private Random rand = new Random();

        public void ProcessRequest(HttpContext context)
        {

            if (!Security.AllowCall(context)) return;

            context.Response.ContentType = "image/jpeg";
            CreateImage();
        }

        private void CreateImage()
        {
            string code = GetRandomText();
            Bitmap bitmap = new Bitmap(140, 50, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Yellow);
            Rectangle rect = new Rectangle(0, 0, 140, 50);
            SolidBrush b = new SolidBrush(Color.DarkKhaki);
            SolidBrush blue = new SolidBrush(Color.Blue);
            int counter = 0;
            g.DrawRectangle(pen, rect);
            g.FillRectangle(b, rect);
            for (int i = 0; i < code.Length; i++)
            {
                g.DrawString(code[i].ToString(), new Font("Verdena", 10 + rand.Next(14, 18)), blue, new PointF(10 + counter, 5));
                counter += 20;
            }
            DrawRandomLines(g);
            bitmap.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Gif);
            g.Dispose();
            bitmap.Dispose();
        }

        private void DrawRandomLines(Graphics g)
        {
            SolidBrush green = new SolidBrush(Color.Green);
            for (int i = 0; i < 20; i++)
            {
                g.DrawLines(new Pen(green, 2), GetRandomPoints());
            }
        }

        private Point[] GetRandomPoints()
        {
            Point[] points = { new Point(rand.Next(10, 150), rand.Next(10, 150)), new Point(rand.Next(10, 100), rand.Next(10, 100)) };
            return points;
        }

        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "0123456789abcdefghijklmnopqrstuvwxyzQWERTYUIOPASDFGHJKLZXCVBNM";
            Random r = new Random();
            for (int j = 0; j < 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            HttpContext.Current.Session[Config.GetSessionCode] = randomText.ToString().ToUpper();
            return randomText.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}