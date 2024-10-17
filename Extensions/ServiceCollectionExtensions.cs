using nugets_in_cms_12.Business;
using nugets_in_cms_12.Business.Channels;
using nugets_in_cms_12.Business.Rendering;
using EPiServer.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using EPiServer.Cms.Shell.UI.Rest;
using Microsoft.Extensions.DependencyInjection.Extensions;
using EPiServer.Cms.TinyMce.Core;
using nugets_in_cms_12.Models.Pages;

namespace nugets_in_cms_12.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RemoveContentTypeAdvisor(this IServiceCollection services)
    {
        services.RemoveAll<IContentTypeAdvisor>();
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IContentTypeAdvisor, EmptyContentTypeAdvisor>());

        return services;
    }

    public static IServiceCollection AddTinyMceCustomConfiguration(this IServiceCollection services)
    {
        services.Configure((TinyMceConfiguration config) =>
        {
            var tinyMceConfig = config.Default()
                .Clone()
                .Resize(TinyMceResize.Both)
                .AddEpiserverSupport()
                .AddExternalPlugin("youtube", "/ClientResources/Scripts/Editors/youtube.js")
                .Toolbar("youtube | epi-create-block | styles | blocks | bold italic underline | alignleft aligncenter alignright alignjustify | help searchreplace")
                .BlockFormats($"Headline 2=h2; Headline 3=h3; Headline 4=h4; Paragraph=p")
                    .BodyClass("custom_body_css")
                    .Height(768)
                    .Width(640)
                .AddSetting("importcss_append", true)
                .AddSetting("allow_script_urls", true) 
                .AddSetting("extended_valid_elements", "script[language|type|src],iframe[src|alt|title|width|height|align|name|style],picture,source[srcset|media],a[id|href|target|onclick|class|style],span[*],div[*],img[*],b[*],strong[*],i[*],em[*],span[*],table[*]")
                .StyleFormats(
                    new { title = "Set image to left", selector = "img", classes = "left" },
                    new { title = "Set image to right", selector = "img", classes = "right" });

            config.For<ArticlePage>(p => p.MainBody, tinyMceConfig);
        });

        return services;
    }
    
    public static IServiceCollection AddAlloy(this IServiceCollection services)
    {
        services.Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new SiteViewEngineLocationExpander()));

        services.Configure<DisplayOptions>(displayOption =>
        {
            displayOption.Add("full", "/displayoptions/full", Globals.ContentAreaTags.FullWidth, string.Empty, "epi-icon__layout--full");
            displayOption.Add("wide", "/displayoptions/wide", Globals.ContentAreaTags.WideWidth, string.Empty, "epi-icon__layout--wide");
            displayOption.Add("half", "/displayoptions/half", Globals.ContentAreaTags.HalfWidth, string.Empty, "epi-icon__layout--half");
            displayOption.Add("narrow", "/displayoptions/narrow", Globals.ContentAreaTags.NarrowWidth, string.Empty, "epi-icon__layout--narrow");
        });

        services.Configure<MvcOptions>(options => options.Filters.Add<PageContextActionFilter>());

        services.AddDisplayResolutions();
        services.AddDetection();

        return services;
    }

    private static void AddDisplayResolutions(this IServiceCollection services)
    {
        services.AddSingleton<StandardResolution>();
        services.AddSingleton<IpadHorizontalResolution>();
        services.AddSingleton<IphoneVerticalResolution>();
        services.AddSingleton<AndroidVerticalResolution>();
    }
}
