# EnrollPro API
## Development Server for .NET API

### 1. Install .NET SDK:
- Before you can work with any .NET project, you need the .NET SDK (Software Development Kit) installed on your machine.
- You can download the .NET SDK from the official [.NET download page](https://dotnet.microsoft.com/download).
- After downloading, follow the installation instructions for your operating system.
- To verify that the .NET SDK has been installed correctly, open a terminal and run:
  ```bash
  dotnet --version
  ```
  This command should display the installed .NET version. For this project, it should be 8.0 or higher

### 2. Navigate to the Project Directory:
- Navigate to the folder containing your .NET project, where the `.csproj` file is located. This file contains the project configuration and dependency information.
- Open a terminal and change directories to this location using:
  ```bash
  cd API/EnrollPro
  ```

### 3. Restore Dependencies:
- .NET projects use NuGet packages as dependencies, which need to be restored before you can build or run your project.
- Run the following command in the terminal to restore these dependencies:
  ```bash
  dotnet restore
  ```
- This command reads the `.csproj` file and downloads the required packages to the `packages` folder in your project directory.

### 4. Build the Project:
- Once all dependencies are restored, the next step is to build your project to check for any compile-time errors and to prepare it for execution.
- Build your project using:
  ```bash
  dotnet build
  ```
- This command compiles the project and outputs any build errors. Fix any errors before proceeding.

### 5. Run the API:
- After building the project successfully, you can run your .NET API using:
  ```bash
  dotnet run
  ```
- This command will start the API server, typically listening on `http://localhost:5000` or `https://localhost:5001`. You can access your API endpoints via these URLs using a browser or tools like Postman.
- To check the list of endpoints you can run the API and head over to `http://localhost:5000/swagger` or `https://localhost:5001/swagger`