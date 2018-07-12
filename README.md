# demo-azure-active-directory-wsfederation

## WebApp-WSFederation-DotNet.sln

Part of this repository, namely [WebApp-WSFederation-DotNet.sln](https://github.com/hris-front-end-team/demo-azure-active-directory-wsfederation/blob/master/ORIGINAL_MICROSOFT_README.md) solution, originates from Microsoft Documentation maintainers.
This project was used to determine the basic configuration necessary to enable WsFederation (via Azure ADFS).
Things like `Wtrealm`, `MetadataAddress`, etc.

## ApiWithAdfsAuth.sln

`ApiWithAdfsAuth` solution implements a POC that illustrates several **.NET Core WebApi** authentication variants that are based on:

* **Cookies** (`Microsoft.AspNetCore.Authentication.Cookies` namespace & [NuGet](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.Cookies/)); and
* **WsFederation** (`Microsoft.AspNetCore.Authentication.WsFederation` namespace & [NuGet](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.WsFederation/)).

### Version

The solution is tested against .NET Core 2 (SDK/CLI).

```sh
$ dotnet --version
2.1.302
```

| NuGet Package                                    | Version |
| ------------------------------------------------ | ------- |
| Microsoft.AspNet.WebApi.Core                     | 5.2.6   |
| Microsoft.AspNetCore                             | 2.1.2   |
| Microsoft.AspNetCore.Authentication.Cookies      | 2.1.1   |
| Microsoft.AspNetCore.Authentication.WsFederation | 2.1.1   |
| Microsoft.AspNetCore.Mvc                         | 2.1.1   |
| Microsoft.Extensions.DependencyInjection         | 2.1.1   |

### Prerequisites

The following resources are strongly recommended for looking at if you're new to the subject:

* [MSDN: Understanding WS-Federation (overview)](https://msdn.microsoft.com/en-us/library/bb498017.aspx?f=255&MSPPError=-2147217396)
* [MDN: HTTP cookies (overview)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Cookies)
* [.NET Core Docs: Authenticate users with WS-Federation in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/ws-federation?view=aspnetcore-2.1)
* [.NET Core Docs: Use cookie authentication without ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-2.1&tabs=aspnetcore2x)
* [StackOverflow: WsFederation & token sliding expiration](https://stackoverflow.com/a/28631956/482868)
