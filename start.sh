cd coordinador-rest
npm install
cd ../transform-data
npm install
docker-compose up -d --build 
cd ../RegistryRoutingApp
docker-compose up -d --build
cd ../DispatcherApp
docker-compose up -d --build