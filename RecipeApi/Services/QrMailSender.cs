using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RecipeApi.Services
{
    public class QrMailSender
    {
        private readonly SmtpClient _smptpClient;

        private readonly NetworkCredential _networkCredential;

        public QrMailSender(NetworkCredential networkCredential)
        {
            this._networkCredential = networkCredential;

            this._smptpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = networkCredential,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
        }
        
        public async Task SendAsync(string to,string recipeId,Bitmap qrImage)
        {
            await this.GetSendTask(to,recipeId,qrImage);
        }

        private Task GetSendTask(string to,string recipeId,Bitmap qrImage)
        {
            var task = new Task(() =>
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(this._networkCredential.UserName),
                    Subject = "Recipe QR code",
                    IsBodyHtml = true
                };

                var stream = new MemoryStream();

                qrImage.Save(stream, ImageFormat.Jpeg);

                stream.Position = 0;

                var img = new LinkedResource(stream, "image/jpeg")
                {
                    ContentId = "recipe_qr_code"
                };

                var foot = AlternateView.CreateAlternateViewFromString("<p> <img src=cid:recipe_qr_code /> </p>", null, "text/html");

                foot.LinkedResources.Add(img);

                mail.AlternateViews.Add(foot);

                mail.To.Add(to);

                this._smptpClient.Send(mail);

                mail.Dispose();
                foot.Dispose();
                img.Dispose();
                stream.Dispose();
                qrImage.Dispose();
            });

            task.Start();

            return task;
        }
    }
}
