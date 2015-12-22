#Code Quality Portal

This web site is part of code quality solution to visualize the following metrics:

* Maintainablity index
* Cyclomatic complexity
* Lines of code
* Depth of inheritance
* Class coupling
* Code coverage
* Code churn

Data is aggregated and can be drilled down through this hierarchy:

* Source control branch
* System 
* Module (assembly)
* Namespace
* Type (class)
* Member


Here is the overall picture of the solution components:

![CodeQ](/../screenshots/CodeQ.png?raw=true "CodeQ")

1. Assembly that is being instrumented
2. Microsoft Metrics [Power Tool](https://www.microsoft.com/en-us/download/details.aspx?id=48213)
3. Xml file that is generated by the Power Tool
4. Assembly with unit tests
5. Nunit console runner
6. [OpenCover](https://github.com/OpenCover/opencover) code coverage tool which runs Nunit
7. Xml file generated by OpenCover
8. [Report Generator](https://github.com/danielpalme/ReportGenerator) parses OpenCover-generated file
9. Summary xml produced by the Report Generator
10. [Code Metrics Loader](https://github.com/StanBPublic/CodeMetricsLoader) utility that I wrote to merge metrics produced by the Power Tool with code coverage.
11. [Code Churn Loader](https://github.com/StanBPublic/CodeChurnLoader) another of mine utilites to collect code churn from Github or Bitbucket hosted repos.
12. Database contains two separate fact tables for [metrics](https://raw.githubusercontent.com/StanBPublic/CodeMetricsLoader/screenshots/CodeMetricsWarehouse.png) and [code churn](https://raw.githubusercontent.com/StanBPublic/CodeChurnLoader/screenshots/CodeChurnDB.png). Date dimension is shared by both fact tables.
13. This repo


##Technology stack
* EF Code First
* ASP.Net MVC
* Web API
* Angular JS
* Wijmo

There is demo data initializer and the site should run right out of the box as long as [SQL Server 2012 Express LocalDB]( https://msdn.microsoft.com/en-us/library/hh510202(v=sql.110).aspx)
is installed.

There is also branch diff feature to compare metrics of two branches on a given date.