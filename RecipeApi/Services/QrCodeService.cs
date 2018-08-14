using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;

namespace RecipeApi.Services
{
    /// <summary>
    /// QR code service
    /// </summary>
    public class QrCodeService
    {
        /// <summary>
        /// QR code generator
        /// </summary>
        private readonly QRCodeGenerator _qrGenerator;

        /// <summary>
        /// Path of the folder where QR codes are stored
        /// </summary>
        private string _path;

        /// <summary>
        /// Creates new instance of <see cref="QrCodeService"/>
        /// </summary>
        public QrCodeService()
        {
            this._qrGenerator = new QRCodeGenerator();

            this.CreateQrFolder();
        }

        /// <summary>
        /// Creates QR code
        /// </summary>
        /// <param name="recipeId">Recipe Id</param>
        /// <returns>nothing</returns>
        public async Task CreateQrCodeAsync(string recipeId)
        {
            await this.GetCreateTask(recipeId); 
        }

        /// <summary>
        /// Gets QR image by the given recipe Id
        /// </summary>
        /// <param name="recipeId">Recipe Id</param>
        /// <returns>QR code bitmap image</returns>
        public async Task<Bitmap> GetQrImage(string recipeId)
        {
            if (!Directory.Exists($@"{this._path}\{recipeId}.jpg"))
                await this.CreateQrCodeAsync(recipeId);

            return await this.GetImageTask(recipeId);
        }

        /// <summary>
        /// Deletes QR code image by the given recipe Id
        /// </summary>
        /// <param name="recipeId">Recipe Id</param>
        public void DeleteQrCode(string recipeId)
        {
            File.Delete($@"{this._path}\{recipeId}.jpg");
        }

        /// <summary>
        /// Creates QR code generating task and gets it.
        /// </summary>
        /// <param name="recipeId">Recipe Id</param>
        /// <returns>QR code generation task</returns>
        private Task GetCreateTask(string recipeId)
        {
            var task = new Task(() =>
            {
                var qrCodeData = this._qrGenerator.CreateQrCode(recipeId, QRCodeGenerator.ECCLevel.Q);

                var qrCode = new QRCode(qrCodeData);

                var qrImage = qrCode.GetGraphic(20);

                var fileStream = File.Create($@"{this._path}\{recipeId}.jpg");

                qrImage.Save(fileStream, ImageFormat.Jpeg);

                fileStream.Dispose();
                qrImage.Dispose();
                qrCode.Dispose();
                qrCodeData.Dispose();
            });

            task.Start();

            return task;
        }

        /// <summary>
        /// Creates QR code image getting task and gets it.
        /// </summary>
        /// <param name="recipeId">Recipe Id</param>
        /// <returns>QR code image getting task</returns>
        private Task<Bitmap> GetImageTask(string recipeId)
        {
            var task = new Task<Bitmap>(() => (Bitmap)Bitmap.FromFile($@"{this._path}\{recipeId}.jpg"));
 
            task.Start();

            return task;
        }

        /// <summary>
        /// Creates QR codes folder in AppData local
        /// </summary>
        private void CreateQrFolder()
        {
            var builder = new StringBuilder();

            builder.Append(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))
                   .Append("\\RecipeAPI\\QrCodes");

            var path = builder.ToString();

            Directory.CreateDirectory(path);

            this._path = path;
        }
    }
}
