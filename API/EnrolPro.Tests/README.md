## EnrollPro Unit Tests Project
### 1. Install .NET SDK:
- Ensure that the .NET SDK is installed on your machine. If not, you can download it from the [.NET download page](https://dotnet.microsoft.com/download).
- After installation, you can check your installation by running:
  ```bash
  dotnet --version
  ```
  in the terminal to see the installed version of the .NET SDK.

### 2. Navigate to the Project Directory:
- Change to the project directory using:
  ```bash
  cd API/EnrolPro.Tests
  ```

### 3. Restore Dependencies:
- Just like with the API, unit tests have dependencies that need to be restored using the following command:
  ```bash
  dotnet restore
  ```
- This will ensure all NuGet packages needed for the tests are correctly installed.

### 4. Build the Test Project:
- Building the test project is important to ensure there are no compilation errors before running the tests:
  ```bash
  dotnet build
  ```
- This step compiles the project and is necessary to proceed with running the tests.

### 5. Run the Tests:
- To execute the unit tests, use the `dotnet test` command. This command runs the tests defined in the project:
  ```bash
  dotnet test --verbosity normal
  ```
- The `--verbosity normal` option provides a detailed output of the tests' execution, which includes a line for each test executed along with the pass or fail outcome.