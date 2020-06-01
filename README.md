# Overview

All in one finance API

## Docker

1. Get container IP Address

```ps
docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' aiof
```

`docker pull gkama/aiof-api`