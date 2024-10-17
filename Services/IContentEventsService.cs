namespace nugets_in_cms_12.Services
{
    public interface IContentEventsService 
    {
        void OnCreatedContent(object sender, ContentEventArgs e);

        void OnPublishedContent(object sender, ContentEventArgs e);

        void OnPublishingContent(object sender, ContentEventArgs e);

        void OnMovingContent(object sender, ContentEventArgs e);

        void OnMovedContent(object sender, ContentEventArgs e);
    }
}