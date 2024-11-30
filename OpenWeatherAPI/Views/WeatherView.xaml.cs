namespace OpenWeatherAPI.Views;
using OpenWeatherAPI.ViewModels;

public partial class WeatherView : ContentPage
{
	public WeatherView()
	{
		InitializeComponent();
        BindingContext = new WeatherViewModel();

    }
}