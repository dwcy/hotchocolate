# Introduction 
Example project to show of HotChocolate GraphQL with a proper real life implementation with all the common basics

Project Board [Here](https://)

# Arcitecture & Patterns
Clean-Architecture [Read more!](https://www.aalpha.net/blog/clean-architecture-design-pattern-for-modern-application-development/)
Repository Pattern
Adapter Pattern
DDD
Entity framework code first

# Code rules
No dependencies in Domain layer
All dependencies in Infrastructure layer ecxept test
Style Rules: Warnings set as Errors to prevent misstakes early on

# Development Configurations
Code rules in sln root -> .editorconfig
App debug URL -> LaunchSettings.json in your host project
CI/CD Pipeline in sln root -> azure-pipelines folder

# Get Started!
1. Build & run!

# When Running app
[GraphQL API](https://localhost:5000/graphql) 
[GraphQL Voyager](https://localhost:5000/voyager) 
#[Web API](https://localhost:5001/api) 
#[Swagger API](https://localhost:5001/swagger) 

# Build and Test
Apply Integration tests and unit tests

#Infrastructure
Build Pipeline [Here](https://) 
Release Pipeline [Here](https://) 
App Service [Here](https://) 

# Deplyment 
- [TEST](https://somewhere)
- [UAT](https://somewhere)
- [PROD](https://somewhere)


# Dependencies
.NET 7.x
Entity Framework
HotChocolate v13

- [.NET 7.x](https://github.com/aspnet/Home)
- [Entity Framework Core](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)
- [HotChocolate](https://chillicream.com/docs/hotchocolate/v13)