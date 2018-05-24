cd coordinador-rest
npm install
cd ../transform-data
npm install
docker-compose up -d --build 
cd ../DispatcherApp
docker-compose up -d --build


result=$( docker images -q consul )

if [[ -n "$result" ]]; then
  docker start devconsul1

else
  docker run -p 8500:8500 -d --name=devconsul1 consul

fi

