using PostListApp.Models;
#nullable disable

namespace PostListApp;

public partial class PostDetailPage : ContentPage
{
    public PostDetailPage(Post post)
    {
        InitializeComponent();

        PostImage.Source = post.imageUrl;
        PostTitle.Text = post.title;
        PostBody.Text = $"{post.body}\n\nEvery picture holds a story. Explore the emotions, colors, and moments behind this visual tale.";
    }
}
