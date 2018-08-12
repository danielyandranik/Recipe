﻿using System.IO;
using System.Media;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Common;
using Desktop.ViewModels;
using Desktop.Views.Windows;


namespace Desktop.Services
{
    /// <summary>
    /// Service for decoding service
    /// </summary>
    public class QrDecoderService
    {
        /// <summary>
        /// Video capture device
        /// </summary>
        private readonly VideoCaptureDevice _finalFrame;

        /// <summary>
        /// Sell medicines page viewmodel
        /// </summary>
        private readonly SellMedicinesViewModel _vm;

        /// <summary>
        /// Dispatcher
        /// </summary>
        private readonly Dispatcher _dispatcher;

        /// <summary>
        /// Creates new instance of <see cref="QrDecoderService"/>
        /// </summary>
        /// <param name="vm">Sell Medicines page viewmodel</param>
        /// <param name="dispatcher">Dispatcher</param>
        public QrDecoderService(SellMedicinesViewModel vm, Dispatcher dispatcher)
        {
            // setting fields
            this._vm = vm;
            this._dispatcher = dispatcher;

            // initializing components
            var filterInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            var device = filterInfo[0];

            this._finalFrame = new VideoCaptureDevice(device.MonikerString);
            this._finalFrame.NewFrame += this.FrameHandler;
        }

        /// <summary>
        /// Starts qr decoding
        /// </summary>
        public void Start()
        {
            if(!this._finalFrame.IsRunning)
                this._finalFrame.Start();
        }

        /// <summary>
        /// Stops qr decoding
        /// </summary>
        public void Stop()
        {
            if(this._finalFrame.IsRunning)
                this._finalFrame.Stop();
        }

        /// <summary>
        /// Handler for new frame
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="eventArgs">Event argument</param>
        private async void FrameHandler(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                var bitmapImage = new BitmapImage();

                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    // decoding QR 
                    var id = await this.GetRecipeId(bitmap);

                    // if id is not null end QR decoding and open sell page
                    if (id != null)
                    {
                        this.Stop();
                        this._vm.RecipeId = id;
                        this._vm.QrDecoderVisibility = Visibility.Hidden;
                        this._vm.ItemsVisibility = Visibility.Visible;
                        this._dispatcher.Invoke(() => this._vm.FindRecipeCommand.Execute(this._vm.RecipeId));
                        SystemSounds.Beep.Play();
                        return;
                    }

                    bitmapImage = this.GetImage(bitmap);
                }

                bitmapImage.Freeze();

                await this._dispatcher.BeginInvoke(new ThreadStart(delegate { this._vm.QrDecoderSource = bitmapImage; }));
            }
            catch
            {
                var dictionary = App.Current.Resources;

                RecipeMessageBox.Show((string)dictionary["qr_decode_error"]);
            }
        }

        /// <summary>
        /// Gets bitmap image from bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap</param>
        /// <returns>bitmap image</returns>
        private BitmapImage GetImage(Bitmap bitmap)
        {
            var bi = new BitmapImage();
            bi.BeginInit();

            var ms = new MemoryStream();

            bitmap.Save(ms, ImageFormat.Bmp);

            ms.Seek(0, SeekOrigin.Begin);

            bi.StreamSource = ms;

            bi.EndInit();
            
            return bi;
        }

        /// <summary>
        /// Gets recipe id
        /// </summary>
        /// <param name="bitmap">QR code bitmap</param>
        /// <returns>recipe id</returns>
        private Task<string> GetRecipeId(Bitmap bitmap)
        {
            return Task.Run(() =>
            {
                var source = new BitmapLuminanceSource(bitmap);

                var hybridBinarizer = new HybridBinarizer(source);

                var binaryBitmap = new BinaryBitmap(hybridBinarizer);

                var reader = new MultiFormatReader();

                var result = reader.decode(binaryBitmap);

                var id = result?.Text;

                return id;
            });
        }
    }
}