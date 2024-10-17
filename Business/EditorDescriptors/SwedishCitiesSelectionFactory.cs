using EPiServer.Shell.ObjectEditing;

namespace nugets_in_cms_12.Business.EditorDescriptors
{
    public class SwedishCitiesSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new List<SelectItem>
            {
                new() { Text = "Stockholm", Value = "Stockholm" },
                new() { Text = "Gothenburg", Value = "Gothenburg" },
                new() { Text = "Malmö", Value = "Malmö" },
                new() { Text = "Uppsala", Value = "Uppsala" },
                new() { Text = "Västerås", Value = "Västerås" },
                new() { Text = "Örebro", Value = "Örebro" },
                new() { Text = "Linköping", Value = "Linköping" },
                new() { Text = "Helsingborg", Value = "Helsingborg" },
                new() { Text = "Jönköping", Value = "Jönköping" },
                new() { Text = "Norrköping", Value = "Norrköping" },
                new() { Text = "Lund", Value = "Lund" },
                new() { Text = "Umeå", Value = "Umeå" },
                new() { Text = "Gävle", Value = "Gävle" },
                new() { Text = "Borås", Value = "Borås" },
                new() { Text = "Eskilstuna", Value = "Eskilstuna" },
                new() { Text = "Södertälje", Value = "Södertälje" },
                new() { Text = "Karlstad", Value = "Karlstad" },
                new() { Text = "Trollhättan", Value = "Trollhättan" },
                new() { Text = "Halmstad", Value = "Halmstad" },
                new() { Text = "Luleå", Value = "Luleå" },
                new() { Text = "Kalmar", Value = "Kalmar" },
                new() { Text = "Växjö", Value = "Växjö" },
                new() { Text = "Kristianstad", Value = "Kristianstad" },
                new() { Text = "Skövde", Value = "Skövde" },
                new() { Text = "Karlskrona", Value = "Karlskrona" },
                new() { Text = "Sundsvall", Value = "Sundsvall" },
                new() { Text = "Trelleborg", Value = "Trelleborg" },
                new() { Text = "Falkenberg", Value = "Falkenberg" },
                new() { Text = "Mölndal", Value = "Mölndal" },
                new() { Text = "Falun", Value = "Falun" },
                new() { Text = "Borlänge", Value = "Borlänge" },
                new() { Text = "Varberg", Value = "Varberg" },
                new() { Text = "Vänersborg", Value = "Vänersborg" },
                new() { Text = "Östersund", Value = "Östersund" },
                new() { Text = "Nyköping", Value = "Nyköping" },
                new() { Text = "Visby", Value = "Visby" },
                new() { Text = "Ystad", Value = "Ystad" },
                new() { Text = "Katrineholm", Value = "Katrineholm" },
                new() { Text = "Landskrona", Value = "Landskrona" },
                new() { Text = "Motala", Value = "Motala" }
            };
        }
    }
}