@echo off

cd BackendApi
start /b dotnet BackendApi.dll
cd ../TaskCreator
start /b dotnet TaskCreator.dll
cd ../JobLogger 
start dotnet JobLogger.dll
