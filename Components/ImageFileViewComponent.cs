using nugets_in_cms_12.Models.Media;
using nugets_in_cms_12.Models.ViewModels;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;

namespace nugets_in_cms_12.Components;

/// <summary>
/// Controller for the image file.
/// </summary>
public class ImageFileViewComponent : PartialContentComponent<ImageFile>
{
    private readonly UrlResolver _urlResolver;

    public ImageFileViewComponent(UrlResolver urlResolver)
    {
        _urlResolver = urlResolver;
    }

    /// <summary>
    /// The index action for the image file. Creates the view model and renders the view.
    /// </summary>
    /// <param name="currentContent">The current image file.</param>
    protected override IViewComponentResult InvokeComponent(ImageFile currentContent)
    {
        var model = new ImageViewModel
        {
            Url = _urlResolver.GetUrl(currentContent.ContentLink),
            Name = currentContent.Name,
            Copyright = currentContent.Copyright
        };

        return View(model);
    }
}
