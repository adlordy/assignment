FROM microsoft/dotnet
WORKDIR /dotnetapp

COPY . . 
RUN dotnet restore

CMD dotnet test -c Release test/adlordy.Assignment.IntegrationTests/project.json  -json -nologo