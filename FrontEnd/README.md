# EnrollPro
This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 14.0.2.


## Development Server

### Before Running `ng serve`

Before you can successfully run `ng serve`, there are a couple of prerequisites and troubleshooting steps to consider:

1. **Check for `node_modules` Directory:**
   - The `node_modules` directory contains all the project dependencies as defined in the `package.json` file. These dependencies need to be installed for your Angular application to run.
   - If the `node_modules` directory is missing, you need to install these dependencies by running:
     ```bash
     npm install
     ```
   - This command reads the `package.json` file and installs all the required packages into the `node_modules` directory.

2. **Ensure npm is Installed:**
   - `npm` (Node Package Manager) is required to install the dependencies. If `npm` is not installed, the `npm install` command will fail.
   - To install `npm`, you first need to install Node.js, as `npm` comes bundled with it. You can download it from [Node.js official website](https://nodejs.org/).
   - After installation, you can verify that `npm` is installed by running:
     ```bash
     npm --version
     ```

3. **Check Angular CLI Installation:**
   - The `ng serve` command is part of the Angular CLI (Command Line Interface), which must be installed globally or in your project to use Angular-specific commands.
   - If running `ng serve` results in a command not found error, it likely means Angular CLI is not installed. To install Angular CLI globally, run:
     ```bash
     npm install -g @angular/cli
     ```
   - Once installed, you should be able to run `ng serve` without issues.

### Running `ng serve`

Once you have `node_modules` installed and Angular CLI available, you can start your development server:
- Execute:
  ```bash
  ng serve
  ```
- Open a browser and navigate to `http://localhost:4200/` to view your application.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
