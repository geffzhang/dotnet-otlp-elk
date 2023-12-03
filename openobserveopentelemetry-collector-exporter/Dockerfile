FROM otel/opentelemetry-collector-contrib:0.90.1

ARG Openobserve_Authorization
ENV Openobserve_Authorization=${Openobserve_Authorization}

COPY openobserveopentelemetry-collector-exporter/config.yaml /etc/otel-collector-config.yml

# pprof extension
EXPOSE 1888
# Prometheus metrics exposed by the collector
EXPOSE 8888
# Prometheus exporter metrics
EXPOSE 8889
# health_check extension
EXPOSE 13133 
# OTLP gRPC receiver
EXPOSE 4317  
# OTLP http receiver 
EXPOSE 4318  
# zpages extension
EXPOSE 55679 

CMD ["--config=/etc/otel-collector-config.yml"]