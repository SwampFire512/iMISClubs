using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace iMISClubs.ViewModels
{
    public class ScannerViewModel : BaseViewModel
    {
        //ZXingScannerView scannerView;

        public ScannerViewModel()
        {
            Title = "Check in youth member";

            //OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));

        }

        public ICommand OpenWebCommand { get; }
    }
}