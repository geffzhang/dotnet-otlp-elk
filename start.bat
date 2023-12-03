docker build -t aspcore-service-a -f ApiApplication1\Dockerfile .
docker build -t aspcore-service-b -f ApiApplication2\Dockerfile .
docker build -t aspcore-service-c -f WebApplication1\Dockerfile .
docker build -t openobserveopentelemetry-collector-exporter -f openobserveopentelemetry-collector-exporter\Dockerfile .
docker-compose up