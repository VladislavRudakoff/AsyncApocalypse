FROM golang:1.20-alpine AS build-env
RUN apk add --no-cache git
RUN go install go.k6.io/xk6/cmd/xk6@v0.13.0
RUN xk6 build --with github.com/grafana/xk6-dashboard@v0.7.5

FROM alpine:3.15
COPY --from=build-env /go/k6 /usr/bin/k6
ENTRYPOINT ["k6"]
