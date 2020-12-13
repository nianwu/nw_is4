1. 依次运行如下命令创建项目
  ```sh
  dotnet new -i IdentityServer4.Templates
  md quickstart
  cd quickstart
  md src
  cd src
  dotnet new is4empty -n IdentityServer
  cd ..
  dotnet new sln -n Quickstart
  dotnet sln add ./src/IdentityServer/IdentityServer.csproj
  ```
