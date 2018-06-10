using Newtonsoft.Json;
using rdk.kaamelott.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace rdk.kaamelott.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Properties
        private INavigation Navigation;

        private List<Sample> allSamples;
        public List<Sample> AllSamples
        {
            get { return allSamples; }
            set
            {
                allSamples = value;
                OnPropertyChanged();
            }
        }

        private List<Sample> filteredSamples;
        public List<Sample> FilteredSamples
        {
            get { return filteredSamples; }
            set
            {
                filteredSamples = value;
                OnPropertyChanged();
            }
        }

        private Sample selectedSample;
        public Sample SelectedSample
        {
            get { return selectedSample; }
            set
            {
                selectedSample = value;
                OnPropertyChanged();
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value.ToLower();
                ExecuteSearchCommand();
            }
        }
        #endregion

        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Title = "Liste des samples";

            var json = Resources.Resources.GetJSONFromResources("sounds.json");
            AllSamples = JsonConvert.DeserializeObject<List<Sample>>(json);
            AllSamples = AllSamples.OrderBy(x => x.Episode).ToList();
            FilteredSamples = AllSamples;

            GoToDetailCommand = new Command(async (object o) => await ExecuteGoToDetailCommand(o));
        }

        public Command GoToDetailCommand { get; set; }
        private async Task ExecuteGoToDetailCommand(object param)
        {
            try
            {
                var sample = (Sample)param;
                SelectedSample = null;
                await Navigation.PushAsync(new Views.DetailPage(sample));
            }
            catch { }
        }
        
        public Command SearchCommand { get; set; }
        private void ExecuteSearchCommand()
        {
            try
            {
                FilteredSamples = AllSamples.Where(
                          x => x.Title.ToLower().Contains(SearchText)
                            || x.Character.ToLower().Contains(SearchText))
                          .ToList();
            }
            catch { }
        }
    }
}
