using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace Android7
{
    public partial class MainPage : ContentPage
    {


        public MainPage() // just for the designer preview
        {
            InitializeComponent();
        }


        public void Start(BaseUdpReceiver udpReceiver)
        {
            udpReceiver.Updated += new EventHandler(UdpReceiver_Updated);
        }


        private void UdpReceiver_Updated(object sender, EventArgs e)
        {
            BaseUdpReceiver udpReceiver = (BaseUdpReceiver)sender;
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BatchBegin();
                speed.Text = udpReceiver.Info.speed.ToString();
                gear.Text = udpReceiver.Info.gear;
                slipFL.Color = udpReceiver.Info.slipFL;
                slipFR.Color = udpReceiver.Info.slipFR;
                slipRL.Color = udpReceiver.Info.slipRL;
                slipRR.Color = udpReceiver.Info.slipRR;
                dirtFL.Color = udpReceiver.Info.dirtFL;
                dirtFR.Color = udpReceiver.Info.dirtFR;
                dirtRL.Color = udpReceiver.Info.dirtRL;
                dirtRR.Color = udpReceiver.Info.dirtRR;
                rpm.WidthRequest = udpReceiver.Rpm1024();
                rpm.Color = udpReceiver.RpmColor();
                gearAuto.Text = udpReceiver.Info.gearAuto ? "Gear Auto" : "Gear Manual";
                clutch.HeightRequest = udpReceiver.Info.clutch * 325;
                brake.HeightRequest = udpReceiver.Info.brake * 325;
                accel.HeightRequest = udpReceiver.Info.accel * 325;
                Distance.Text = ((Single)udpReceiver.InfoExtra.DistanceTraveled ).ToString("0.0");
                Lap.Text = (udpReceiver.InfoExtra.CompletedLaps + 1).ToString() + " / " + udpReceiver.InfoExtra.NumberOfLaps.ToString();
                if (udpReceiver.InfoExtra.FuelAvg == 0)
                {
                    FuelKMs.Text = "-";
                    FuelAvg.Text = "-";
                }
                else
                {
                    FuelKMs.Text = ((Single)udpReceiver.InfoExtra.Fuel / udpReceiver.InfoExtra.FuelAvg * 10).ToString("0");
                    FuelAvg.Text = (udpReceiver.InfoExtra.FuelAvg).ToString(udpReceiver.InfoExtra.FuelAvg < 10 ? "0.0" : "0");
                }

                this.BatchCommit();
            });
        }


    }
}
