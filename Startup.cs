using nugets_in_cms_12.Extensions;
using EPiServer.Cms.Shell;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Initialization;
using Advanced.CMS.GroupingHeader;
using Alloy.HideTabs;
using nugets_in_cms_12.Services;

namespace nugets_in_cms_12;

public class Startup(IWebHostEnvironment webHostingEnvironment)
{
    public void ConfigureServices(IServiceCollection services)
    {
        if (webHostingEnvironment.IsDevelopment())
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(webHostingEnvironment.ContentRootPath, "App_Data"));

            services.Configure<SchedulerOptions>(options => options.Enabled = false);
        }

        services
            .AddCmsAspNetIdentity<ApplicationUser>()
            .AddCms()
            .AddAlloy()
            .AddAdminUserRegistration()
            .AddEmbeddedLocalization<Startup>();

        services.AddTransient<IContentEventsService, ContentEventsService>();

        // Required by Wangkanai.Detection
        services.AddDetection();

        // Geta Content Type Icons
        services.AddContentTypeIcons(o =>
        {
            o.EnableTreeIcons = true;
            o.ForegroundColor = "#ffffff";
            o.BackgroundColor = "#02423F";
            o.FontSize = 40;
            o.CachePath = "[appDataPath]\\thumb_cache\\";
            o.CustomFontPath = "[appDataPath]\\fonts\\";
        });

        // Hide Tabs
        services.AddAlloyHideTabs();
        
        // Advanced Cms Grouping
        services.AddGroupingHeader();

        // Add custom TinyMce configurations
        services.AddTinyMceCustomConfiguration();

        // Remove built in suggested content types in edit mode
        services.RemoveContentTypeAdvisor();
        
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IContentEvents contentEvents)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Optimizely Content Events
        var contentEventsService = ServiceLocator.Current.GetInstance<IContentEventsService>();

        contentEvents.PublishedContent += contentEventsService.OnPublishedContent;
        contentEvents.PublishingContent += contentEventsService.OnPublishingContent;
        contentEvents.MovingContent += contentEventsService.OnMovingContent;
        contentEvents.MovedContent += contentEventsService.OnMovedContent;
        contentEvents.CreatedContent += contentEventsService.OnCreatedContent;

        // Required by Wangkanai.Detection
        app.UseDetection();
        app.UseSession();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        // Geta Content Type Icons
        app.UseContentTypeIcons();

        // Replace built in login image witg custom 
        app.UseCustomLoginBackgroundImage();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapContent();
        });
    }
}
