FROM envoyproxy/envoy:v1.29-latest
EXPOSE 80
EXPOSE 443

COPY apigateways/envoy/envoy.yaml /etc/envoy.yaml
COPY apigateways/envoy/docker-entrypoint.sh /

RUN chmod 500 /docker-entrypoint.sh

ENTRYPOINT ["/docker-entrypoint.sh"]