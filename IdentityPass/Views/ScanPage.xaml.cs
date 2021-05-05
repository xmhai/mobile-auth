using IdentityPass.Services;
using IdentityPass.ViewModels;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IdentityPass.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
            Console.WriteLine("ScanPage Constructor is called...");
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        protected override void OnAppearing()
        {
            Console.WriteLine("ScanPage OnAppearing...");

            infoGroup.IsVisible = false;

            scanner.IsVisible = true;
            scanner.IsScanning = true;

            base.OnAppearing();

            //DependencyService.Get<IQRCodeScanner>().ScanAsync();
        }

        void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(()=>
                {
                    if (result != null)
                    {
                        infoGroup.IsVisible = true;
                        var url = Regex.Match(result.Text, @"(http:|https:)\/\/(.*?)\/");
                        scanUrl.Text = url.Value;
                        var dateTime = DateTime.Now;
                        scanDate.Text = "Date: " + dateTime.ToLongDateString();
                        scanTime.Text = "Time: " + dateTime.ToShortTimeString();

                        scanner.IsScanning = false;
                        scanner.IsVisible = false;
                    }
                }
            );
        }

        void CancelButton_OnClicked(object sender, EventArgs e)
        {
            //Navigation.GoToAsync("//AboutPage");
            (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//AboutPage", true);
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
            if (!isFingerprintAvailable)
            {
                await DisplayAlert("Error", "Biometric authentication is not available or is not configured.", "OK");
                return;
            }

            AuthenticationRequestConfiguration conf = new AuthenticationRequestConfiguration("Authentication", "Authenticate access to your personal data");

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(conf);
            if (authResult.Authenticated)
            {
                //Success
                await DisplayAlert("Success", "Authentication succeeded", "OK");
            }
        }
    }
}