# Todo App - Backend

## Running locally

### MongoDB

For a docker instance to run Mongo you can use these commands

```bash
docker pull flqw/docker-mongo-local-replicaset
docker run -d -p 27001:27001 -p 27002:27002 -p 27003:27003 --name mongo -e “REPLICA_SET_NAME=mongo-rs” --restart=always flqw/docker-mongo-local-replicaset mongodb://localhost:27001,localhost:27002,localhost:27003/db
```

This will have an instance running at 27001/2/3 so your connection string will be: `mongodb://localhost:27001,localhost:27002,localhost:27003/`
