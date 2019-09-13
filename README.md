# Async-ChatServer

## Running in Docker
##### This section will instruct you in how to get this project up and running in a Docker container

Make app ready for deployment
```dotnet publish -c Release```

Build Docker image  
```docker build -t chatserver -f Dockerfile .```

Create docker container from Docker image
```docker create --name chat chatserver```

Make sure container has been created  
```docker ps -a```

Run container and attach STDOUT
```docker start --attach name```