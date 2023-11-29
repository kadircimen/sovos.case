namespace Core.CrossCuttingConcerns.Logging;
public class ElasticSearchConfiguration
{
    public string ConnectionString { get; set; }

    public ElasticSearchConfiguration()
    {
        ConnectionString = string.Empty;
    }
}