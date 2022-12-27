using CommunityToolkit.Maui.Core;
using System.Collections.ObjectModel;


namespace DrawingDemo;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
        BindingContext = this;

		InitializeComponent();
	}

    private void clearButton_Clicked(object sender, EventArgs e)
    {
        drawView.Clear();
    }

    private async void drawView_DrawingLineCompleted(object sender, CommunityToolkit.Maui.Core.DrawingLineCompletedEventArgs e)
    {
        var stream = await drawView.GetImageStream(200, 200);
        // makes no difference what values I put in here. Same result with this:
        // var stream = await drawView.GetImageStream(400, 20);

        // ^ setting the stream size appears to do NOTHING ^

        drawPreview.Source = ImageSource.FromStream(() => stream);

    }
}

