npm install
docker build -t transform-data .
docker run -d -p 8081:8081 --name transform-data transform-data