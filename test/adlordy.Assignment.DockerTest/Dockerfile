FROM microsoft/dotnet:runtime

ENV ASPNETCORE_URLS=http://*:5005

WORKDIR /dotnetapp
COPY .out .

CMD dotnet adlordy.Assignment.DockerTest.dll

EXPOSE 5005