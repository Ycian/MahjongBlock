using System;
using Xamarin.Forms;
using Plugin.SimpleAudioPlayer;
using System.Reflection;
using System.IO;

namespace DragNDrop
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        GamePage gp;
        private async void startClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage(), true);
        }
    }
}

