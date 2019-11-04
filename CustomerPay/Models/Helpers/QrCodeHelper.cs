using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace CustomerPay.Models.Helpers
{
    /// <summary>
    /// 二维码生成帮助类
    /// </summary>
    public class QrCodeHelper
    {
        public static string GenQrCodeHtmlImg (string content, int size)
        {
            return string.Format("<img src=\"data:image/png;base64,{0}\" alt=\"扫我\" />", GenQrCodeBase64(content, size));
        }
        public static string GenQrCodeBase64 (string content, int size)
        {
            MemoryStream qrCode = GenQrCode(content, size);
            byte[] arr1 = new byte[qrCode.Length];
            qrCode.Position = 0;
            qrCode.Read(arr1, 0, (int)qrCode.Length);
            qrCode.Close();
            return Convert.ToBase64String(arr1);
        }
        public static MemoryStream GenQrCode (string content, int size)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.L);
            QrCode qrCode;
            qrEncoder.TryEncode(content, out qrCode);

            GraphicsRenderer render = new GraphicsRenderer(new FixedCodeSize(size, QuietZoneModules.Zero), Brushes.Black, Brushes.Transparent);
            MemoryStream ms = new MemoryStream();

            Bitmap bitmap = new Bitmap(size, size);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(System.Drawing.Color.White);
            render.Draw(graphics, qrCode.Matrix);
            bitmap.Save(ms, ImageFormat.Jpeg);
            return ms;
        }

        public static MemoryStream GenQrCode (string content, int size, Image logo)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.L);
            QrCode qrCode;
            qrEncoder.TryEncode(content, out qrCode);

            GraphicsRenderer render = new GraphicsRenderer(new FixedCodeSize(size, QuietZoneModules.Two), Brushes.Black, Brushes.Transparent);
            MemoryStream ms = new MemoryStream();

            Bitmap bitmap = new Bitmap(size, size);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(System.Drawing.Color.White);
            render.Draw(graphics, qrCode.Matrix);

            //计算画框的宽度高度以及位置
            float rectWidth = logo.Width;
            float rectHeight = logo.Height;
            float rectX = (size - logo.Width)/2;
            float rectY = (size - logo.Height)/2;
            //加上水印
            graphics.DrawImage(logo, rectX, rectY, rectWidth, rectHeight);

            bitmap.Save(ms, ImageFormat.Jpeg);
            return ms;
        }
    }
}