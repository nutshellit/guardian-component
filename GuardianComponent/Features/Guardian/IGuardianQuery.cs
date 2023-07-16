namespace GuardianComponent.Features.Guardian;

public interface IGuardianQuery
{
    Task<SectionResult> GetGuardianSections();

    Task<TagResult> GetGuardianTags(TagQuery tagQuery );

    Task<SearchResult> GetGuardianSearchResults(string searchQuery, string section, int page = 1);
}
