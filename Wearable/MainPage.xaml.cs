using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using Samsung.Sap;
using Tizen.Applications;

namespace Wearable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        private Agent Agent;
        private Connection Connection;
        private Peer Peer;
        private Channel ChannelId;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void Button_Clicked_Connect(object sender, EventArgs e)
        {
            Connect();
        }

        private void Button_Clicked_LaunchStore(object sender, EventArgs e)
        {
            DeepLinkLaunchStore();
        }

        private void Button_Clicked_Send(object sender, EventArgs e)
        {
            try
            {
                if (Peer != null)
                {
                    Connection.Send(ChannelId, Encoding.UTF8.GetBytes("Hello from watch!"));
                    ShowMessage("Sent to phone");
                }
                else
                {
                    ShowMessage("Connect to phone first");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("SendMessage error: " + ex);
            }
        }

        // connect to phone using the Samsung.SAP nuget package
        private async void Connect()
        {
            try
            {
                Agent = await Agent.GetAgent("/example/companion");
                var peers = await Agent.FindPeers();
                ChannelId = Agent.Channels.First().Value;
                if (peers.Count() > 0)
                {
                    Console.WriteLine("Peer found");
                    Peer = peers.First();
                    Connection = Peer.Connection;
                    Connection.DataReceived -= Connection_DataReceived;
                    Connection.DataReceived += Connection_DataReceived;
                    await Connection.Open();
                    ShowMessage("Connected");
                }
                else
                {
                    ShowMessage("Peer not found");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message);
            }
        }

        // broadcaster that looks for messages from phone
        private void Connection_DataReceived(object sender, DataReceivedEventArgs e)
        {
            ShowMessage("Message received");
            ReceivedMessage = System.Text.Encoding.ASCII.GetString(e.Data);
        }

        private string receivedMessage = "Label will show message from phone";
        public string ReceivedMessage
        {
            get => receivedMessage;
            set
            {
                receivedMessage = value;
                OnPropertyChanged();
            }
        }

        // toast to show messages
        private void ShowMessage(string message, string debugLog = null)
        {
            Toast.DisplayText(message, 1000);
            if (debugLog != null)
            {
                debugLog = message;
            }
            Console.WriteLine("[DEBUG] " + message);
        }

        private void DeepLinkLaunchStore()
        {
            AppControl launchStore = new AppControl();
            string storeUrl = @"https://play.google.com/store/apps/details?id=[PACKAGE-NAME-HERE]";
            launchStore.Operation = AppControlOperations.Default;
            launchStore.ApplicationId = "com.samsung.w-manager-service";
            launchStore.ExtraData.Add("deeplink", storeUrl);
            launchStore.ExtraData.Add("type", "phone");

            try
            {
                AppControl.SendLaunchRequest(launchStore);
            }
            catch (Exception e)
            {
                Console.WriteLine("Store launch error: " + e);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}