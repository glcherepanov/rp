@echo off

cd BackendApi
start /b dotnet BackendApi.dll --configuration Release
cd ../TaskCreator
start /b dotnet TaskCreator.dll --configuration Release
