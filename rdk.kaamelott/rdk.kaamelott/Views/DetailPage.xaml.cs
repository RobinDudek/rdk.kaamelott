using rdk.kaamelott.Models;
using rdk.kaamelott.ViewModels;
using Xamarin.Forms;

namespace rdk.kaamelott.Views
{
	public partial class DetailPage : ContentPage
	{
		public DetailPage (Sample sample)
		{
			InitializeComponent ();
            BindingContext = new DetailPageViewModel(Navigation, sample);
		}
	}
}