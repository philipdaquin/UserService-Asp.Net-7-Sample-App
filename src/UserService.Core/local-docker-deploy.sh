docker network create user-service-local


docker kill user-service
docker rm user-service

# Build 
dotnet build

# Test 
dotnet test

# Restore as distinct layers 
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# Docker build image
docker build -t user-service .

docker tag user-service philipasd/user-service:v0.0.0

# docker push philipasd/user-service:v0.0.0

# Run the user_api container
# docker run --name user-service -d -t --link postgres-db:postgres -p 7000:7000 user-service