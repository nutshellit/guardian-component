namespace GuardianComponent.Features.Guardian;

public interface IGuardianQuery
{
    Task<SectionResult> GetGuardianSections(string apiKey);

    Task<TagResult> GetGuardianTags(TagQuery tagQuery, string apiKey);

    Task<SearchResult> GetGuardianSearchResults(string apiKey, string searchQuery, string section, int page = 1);
}
