using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WearableCompanion
{
    public partial class MainPage : ContentPage
    {
        public static string ReceivedMessage;
        public static string StaticConnection = "No connection";
        public IProviderService provider { get; set; }
        public MainPage()
        {
            provider = DependencyService.Get<IProviderService>();
            InitializeComponent();
            BindingContext = this;
        }

        public string ConnectionString
        {
            get => StaticConnection;
        }

        private void Button_Clicked_Close(object sender, EventArgs e)
        {
            provider.CloseConnection();
        }


        private string entryString;
        public string EntryString
        {
            get => entryString;
            set
            {
                entryString = value;
                OnPropertyChanged();
            }
        }

        private void Button_Clicked_Send(object sender, EventArgs e)
        {
            provider.SendData(EntryString);
        }

        public string FromWatch
        {
            get => ReceivedMessage;
        }

        private void Button_Clicked_Refresh(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(FromWatch));
            OnPropertyChanged(nameof(ConnectionString));
        }
    }
}
