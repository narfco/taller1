npm install
docker build -t coordinador-rest .
docker run -d -p 8082:8082 --name coordinador-rest coordinador-rest