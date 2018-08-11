using AForge.Video;
using AForge.Video.DirectShow;
using Desktop.ViewModels;
using RecipeClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Threading;
using ZXing;
using ZXing.Common;
using Desktop.Views.Windows;
using System.Windows;
using System.Media;

namespace Desktop.Services
{
    public class QrDecoderService
    {
        private readonly VideoCaptureDevice _finalFrame;

        private readonly SellMedicinesViewModel _vm;

        private readonly Dispatcher _dispatcher;

        public QrDecoderService(SellMedicinesViewModel vm, Dispatcher dispatcher)
        {
            this._vm = vm;
            this._dispatcher = dispatcher;

            var filterInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            var device = filterInfo[0];

            this._finalFrame = new VideoCaptureDevice(device.MonikerString);
            this._finalFrame.NewFrame += this.FrameHandler;
        }

        public void Start()
        {
            this._finalFrame.Start();
        }

        public void Stop()
        {
            this._finalFrame.Stop();
        }

        private async void FrameHandler(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                var bitmapImage = new BitmapImage();

                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    var id = await this.GetRecipeId(bitmap);

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
