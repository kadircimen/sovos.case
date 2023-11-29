﻿using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Core.CrossCuttingConcerns.Logging;
public class ElasticSearchLogger : LoggerServiceBase
{
    public ElasticSearchLogger(IConfiguration configuration)
    {
        ElasticSearchConfiguration? logConfiguration = configuration
            .GetSection("ElasticSearchConfiguration")
            .Get<ElasticSearchConfiguration>();

        Logger = new LoggerConfiguration().WriteTo
            .Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(logConfiguration.ConnectionString))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
                }
            )
            .CreateLogger();
    }
}
