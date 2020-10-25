@echo off
set VERSION=%~1

cd ..

cd BackendApi
dotnet publish --configuration Release --output "../product-%VERSION%\BackendApi"
cd ../TaskCreator
dotnet publish --configuration Release --output "../product-%VERSION%\TaskCreator"

mkdir "../product-%VERSION%\config"
mkdir "../product-%VERSION%\Protos"

cd ..
copy "Protos" "product-%VERSION%\Protos"
copy "scripts\start.cmd" "product-%VERSION%\"
copy "scripts\stop.cmd" "product-%VERSION%\"
copy "config" "product-%VERSION%\config"