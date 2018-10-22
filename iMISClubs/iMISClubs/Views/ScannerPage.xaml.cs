using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using iMISClubs.Models;
using iMISClubs.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using Button = Xamarin.Forms.Button;

namespace iMISClubs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        readonly ZXingScannerView scannerView;
        readonly ZXingDefaultOverlay scannerOverlay;
        ObservableCollection<RosterMember> members;
        public ScannerPage()
        {
            InitializeComponent();
            scannerView = new ZXingScannerView { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center};
            var mockStore = new MockDataStore();
            members = new ObservableCollection<RosterMember>();
            var stackLayout = new StackLayout {BackgroundColor = Color.LightGray};
            scannerView.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () =>
                {
                    scannerView.IsAnalyzing = false;
                    // TODO invoke imis web service; send barcoded data and memeber data
                    var member = await mockStore.GetItemAsync(result.Text);
                    if(stackLayout.Children.Count > 0)
                        stackLayout.Children.RemoveAt(0);
                    if (member != null)
                    {
                        
                        var memberStackLayout = new StackLayout();
                        var memberStatusLabel = new Label();
                        member.Status = SwitchStatus(member.Status);
                        await mockStore.UpdateItemAsync(member);
                        // ReSharper disable once UseStringInterpolation
                        memberStatusLabel.Text = string.Format(@"{0} has been {1} at {2}", member.FullName, GetStatusText(member.Status), DateTime.Now.ToShortTimeString());
                        memberStatusLabel.BackgroundColor = Color.FromHex("#dff0d8");
                        var button = new Button();
                        button.Text = "Undo";
                        button.BackgroundColor = Color.FromHex("#e98300");
                        button.TextColor = Color.White;
                        button.Clicked += async delegate
                        {
                            if (stackLayout.Children.Count > 0)
                                stackLayout.Children.RemoveAt(0);
                            member.Status = SwitchStatus(member.Status);
                            await mockStore.UpdateItemAsync(member);
                            // ReSharper disable once UseStringInterpolation
                            memberStatusLabel.Text = string.Format(@"{0} has been {1} at {2}", member.FullName, GetStatusText(member.Status), DateTime.Now.ToShortTimeString());
                            stackLayout.Children.Add(memberStackLayout);
                        };

                        memberStackLayout.Children.Add(memberStatusLabel);
                        memberStackLayout.Children.Add(button);
                        stackLayout.Children.Add(memberStackLayout);

                    }
                    else
                    {
                        var memberStackLayout = new StackLayout();
                        var memberStatusLabel = new Label();
                        var memberNameField = new Entry();
                        // ReSharper disable once UseStringInterpolation
                        memberStatusLabel.Text = string.Format(@"Youth member not found {0}", result.Text);
                        memberStatusLabel.BackgroundColor = Color.FromHex("#FEEFB3");
                        var button = new Button();
                        button.Text = "Add";
                        button.BackgroundColor = Color.FromHex("#e98300");
                        button.TextColor = Color.White;
                        button.Clicked += async delegate
                        {
                            if (button.Text.Equals("Add"))
                            {
                                if (stackLayout.Children.Count > 0)
                                    stackLayout.Children.RemoveAt(0);
                                var newMember = new RosterMember();
                                newMember.Id = result.Text;
                                newMember.FullName = string.IsNullOrEmpty(memberNameField.Text) ? memberNameField.Text : "new member";
                                newMember.Status = CheckInStatus.CheckedIn;
                                await mockStore.AddItemAsync(newMember);
                                if (stackLayout.Children.Count > 0)
                                    stackLayout.Children.RemoveAt(0);
                                // ReSharper disable once UseStringInterpolation
                                memberStatusLabel.Text = string.Format(@"{0} has been added and {1} at {2}", result.Text, newMember.Status, DateTime.Now.ToShortTimeString());
                                button.Text = "Undo";
                            }
                            else if (button.Text.Equals("Undo"))
                            {
                                member = await mockStore.GetItemAsync(result.Text);
                                if (member != null)
                                {
                                    member.Status = SwitchStatus(member.Status);
                                    await mockStore.UpdateItemAsync(member);
                                }
                                
                                // ReSharper disable once UseStringInterpolation
                                memberStatusLabel.Text = string.Format(@"{0} has been {1} at {2}", member.FullName, GetStatusText(member.Status), DateTime.Now.ToShortTimeString());

                            }
                            memberStackLayout = new StackLayout();
                            memberStackLayout.Children.Add(memberStatusLabel);
                            memberStackLayout.Children.Add(button);
                            stackLayout.Children.Add(memberStackLayout);
                        };
                        memberStackLayout.Children.Add(memberStatusLabel);
                        memberStackLayout.Children.Add(memberNameField);
                        memberStackLayout.Children.Add(button);
                        stackLayout.Children.Add(memberStackLayout);

                    }

                    await Task.Delay(2000);
                    scannerView.IsAnalyzing = true;
                });

            scannerOverlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone over youth member code to check in",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = scannerView.HasTorch,
            };
            scannerOverlay.Children.AddVertical(stackLayout);

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


        private string GetStatusText(CheckInStatus memberStatus)
        {
            switch (memberStatus)
            {
                case CheckInStatus.New:
                    return "new";
                case CheckInStatus.CheckedIn:
                    return "checked in";
                case CheckInStatus.CheckedOut:
                    return "checked out";
            }
            return "checked in";
        }

        private static CheckInStatus SwitchStatus(CheckInStatus memberStatus)
        {
            switch (memberStatus)
            {
                case CheckInStatus.New:
                    return CheckInStatus.CheckedIn;
                case CheckInStatus.CheckedIn:
                    return CheckInStatus.CheckedOut;
                case CheckInStatus.CheckedOut:
                    return CheckInStatus.CheckedIn;
            }
            return CheckInStatus.CheckedIn;
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