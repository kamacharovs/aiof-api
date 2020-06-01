# Overview

All in one finance API

## Docker

1. Get Docker image

```ps
docker pull gkama/aiof-api
```

3. Get container IP Address

```ps
docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' aiof
```