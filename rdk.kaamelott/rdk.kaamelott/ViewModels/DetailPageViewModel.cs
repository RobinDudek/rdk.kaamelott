using Plugin.Share;
using Plugin.Share.Abstractions;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using rdk.kaamelott.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace rdk.kaamelott.ViewModels
{
    public class DetailPageViewModel : BaseViewModel
    {
        #region Properties
        private INavigation Navigation;

        private Sample sample;
        public Sample Sample
        {
            get { return sample; }
            set { sample = value; }
        }
        #endregion

        public DetailPageViewModel(INavigation navigation, Sample sample)
        {
            Navigation = navigation;

            Title = "Détail d'un sample";

            Sample = sample;

            PlaySampleCommand = new Command(async () => await ExecutePlaySampleCommand());
            TextToSpeechCommand = new Command(async () => await CrossTextToSpeech.Current.Speak(
                Sample.Title, 
                new CrossLocale{ Language="fr", Country="fr" }));
            ShareCommand = new Command(async () => await ExecuteShareCommand());
        }

        public Command PlaySampleCommand { get; set; }
        private async Task ExecutePlaySampleCommand()
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    if (Sample != null)
                    {
                        var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                        var stream = Resources.Resources.GetSampleStreamFromResources(Sample.File);
                        if (stream != null)
                        {
                            player.Load(stream);
                            player.Play();
                        }
                    }
                });
            }
            catch { }
        }

        public Command TextToSpeechCommand { get; set; }

        public Command ShareCommand { get; set; }
        private async Task ExecuteShareCommand()
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    if (!CrossShare.IsSupported)
                        return;

                    CrossShare.Current.Share(new ShareMessage
                    {
                        Title = "Ecoute ce sample de fou",
                        Text = "Ce sample de Kaamelott est génial: " + Sample.Title,
                        Url = "https://github.com/rdk74/xam.kaamelott"
                    });
                });
            }
            catch { }
        }
    }
}
