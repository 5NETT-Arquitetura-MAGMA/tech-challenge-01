FROM prom/prom/prometheus:latest as image-prometheus-base
COPY ./prometheus.yml /etc/prometheus

VOLUME ./data
EXPOSE 7070