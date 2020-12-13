using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace Android7
{
    public partial class MainPage2 : ContentPage
    {


        public MainPage2() // just for the designer preview
        {
            InitializeComponent();
        }


        public void Start(BaseUdpReceiver udpReceiver)
        {
            udpReceiver.UpdatedExtra += new EventHandler(UdpReceiver_UpdatedExtra);
        }


        private void UdpReceiver_UpdatedExtra(object sender, EventArgs e)
        {
            BaseUdpReceiver udpReceiver = (BaseUdpReceiver)sender;
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BatchBegin();
                wearFL.Color = udpReceiver.InfoExtra.wearFL;
                wearFR.Color = udpReceiver.InfoExtra.wearFR;
                wearRL.Color = udpReceiver.InfoExtra.wearRL;
                wearRR.Color = udpReceiver.InfoExtra.wearRR;
                Distance.Text = ((Single)udpReceiver.InfoExtra.DistanceTraveled).ToString("0.0");
                Position.Text = udpReceiver.InfoExtra.Position.ToString() + " / " + udpReceiver.InfoExtra.NumCars.ToString();
                Lap.Text = (udpReceiver.InfoExtra.CompletedLaps+1).ToString() + " / " + udpReceiver.InfoExtra.NumberOfLaps.ToString();
                if (udpReceiver.InfoExtra.FuelAvg==0)
                {
                    Fuel.Text = udpReceiver.InfoExtra.Fuel.ToString() + "l   /  -";
                    FuelKKm.Text = "-";
                }
                else
                {
                    Fuel.Text = udpReceiver.InfoExtra.Fuel.ToString() + "l  /  " + ((Single)udpReceiver.InfoExtra.Fuel / udpReceiver.InfoExtra.FuelAvg * 10000).ToString("0") + "Km";
                    FuelKKm.Text = ((Single)udpReceiver.InfoExtra.FuelAvg).ToString(udpReceiver.InfoExtra.FuelAvg < 10 ? "0.0" : "0") + "l /100Km";
                }
                this.BatchCommit();
            });
        }


    }
}
