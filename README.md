# dotnet-otlp-openobserve
Dotnet with OpenTelemetry + OpenObserve - 1 Web + 2 API ,  A sample code showing how to instrument three asp.net core 7 microservices for distributed tracing and metrics with OTLP and OpenObserve. The local OTLP spans are collected by the OpenTelemetry collector, and exported to OpenObserve collector exporter over HTTP. 
Serilog Sink for OpenObserve is an extension that integrates Serilog, a favored logging library for .NET applications, with OpenObserve. Crafted by Konrad Kaminski-Pawlak, the sink allows for effortless logging to OpenObserve, thereby enhancing the ability to store, analyze, and manage logs. See [Serilog Sink for OpenObserve](https://openobserve.ai/blog/serilog-sink-for-openobserve#introducing-serilog-sink-for-openobserve)

## Requirenments
Docker with OpenObserve
See [https://openobserve.ai/docs/quickstart/#openobserve-cloud](https://openobserve.ai/docs/quickstart/#openobserve-cloud)

Run with
```
mkdir data
docker run -v $PWD/data:/data -e ZO_DATA_DIR="/data" -p 5080:5080 \
    -e ZO_ROOT_USER_EMAIL="root@example.com" -e ZO_ROOT_USER_PASSWORD="Complexpass#123" \
    public.ecr.aws/zinclabs/openobserve:latest
```

### Start Notes
The first try the Kibana didn't start with the user - kibana_system
Had to change the password "changeme" of "kibana_system"
```
docker-compose exec elasticsearch bin/elasticsearch-reset-password --batch --user kibana_system
```	
Then copy the new pass to .env

### Testing with ElasticSearch - AppSearch
To enable the management experience for Enterprise Search, modify the Kibana configuration file in
[`kibana/config/kibana.yml`][config-kbn] and add the following setting:
```yaml
enterpriseSearch.host: http://enterprise-search:3002
```	


## Project dotnet
Set multiple Start Visual Studio - Web, Api1, Api2
Will start the https profile configs 

### Configurations
- ApiApplication1 (calls api2)
```
  "Api2Url": "https://localhost:7132",
  "elk-apm-server": "http://localhost:8200"
``` 

- ApiApplication2 (standalone)
```
  "elk-apm-server": "http://localhost:8200"
```


- WebApplication1 (calls api1 & api2)
```
  "elk-apm-server": "http://localhost:8200",

  "Api1Url": "https://localhost:7199",
  "Api2Url": "https://localhost:7132"
```


## DEMO
### ServiceMap 
![ServiceMap .](/assets/images/DEMO-servicemap.png)

### Trace Timeline
![trace-timeline](/assets/images/DEMO-trace-timeline.png)

### Discover
![discover.](/assets/images/DEMO-discover.png)

## DEMO Log Exception
### Trace Timeline
![trace-timeline](/assets/images/DEMO-trace-timeline-exception.png)

### Observability To Discover
![Observability To Discover.](/assets/images/DEMO-observabilityToDiscover.png)



## ElasticSearch - API
### Create Index (API) 
![Elasticsearch-Index-API](/assets/images/Elasticsearch-Index-API.png)

### Add Documents via API (curl, postman...)
![AddDocuments](/assets/images/Elasticsearch-Index-AddDocuments.png)

### Browse Documents
![BrowseDocuments](/assets/images/Elasticsearch-Index-BrowseDocuments.png)

### .NET  ElasticSearchClient - Search by index
![DotNetSearch](/assets/images/Elasticsearch-Index-DotNetSearch.png)
