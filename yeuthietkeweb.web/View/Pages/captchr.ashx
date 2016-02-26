<%@ WebHandler Language="C#" Class="captchr" %>

using System;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

public class captchr : IHttpHandler {
    
    //public void ProcessRequest (HttpContext context) {
    //    context.Response.ContentType = "text/plain";
    //    context.Response.Write("Hello World");
    //}
    private Random ran;
    public void ProcessRequest(HttpContext context)
    {

        context.Response.Cache.SetNoStore();
        context.Response.ContentType = "image/gif";
        ran = new Random();
        string i = GenerateRandomCode(6);
        coc(i);        
        sc(i + string.Empty);
    }

    private const string chars = "1A2B3C4D5E6F7G8H9K1N2U3V4X5Y6Z7";//"a1bA2cB3dC4eD5fE6gF7hG8H9kK1NnUuVvXxYyZz"
    public string GenerateRandomCode(int Keys)
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        int charLength = chars.Length;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < Keys; i++)
        {
            int idx = random.Next(0, charLength);
            sb.Append(chars.Substring(idx, 1));
        }
        return sb.ToString();
    } 

    private void coc(string c)
    {
        HttpCookie coc1 = new HttpCookie("slmsrcd1");
        DateTime dateSLMF = DateTime.Now;
        coc1.Value = c;
        coc1.Expires = dateSLMF.AddDays(3);
        HttpContext.Current.Response.Cookies.Add(coc1);
    }

    private void sc(string text)
    {
        string[] fonts = new string[] { "Tahoma", "Arial" };
        HatchStyle[] styles = new HatchStyle[] { HatchStyle.ForwardDiagonal, HatchStyle.BackwardDiagonal };

        Color c1 = Color.FromArgb(ran.Next(100, 255), ran.Next(200, 255), ran.Next(220, 255), ran.Next(200, 255));
        Color c2 = Color.FromArgb(ran.Next(100, 155), ran.Next(0, 155), ran.Next(0, 155), ran.Next(0, 255));
        Color c3 = Color.FromArgb(ran.Next(100, 255), ran.Next(0, 155), ran.Next(0, 155), ran.Next(0, 255));
        Color c4 = Color.FromArgb(ran.Next(250, 255), ran.Next(250, 255), ran.Next(250, 255));

        //Brush br = new HatchBrush(styles[ran.Next(0, 2)], c1, c2);
        HatchBrush br = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
        Bitmap re = new Bitmap(85, 22);
        Graphics gr = Graphics.FromImage(re);
        gr.CompositingQuality = CompositingQuality.HighQuality;
        gr.SmoothingMode = SmoothingMode.HighQuality;

        int fz = 15;// ran.Next(14, 18);
        //Brush tb = new HatchBrush(styles[ran.Next(0, 2)], c2, c4);
        Brush tb = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.SkyBlue);
        Font fo = new Font(fonts[ran.Next(0, 2)], fz);

        gr.FillRectangle(br, 0, 0, 90, 22);
        gr.DrawString(text, fo, tb, 16 - fz, 0);

        fo.Dispose();
        gr.Flush();
        br.Dispose();
        tb.Dispose();
        gr.Dispose();

        MemoryStream str = new MemoryStream();
        re.Save(str, ImageFormat.Gif);
        str.WriteTo(HttpContext.Current.Response.OutputStream);
        str.Flush();
        str.Close();
        re.Dispose();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}