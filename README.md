# Sample ASP.NET Core Integration Tests DI

Demonstrates how to use the default ASP.NET Core DI Container's features inside ConfigureTestServices during integration testing.

## Solutions

There are various options, some of which are branches in the repository:

- Commit 32da0a7 (see [GitHub comparison](https://github.com/jeroenheijmans/sample-asp-net-core-integration-tests-di/compare/workaround-with-extra-di-registration)) with a workaround by registering `BarService` separately in `Startup`
- Commit 6ab01ec (see [GitHub comparison](https://github.com/jeroenheijmans/sample-asp-net-core-integration-tests-di/compare/fix-with-nuget-package-scrutor)) that uses [the Scrutor NuGet package](https://github.com/khellang/Scrutor) to add decorator support to the default DI container
- Commit 74131c9 (see [GitHub comparison](https://github.com/jeroenheijmans/sample-asp-net-core-integration-tests-di/compare/fix-with-custom-decorator-logic)) that adapts a tiny part of abovementioned NuGet package to add only the needed Decorator features to our own codebase
- The [answer by user @ChrisPratt on Stack Overflow](https://stackoverflow.com/a/55600576/419956) goes into detail about what the underlying problem is, and offers a solution with inheriting from `Startup` with a new `Startup` and `override`ing the appropriate service registrations
