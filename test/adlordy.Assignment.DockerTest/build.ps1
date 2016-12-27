npm install
.\node_modules\.bin\bower install
.\node_modules\.bin\gulp
tsc

Remove-Item .out -Recurse -Force -ErrorAction:Ignore
dotnet publish -c Release -o .out

docker-compose build