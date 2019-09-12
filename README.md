# Async-ChatServer

##Running in Docker

Make app ready for deployment
```dotnet publish -c Release```

Build Docker image  
```docker build -t chatserver -f Dockerfile .```

Create docker container from Docker image
```docker create chatserver```

Make sure container has been created  
```docker ps -a```

*Optional: Rename container*  
```docker rename name newname```

Run container  
```docker start name```