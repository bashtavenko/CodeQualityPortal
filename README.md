#Code Quality Portal

This is web site to visualize the following code quality metrics over given time:

* Maintainablity index
* Cyclcomatic complexity
* Lines of code
* Depth of inheritance
* Class coupling
* Code coverage
* Code churn

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

There is demo data initializer and the site should run right out of the box.