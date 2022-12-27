using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Graphics.Skia;

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
        //var stream = await drawView.GetImageStream(200, 200);
        var dvStream = await DrawingView.GetImageStream(drawView.Lines, new Size(200,200), Colors.LightBlue);
        
        // ^ setting the stream size appears to do NOTHING ^

        string fileName = Path.Combine(FileSystem.Current.AppDataDirectory, "mypic.png");

        // This wont work, results in a blank file due to no format specified (and no ability to specify it. yay)
        //using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        //{
        //    await dvStream.CopyToAsync(dvStream);
        //}

        // convert raw DrawingView stream to IImage so we can set the file format
        var image = SkiaImage.FromStream(dvStream, ImageFormat.Png);
            
        using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
            await image.SaveAsync(fileStream);
        }
        
        // dvStream.Position = 0;
        // send the captured drawing out to UI, Image control
        // (stopped working when I added file save, not sure why ??)
        drawPreview.Source = ImageSource.FromStream(() => dvStream);
    }
}

