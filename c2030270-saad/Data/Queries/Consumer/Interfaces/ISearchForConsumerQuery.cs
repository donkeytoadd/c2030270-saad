namespace c2030270_saad.Data.Queries.Consumer.Interfaces
{
    using Resources.Consumer;

    public interface ISearchForConsumerQuery
    {
        List<SearchConsumerResult> Execute(string query, int tenantId);
    }
}