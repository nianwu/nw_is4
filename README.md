- https://www.cnblogs.com/wangbin5542/p/12018921.html

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
2. 调整配置
  ```C#
  public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
  {
      new ApiScope("api1", "My API")
  };

  public static IEnumerable<Client> Clients => new List<Client>
  {
      new Client
      {
          ClientId = "client",

          // no interactive user, use the clientid/secret for authentication
          AllowedGrantTypes = GrantTypes.ClientCredentials,

          // secret for authentication
          ClientSecrets =
          {
              new Secret("secret".Sha256())
          },

          // scopes that client has access to
          AllowedScopes = { "api1" }
      }
  };
  ```
3. 添加api项目
4. 