
# Build stage
FROM golang:1.25.5-alpine AS builder

WORKDIR /app

COPY go.mod ./
COPY *.go ./

RUN CGO_ENABLED=0 GOOS=linux go build \
    -trimpath \
    -ldflags="-s -w" \
    -o banksystem .

# Runtime stage
FROM alpine:3.22

WORKDIR /app

RUN adduser -D -g '' appuser

COPY --from=builder /app/banksystem ./banksystem

USER appuser

ENTRYPOINT ["./banksystem"]
