using EPiServer.Forms.Implementation.Elements;
using EPiServer.Security;
using nugets_in_cms_12.Models.Pages;

namespace nugets_in_cms_12.Services
{
    public class ContentEventsService(ContentAssetHelper contentAssetHelper, 
        IContentRepository contentRepository) : IContentEventsService
    {
        public void OnCreatedContent(object sender, ContentEventArgs e)
        {
            if (e.Content is ArticlePage articlePage)
            {
               CreateDefaultFormWithElements(articlePage);
            }
        }

        private ContentReference CreateDefaultFormWithElements(ArticlePage articlePage)
        {
            // Get form asset folder
            var formAssetFolder = contentAssetHelper.GetOrCreateAssetFolder(articlePage.ContentLink);

            // Create new form container block
            var formContainerBlock = contentRepository.GetDefault<FormContainerBlock>(formAssetFolder.ContentLink);
            (formContainerBlock as IContent).Name = $"Form Container for {articlePage.Name}";
            formContainerBlock.Form.Name = $"Form Container for {articlePage.Name}";

            contentRepository.Publish(formContainerBlock as IContent, AccessLevel.NoAccess);


            // Create TextBox Element
            var myTextBoxElement = contentRepository.GetDefault<TextboxElementBlock>(formAssetFolder.ContentLink);
            myTextBoxElement.Content.Name = "Your first name";
            myTextBoxElement.PredefinedValue = "Is it Eric?";
            myTextBoxElement.Validators = "EPiServer.Forms.Implementation.Validation.RequiredValidator";

            // Create TextArea Element
            var myTextareaElement = contentRepository.GetDefault<TextareaElementBlock>(formAssetFolder.ContentLink);
            myTextareaElement.Content.Name = "Your thoughts on this presentation";

            // Create SubmitButton Element
            var mySubmitButtonElement = contentRepository.GetDefault<SubmitButtonElementBlock>(formAssetFolder.ContentLink);
            mySubmitButtonElement.Content.Name = "Submit";

            // Publish created elements
            contentRepository.Publish(myTextBoxElement as IContent, AccessLevel.NoAccess);
            contentRepository.Publish(myTextareaElement as IContent, AccessLevel.NoAccess);
            contentRepository.Publish(mySubmitButtonElement as IContent, AccessLevel.NoAccess);

            // Add created elements to created form container block
            var myTextBoxElementItem = new ContentAreaItem { ContentLink = myTextBoxElement.Content.ContentLink };
            var myTextareaElementItem = new ContentAreaItem { ContentLink = myTextareaElement.Content.ContentLink };
            var mySubmitButtonElementItem = new ContentAreaItem { ContentLink = mySubmitButtonElement.Content.ContentLink };

            formContainerBlock.ElementsArea ??= new ContentArea();
            formContainerBlock.ElementsArea.Items.Add(myTextBoxElementItem);
            formContainerBlock.ElementsArea.Items.Add(myTextareaElementItem);
            formContainerBlock.ElementsArea.Items.Add(mySubmitButtonElementItem);

            contentRepository.Publish(formContainerBlock as IContent, AccessLevel.NoAccess);

            // Assign the form container block with the elements to the page
            var articlePageClone = (ArticlePage)articlePage.CreateWritableClone();
            
            var contentAreaItem = new ContentAreaItem
            {
                ContentLink = formContainerBlock.Content.ContentLink
            };

            articlePageClone.MainContentArea ??= new ContentArea();
            articlePageClone.MainContentArea.Items.Add(contentAreaItem);

            return contentRepository.Publish(articlePageClone, AccessLevel.NoAccess);
        }

        public void OnPublishedContent(object sender, ContentEventArgs e)
        {
        }

        public void OnPublishingContent(object sender, ContentEventArgs e)
        {
        }

        public void OnMovingContent(object sender, ContentEventArgs e)
        {
        }

        public void OnMovedContent(object sender, ContentEventArgs e)
        {
        }
    }
}