### **Development**

### Sonar Qube

```
https://download.oracle.com/java/20/latest/jdk-20_windows-x64_bin.msi
dotnet tool install --global dotnet-sonarscanner
dotnet tool install --global dotnet-reportgenerator-globaltool
docker-compose up -d 

dotnet sonarscanner begin /k:"estudos" /n:"estudos" /d:sonar.login="admin" /d:sonar.password="admin" /d:sonar.dotnet.excludeTestProjects=true /d:sonar.coverageReportPaths=".\sonarqubecoverage\SonarQube.xml"
dotnet build
dotnet test --no-build --collect:"XPlat Code Coverage"
reportgenerator "-reports:*\TestResults\*\coverage.cobertura.xml" "-targetdir:sonarqubecoverage" "-reporttypes:SonarQube"
dotnet sonarscanner end /d:sonar.login="admin" /d:sonar.password="admin"
```