using Newtonsoft.Json;
using PostListApp.Models;
#nullable disable

namespace PostListApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        LoadPosts();
    }

    private async void LoadPosts()
    {
        try
        {
            HttpClient client = new HttpClient();
            string url = "https://jsonplaceholder.typicode.com/posts";
            string response = await client.GetStringAsync(url);
            var posts = JsonConvert.DeserializeObject<List<Post>>(response)?.Take(10).ToList();

            // Realistic image-based stories
            var headlines = new List<(string Title, string Body, string Image)>
            {
                ("Sunset Serenity", "A golden evening sky over the calm ocean captures peace and reflection.", "https://picsum.photos/id/1003/600/400"),
                ("Mountain Majesty", "Rising peaks touch the clouds, standing proud and timeless in nature’s silence.", "https://picsum.photos/id/1018/600/400"),
                ("City Lights", "The heartbeat of the city glows at night, alive with endless energy and stories.", "https://picsum.photos/id/1011/600/400"),
                ("Mirror Lake", "A crystal-clear lake reflecting the world in perfect stillness.", "https://picsum.photos/id/1015/600/400"),
                ("Desert Horizon", "Golden dunes and endless skies — a landscape shaped by wind and time.", "https://picsum.photos/id/1002/600/400"),
                ("Ocean Whisper", "Waves crash and fade, leaving trails of foam and memories behind.", "https://picsum.photos/id/1016/600/400"),
                ("Rainy Alley", "Raindrops glisten on cobblestones, painting poetry in motion.", "https://picsum.photos/id/1020/600/400"),
                ("Forest Trail", "Sunbeams break through tall trees, leading the way to peaceful discovery.", "https://picsum.photos/id/1025/600/400"),
                ("Open Road", "The road ahead stretches into freedom — adventure awaits.", "https://picsum.photos/id/1043/600/400"),
                ("Skyline Dreams", "Glass towers reach for the sky, symbols of hope and ambition.", "https://picsum.photos/id/1031/600/400")
            };

            for (int i = 0; i < posts.Count; i++)
            {
                posts[i].title = headlines[i].Title;
                posts[i].body = headlines[i].Body;
                posts[i].imageUrl = headlines[i].Image;
            }

            PostsCollectionView.ItemsSource = posts;
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
        catch
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
            ErrorLabel.Text = "Failed to load posts. Please check your connection.";
            ErrorLabel.IsVisible = true;
        }
    }

    private async void PostsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Post selected)
        {
            await Navigation.PushAsync(new PostDetailPage(selected));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
