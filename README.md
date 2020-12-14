# DotNetFiddle web-app tests

Following 2 basic tests on https://dotnetfiddle.net/ :

a- Run default program and check output:

1. Click “Run” button
   
2. Check Output window (“Hello World” text is expected)

b- (As my name begins with "A") Select NUnit 3.12.0 package and check it is added:
1. Select NuGet Packages: NUnit (3.12.0)

2. Check that NUnit package is added

## Tests framework architecture

1. Test scenarios definitions: Defined in "Specification by example" style, in SpecFlow feature
file `tests/features/DotNetFiddleSpecs.feature`. 
2. Test scenarios implementations: Bindings/step-definitions for the feature file are
implemented in `tests/steps/DotNetFiddleSteps.cs` (Setup and Teardown for these steps
 is implemented in `tests/steps/Hooks.cs`). The step-definitions make use of page-objects
 found in `tests/PageObjects`. 
3. Assertions: Tests use NUnit library for assertions.
4. Configuration: Configuration settings are defined in `tests/config/Config.json` file.
5. Infrastructure: The default configuration is to use selenium-grid, which is setup
via `docker-compose`, as defined in `docker-compose-selenium-grid.yml`, which defines
services for Chrome, Firefox and Opera browsers; with video recording enabled.
6. Tests-runner: Powershell script `RunTestsOnSeleniumGrid.ps1` is used to start selenium 
grid, run the tests via `dotnet test` and then bring down the grid.
7. Verification and troubleshooting: At end of each test-run (not just failures, on successful
runs also), screenshots (titled with scenario name) are placed in `screenshots` folder, 
and videos (if using selenium grid) placed in `videos` folder. Contents of both folders
are defined in `.gitignore` though, so are not version-controlled.

## Run tests locally

Tests can be run in following 2 ways:

a- Run on selenium-grid, via docker-compose, in Cli/Powershell (requires `dotnet` driver to be installed).
1. Clone repository locally.
2. Choose config: In `tests/config/Config.json`, specify `Browser` (Chrome, Firefox or Opera only) 
and MaxWait (maximum wait time threshold, in seconds, for WebDriver to wait for expected conditions
in tests).
2. In project root, run: `./RunTestsOnSeleniumGrid.ps1`

b- Run without selenium-grid, and on an IDE:
1. Clone repository locally.
2. In `tests/config/Config.json`, set `"SeleniumGrid": false` 
2. Open the project in a C# IDE such as `Visual Studio` or `Rider` (I have only used 
these 2, things maybe different in other IDEs) and select/click on "Run all Unit tests" (or similar) 
option. This _should_ clean and build the solution, find the 2 SpecFlow tests and run them on Chrome browser 
in head-full mode (p.s. when running without grid, these tests are setup to only run on Chrome, and expect a Chrome browser to be installed on the machine). In case 
no tests are found and run, then check if your IDE requires NUnit adapter to be manually installed.

### Todos/room-for-improvements
It would be good if tests could run in parallel, on all the browsers in the selenium-grid. At 
the moment, they only run on one browser (defined in config) from the grid.
