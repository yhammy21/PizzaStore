# Pizza Store
Hello and welcome to a simple Pizza Store App API to manage pizzas and toppings.

# Get Started
Here I will explain what needs to be done in order to build, run, test, and use the Pizza Store App API in Visual Studio.

## Database Setup
First thing you need to do is to have a local database server installed, specifically a local SQL Server, that comes alongside with installing ***Microsoft SQL Server Management Studio 18***, since MSSMS 18 is the version of SQL Server Management Studio that was used to create and test the Pizza Store App API's database with, to learn more on how to install and setup SQL Server and ***Microsoft SQL Server Management Studio 18.***

Refer to this tutorial made by Microsoft below: 

https://learn.microsoft.com/en-us/ssms/install/install

### Database Connection String
Next is setting up the connection between the Pizza Store App API and the SQL Server installed locally on your machine.

#### SQL Server Services
First is to check is whether or not the SQL Server Service is running, to check this, go to the **Start Menu** and type in the search bar **SQL Server Configuration Manager**, then on the left side there would be a list, click **SQL Server Services** and service named ***SQL Server (SQLEXPRESS)*** will appear and its *state* should say ***running***, this is to ensure that SQL Server itself will work and respond properly when the Pizza Store App API tries to connect to it to do database operations.

#### appsettings.json
Next is to modify the ``appsettings.json`` file of the project and provide the local SQL Server name via the connection string section of the file.

The easy way to find your local SQL Server name would be by installing and opening ***Microsoft SQL Server Management Studio 18*** and when it prompts to connect to a server it would give you options as to what server you would like to to connect to. 

Use **Windows Authentication** and choose the first SQL Server name that it auto-fills on the ``Server name`` field, the SQL Server name of your local machine is usually denoted by the name of your ***local machine*** followed by the word ***SQLExpress***, so it the SQL Server name should look something like this ``LocalMachineName/SQLEXPRESS.``

Inside the ``appsettings.json`` file there will be a section named ***ConnectionStrings*** and under it is connection string named ***DefaultConnection*** and within it's string value there is section of it that says ``Server=<InsertLocalSqlServerNameHere>``, now replace the ``<InsertLocalSqlServerNameHere>`` part with your SQL Server name that you have found from ***Microsoft SQL Server Management Studio 18.***

### EF Core Setup
EF Core setup is really easy, once you have installed ***SQL Server***, ***Microsoft SQL Server Management Studio 18***, made sure the ***SQL Server Service*** is running, and have provided your ***SQL Server name*** inside the connection string of the ``appsettings.json`` file.

Inside **Visual Studio** go to **Build** tab and click **Build Solution**, this will build the entire solution and all of its projects within it, thus building the Pizza Store App API, this process is needed in order for the Pizza Store App API to install all of its ``NuGet Packages`` used by the project, mainly the EF Core packages for SQL Server and its corresponding tools needed by the application to run properly.

Now go to ***Tools>NuGet Package Manager>Manage NuGet Packages for Solution...*** and you should see the following packages installed:

1. Microsoft.EntityFrameworkCore.SqlServer 8.0.16
2. Microsoft.EntityFrameworkCore.Tools 8.0.16
3. Swashbuckle.AspNetCore 6.2.2

If these are the packages that you see within the ***Installed*** tab of the ``NuGet Package Manager`` window, then EF Core is installed and ready to go!

Lastly, go to ***Tools>NuGet Package Manager>Package Manager Console*** and it will prompt you to a console on the *bottom part* of **Visual Studio**, then simply type ``update-database`` and press **Enter**  in the ``Package Mananger Console``, this command will initialize the database and its needed tables to be used for the entire Pizza Store App API.

The **Package Manager Console** should say ``build succeeded..`` to then create the database and its corresponding tables and ``Done.`` when finally finished.

So, with that you have finally finished setting up and creating the Pizza Store App API's database using **Visual Studio** through the **Nuget Package Manager Console** using **EF Core**!

## How to Use
Now that the database is fully setup on your local machine, it is now time to run the Pizza Store App API and with that comes the explanation on how to run, use, and test the Pizza Store App API project, and of course, that includes the actual features and functionalities of the API itself, this goes both for the ***pizzas and toppings*** parts of the API, which are the only and main purpose of this Pizza Store App API.

### Run the The Pizza Store App API
To run the Pizza Store App API, once you have the Pizza Store App API project open in **Visual Studio**, go to the **top part of Visual Studio,**, and you should see a ***green play button***, and the string of text next to it should say ***https***, if not then click the drop down icon next to the ***green play button*** and a list should appear and select ***https***, as this was the launch settings profile used when the Pizza Store App API was being developed, and to ensure everything goes and works smoothly as much as possible.   







