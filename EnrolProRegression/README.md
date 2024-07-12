# EnrollPro UI framework

## 1. Install .NET SDK:
- Ensure that the .NET SDK is installed on your machine. If not, you can download it from the [.NET download page](https://dotnet.microsoft.com/download).
- After installation, you can check your installation by running:
  ```bash
  dotnet --version
  ```
  in the terminal to see the installed version of the .NET SDK.

## 2. Install Chrome and ChromeDriver:
- Make sure you have the Chrome browser installed. You can download it from the [Chrome website](https://www.google.com/chrome/).
- Download ChromeDriver from the [ChromeDriver download page](https://sites.google.com/a/chromium.org/chromedriver/downloads), ensuring it matches your Chrome version. Follow the instructions to set it up.

## 3. Navigate to the Project Directory:
- Change to the project directory where your test project’s `.csproj` file is located:
  ```bash
  cd StudentEnrolmentRegression
  ```

## 4. Restore Dependencies:
- Restore the necessary NuGet packages using the following command:
  ```bash
  dotnet restore
  ```
- This will ensure all required packages for the regression tests are correctly installed.

## 5. Build the Test Project:
- Building the test project ensures there are no compilation errors before running the tests:
  ```bash
  dotnet build
  ```
- This step compiles the project and is necessary to proceed with running the tests.

## 6. Run the Tests:
- To execute the regression tests, use the `dotnet test` command. This command runs the tests defined in the project:
  ```bash
  dotnet test --verbosity normal
  ```
- The `--verbosity normal` option provides detailed output of the test execution, including the result of each test.