
docker kill postgres
docker rm postgres
docker pull postgres

docker run -d --name postgres --rm -e POSTGRES_PASSWORD=password -e POSTGRES_USER=postgres -e POSTGRES_DB=userservice -p 5432:5432 postgres

