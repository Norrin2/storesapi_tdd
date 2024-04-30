# Stores App Demo.
This is a Stores CRUD app.
The project uses a simple MVC approach, there is no Service Layer, Clean Architecture, DDD or any other more complex architecture as with how simple the problem is this would simply be over-engineering.
Always feel free to refer to other .NET projects on my github for more complex samples.
## Back-end
- .NET 8
- The back-end provides an API that exposes CRUD operations over the Stores. The stores are stored in memory and for that purpose a StoresRepository is used, following the RepositoryPattern which brings the demo a little closer to what a real application would be. The repository is injected as a singleton to ensure it stays alive while the application is running.
- Stores also need a company Id to reflect a multi company scenario.
- There is a test project which covers use cases over the controller. It uses the xUnit nuget package which is one of the standard options for testing in .NET.

## Running the Project

To run the Stores project, follow these steps:

1. Open the solution in Visual Studio 2022 or later.
2. Set the Stores project as the startup project.
3. Build the solution.
4. Press F5 or use the Debug -> Start Debugging menu to run the project.

Alternatively, you can use the .NET CLI to run the project:

    cd StoresApi/StoresApi
    dotnet run

## Running the Tests

To run the unit tests in the Stores Test project, follow these steps:

1. Open the solution in Visual Studio 2022 or later.
2. Set the Test Explorer window as the active window.
3. Build the solution.
4. In the Test Explorer window, select the tests you want to run.
5. Press the Run button or use the Test -> Run menu to run the selected tests.

Alternatively, you can use the .NET CLI to run the tests:

    cd StoresApi/StoresApi.Tests
    dotnet test
