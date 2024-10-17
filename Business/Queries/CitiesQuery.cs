using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using nugets_in_cms_12.Business.EditorDescriptors;

namespace nugets_in_cms_12.Business.Queries
{
    [ServiceConfiguration(typeof(ISelectionQuery))]
    public class CitiesQuery : ISelectionQuery
    {
        SelectItem[] _items;
        
        public CitiesQuery()
        {
            var factory = new SwedishCitiesSelectionFactory();

            _items = factory.GetSelections(null).OfType<SelectItem>().ToArray();
        }

        public IEnumerable<ISelectItem> GetItems(string query)
        {
            return _items.Where(i => i.Text.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        public ISelectItem GetItemByValue(string value)
        {
            return _items.FirstOrDefault(i => i.Value.Equals(value));
        }
    }
}