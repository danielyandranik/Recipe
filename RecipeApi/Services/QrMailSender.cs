using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace RecipeApi.Services
{
    /// <summary>
    /// QR Sender
    /// </summary>
    public class QrMailSender
    {
        /// <summary>
        /// SMTP client
        /// </summary>
        private readonly SmtpClient _smptpClient;

        /// <summary>
        /// Network credentials
        /// </summary>
        private readonly NetworkCredential _networkCredential;

        /// <summary>
        /// Creates new instance of <see cref="NetworkCredential"/>
        /// </summary>
        /// <param name="networkCredential"></param>
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
        
        /// <summary>
        /// Sends QR code image to given mail address asynchronously.
        /// </summary>
        /// <param name="to">Mail address which message will be sent to.</param>
        /// <param name="recipeId">Recipe Id</param>
        /// <param name="qrImage">QR code Image</param>
        /// <returns>nothing</returns>
        public async Task SendAsync(string to,string recipeId,Bitmap qrImage)
        {
            await this.GetSendTask(to,recipeId,qrImage);
        }

        /// <summary>
        /// Creates QR code image sending task.
        /// </summary>
        /// <param name="to">Mail address which message will be sent to.</param>
        /// <param name="recipeId">Recipe Id</param>
        /// <param name="qrImage">QR code Image</param>
        /// <returns>QR code image sending task.</returns>
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