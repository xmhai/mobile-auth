using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IdentityPass.Droid.Services;
using IdentityPass.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXing.Mobile;

[assembly: Xamarin.Forms.Dependency(typeof(QRCodeScanner))]
namespace IdentityPass.Droid.Services
{
    public class QRCodeScanner : IQRCodeScanner
    {
        ZXing.Mobile.MobileBarcodeScanner scanner = null;

        public QRCodeScanner()
        {
            Console.WriteLine("Create Scanner");
            scanner = new ZXing.Mobile.MobileBarcodeScanner();
        }
        public async System.Threading.Tasks.Task<string> ScanAsync()
        {
            try
            {
                Console.WriteLine("Scanning Barcode...");

                var result = await scanner.Scan();

                if (result != null)
                {
                    Console.WriteLine("Scanned Barcode: " + result.Text);
                    return result.Text;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
        }
    }
}

