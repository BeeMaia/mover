FROM envoyproxy/envoy-alpine:v1.21-latest
EXPOSE 80
EXPOSE 443

COPY apigateways/envoy/envoy.yaml /etc/envoy.yaml
COPY apigateways/envoy/docker-entrypoint.sh /

ENTRYPOINT ["/docker-entrypoint.sh"]