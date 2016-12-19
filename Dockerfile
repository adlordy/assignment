FROM microsoft/windowsservercore

RUN mkdir "C:\Setup"

COPY ./adlordy.Assignment/bin/Release C:/Setup
WORKDIR "C:\Setup"

CMD ".\adlordy.Assignment.exe"

EXPOSE 8081




