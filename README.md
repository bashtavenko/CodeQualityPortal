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

There is branch diff feature to compare metrics of two branches on a given date.


The metrics are loaded into warehouse database by two of my applications - [Code Metrics Loader](https://github.com/StanBPublic/CodeMetricsLoader) and
 [Code Churn Loader](https://github.com/StanBPublic/CodeChurnLoader). 

Database contains two separate fact tables for [metrics](https://raw.githubusercontent.com/StanBPublic/CodeMetricsLoader/screenshots/CodeMetricsWarehouse.png) and
[code churn](https://raw.githubusercontent.com/StanBPublic/CodeChurnLoader/screenshots/CodeChurnDB.png). Date dimension is shared by both fact tables.

##Technology stack
* EF Code First
* ASP.Net MVC
* Web API
* Angular JS
* Wijmo

There is demo data initializer and the site should run right out of the box as long as [SQL Server 2012 Express LocalDB]( https://msdn.microsoft.com/en-us/library/hh510202(v=sql.110).aspx)
is installed.