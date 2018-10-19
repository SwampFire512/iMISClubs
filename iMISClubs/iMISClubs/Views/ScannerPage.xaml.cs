using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace iMISClubs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        ZXingScannerView scannerView;
        ZXingDefaultOverlay scannerOverlay;
        public ScannerPage()
        {
            InitializeComponent();
            scannerView = new ZXingScannerView { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            scannerView.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () =>
                {
                    scannerView.IsAnalyzing = false;
                    // TODO invoke imis web service; send barcoded data and memeber data
                    await DisplayAlert("Checking in youth member", result.Text, "OK");
                    //Navigation??
                });

            scannerOverlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone over youth member code to check in",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = scannerView.HasTorch,
            };
            scannerOverlay.FlashButtonClicked += (sender, e) => {
                scannerView.IsTorchOn = !scannerView.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(scannerView);
            grid.Children.Add(scannerOverlay);

            // The root page of your application
            Content = grid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            scannerView.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            scannerView.IsScanning = false;

            base.OnDisappearing();
        }
    }
}