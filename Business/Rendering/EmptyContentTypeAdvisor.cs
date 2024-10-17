using EPiServer.Cms.Shell.UI.Rest;

namespace nugets_in_cms_12.Business.Rendering
{
    public class EmptyContentTypeAdvisor : IContentTypeAdvisor
    {
        public IEnumerable<int> GetSuggestions(IContent parent, bool contentFolder, IEnumerable<string> requestedTypes)
        {
            return [];
        }
    }
}