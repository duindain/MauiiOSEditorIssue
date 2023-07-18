using MauiBrokenEditoriOS.Views;

namespace MauiBrokenEditoriOS;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    void Button_Clicked_AutoGrow(System.Object sender, System.EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushAsync(new EditorAutoGrowPage());
    }

    void Button_Clicked_ScrollView(System.Object sender, System.EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushAsync(new EditorInScrollViewPage());
    }

    void Button_Clicked_Default(System.Object sender, System.EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushAsync(new DefaultEditorPage());
    }

    void Button_Clicked_Grid(System.Object sender, System.EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushAsync(new EditorInGrid());
    }

    void Button_Clicked_Height(System.Object sender, System.EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushAsync(new EditorWithHeight());
    }
}


