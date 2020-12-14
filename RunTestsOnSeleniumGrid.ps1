Start-Job -ScriptBlock { docker-compose -f ./docker-compose-selenium-grid.yml up }
dotnet test
docker-compose -f ./docker-compose-selenium-grid.yml down
