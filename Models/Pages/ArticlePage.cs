using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Advanced.CMS.GroupingHeader;
using Alloy.HideTabs.VisibilityAttributes;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using nugets_in_cms_12.Business.EditorDescriptors;
using nugets_in_cms_12.Business.Queries;

namespace nugets_in_cms_12.Models.Pages;

/// <summary>
/// Used primarily for publishing news articles on the website
/// </summary>
[SiteContentType(
    GroupName = Globals.GroupNames.News,
    GUID = "AEECADF2-3E89-4117-ADEB-F8D43565D2F4")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-article.png")]
[TreeIcon(FontAwesome.NewspaperO)]
public class ArticlePage : StandardPage
{
    #region [Backing Types]

    [BackingType(typeof(PropertyAppSettings))]
    public virtual string Ratings { get; set; }

    [BackingType(typeof(PropertyAppSettingsMultiple))]
    public virtual string Animals { get; set; }
    
    #endregion

    #region [Selections]

    [SelectOne(SelectionFactoryType = typeof(SwedishCitiesSelectionFactory))]
    public virtual string City { get; set; }

    [AutoSuggestSelection(typeof (CitiesQuery))]
    [Display(Name = "City (by query)")]
    public virtual string CityQuery { get; set; }

    #endregion

    #region [Groupings]

    [GroupingHeader("Weather")]
    [Display(Name = "Sunny weather? ☀️")]
    public virtual bool WeatherIsSunny { get; set; }

    [Display(Name = "Cloudy weather? 🌥️")]
    public virtual bool WeatherIsCloudy { get; set; }

    [Display(Name = "Rainy weather? 🌧️")]
    public virtual bool WeatherIsRainy { get; set; }

    [GroupingHeader("Food")]
    [Display(Name = "You want beverage? 🍺")]
    public virtual bool IWantBeverage { get; set; }

    [Display(Name = "You want food? 🍛")]
    public virtual bool IWantFood { get; set; }

    [Display(Name = "You're vegetarian? 🥦")]
    public virtual bool IWantVegetarianOption { get; set; }

    #endregion

    #region [Hide Tabs]

    [Display(Name = "Display more food & beverage options")]
    [ShowPropertyWhenValueEquals(nameof(SpecialOrderSummary), true)]
    [ShowPropertyWhenValueEquals(nameof(DiscountCode), true)]
    public virtual bool ShowAdvancedOptions { get; set; }

    [Display(Name = "Special order")]
    [UIHint(UIHint.Textarea)]
    public virtual string SpecialOrderSummary { get; set; }

    [Display(Name = "Do you have a discount code?")]
    public virtual string DiscountCode { get; set; }

    #endregion
    
    public override void SetDefaultValues(ContentType contentType)
    {
        base.SetDefaultValues(contentType);

        VisibleInMenu = false;
    }
}
