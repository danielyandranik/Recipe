using System.IO;
using System.Drawing.Imaging;
using QRCoder;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;

namespace RecipeApi.Services
{
    public class QrCodeService
    {
        private readonly QRCodeGenerator _qrGenerator;       

        public QrCodeService()
        {
            this._qrGenerator = new QRCodeGenerator();
        }

        public async Task CreateQrCodeAsync(string recipeId)
        {
            await this.GetCreateTask(recipeId); 
        }

        public async Task<Bitmap> GetQrImage(string recipeId)
        {
            if (!Directory.Exists($@".\QrCodes\{recipeId}.jpg"))
                await this.CreateQrCodeAsync(recipeId);

            return await this.GetImageTask(recipeId);
        }

        public void DeleteQrCode(string recipeId)
        {
            File.Delete($".\\QrCodes\\{recipeId}.jpg");
        }

        private Task GetCreateTask(string recipeId)
        {
            var task = new Task(() =>
            {
                var qrCodeData = this._qrGenerator.CreateQrCode(recipeId, QRCodeGenerator.ECCLevel.Q);

                var qrCode = new QRCode(qrCodeData);

                var qrImage = qrCode.GetGraphic(20);

                var fileStream = File.Create($".\\QrCodes\\{recipeId}.jpg");

                qrImage.Save(fileStream, ImageFormat.Jpeg);

                fileStream.Dispose();
                qrImage.Dispose();
                qrCode.Dispose();
                qrCodeData.Dispose();
            });

            task.Start();

            return task;
        }

        private Task<Bitmap> GetImageTask(string recipeId)
        {
            var task = new Task<Bitmap>(() => (Bitmap)Bitmap.FromFile($@".\QrCodes\{recipeId}.jpg"));

            task.Start();

            return task;
        }
    }
}
