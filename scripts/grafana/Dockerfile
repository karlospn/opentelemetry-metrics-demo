FROM grafana/grafana:10.2.2
COPY ./prometheus-connector.yml  /etc/grafana/provisioning/datasources/ 
COPY ./prometheus-dashboards.yml  /etc/grafana/provisioning/dashboards/
COPY ./dashboards/bookstore-dashboard.json  /var/lib/grafana/dashboards/
COPY ./dashboards/dotnet-performance-counters-dashboard.json  /var/lib/grafana/dashboards/
COPY ./dashboards/aspnet-core-metrics-dashboard.json  /var/lib/grafana/dashboards/
COPY ./dashboards/aspnet-core-endpoints-dashboard.json  /var/lib/grafana/dashboards/
