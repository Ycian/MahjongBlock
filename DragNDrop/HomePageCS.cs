using Xamarin.Forms;

namespace DragNDrop
{
	public class HomePageCS : ContentPage
	{
		public HomePageCS()
		{
			Content = new StackLayout
			{
				Padding = new Thickness(20),
				Orientation = StackOrientation.Horizontal,
				Children = {
					new PanContainer {
						Content = new Image {
							Source = "dot1.png",
							WidthRequest = 45,
							HeightRequest = 45,
						}
					}
				}
			};
		}
	}
}
