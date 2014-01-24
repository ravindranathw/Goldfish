# Goldfish Core

These are the core projects for Goldfish that provides the basic functionality.

## Goldfish

This is the main project of the framework which contains:

* The main application framework
* Data **entities** & **models**
* Core **Api** & **Repositories**

This project should be kept as clean an minimal as possible. New functions and data entities should be added as modules to ensure the modularity of the framework.

When adding **new methods** to existing repositories or **new functionality**, make sure you **add tests** to the Goldfish.Test repository. 

## Goldfish.Test

Integration tests for the core library. The integration tests assume that you have **LocalDb** installed in your development environment. If you don't, make sure you change the **ConnectionString** before running the tests.