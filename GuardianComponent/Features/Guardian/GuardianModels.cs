using static GuardianComponent.Features.Guardian.SearchResult;
using static GuardianComponent.Features.Guardian.SectionResult;
using static GuardianComponent.Features.Guardian.TagResult;

namespace GuardianComponent.Features.Guardian;


public record SectionResult(SectionsResponse response)
{
    public record SectionsResponse(string status, string userTier, int total, Result[] results);
    public record Result(string id, string webTitle, string webUrl, string apiUrl, Edition[] editions);
    public record Edition(string id, string webTitle, string webUrl, string apiUrl, string code);
}

public enum TagType { blog, contributor, keyword, newspaperbook, newspaperbooksection, publication, series, tone, type, all }
public record TagQuery(string apikey, string format, string query, string webTitle, string section, int page);

public record TagResult(TagResponse response)
{
    public record TagResponse(string status, string userTier, int total, int pageSize, int currentPage, int pages, TagItem[] results);
    public record TagItem(string id, string type, string webTitle, string webUrl, string apiUrl, string sectionId, string sectionName);
}

public record SearchResult(SearchResponse response)
{
    public record SearchResponse(string status, string userTier, int total, int pageSize, int currentPage, int pages, string orderBy, SearchItem[] results);
    public record SearchItem(string id, string type, string sectionId, string sectionName, string webPublicationDate, string webTitle, string webUrl, string apiUrl, bool isHosted, string pillarId, string pillarName);
}


