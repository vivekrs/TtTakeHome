TtTakeHome
==========

Post interview followup sample project to 'show off' c# skills

Getting Started
---------------

1. Clone repository
2. Use the existing database in Tt.Framework.Data (mdf and ldf) or, preferably, use a Sql Server Instance
3. Open Tt.Framework/DiServiceResources.cs and Update the configuration to reflect the database connection string and the local path (this directory must already exist) that will contain the saved transaction files. Note: I put these settings in a static class because I did not want to replicate the information in Web.config (for the Rest project) and in app.config (for the Service project). Ideally, these configuration values will be in a resource file or other editable file which will not need a re-compile.
4. Build the solution (it should automatically download all the missing nuget packages).
5. Tt.Service can be installed as a Windows Service or run as a Console Application. The program will detect the environment automatically. I did this because it will be easier to debug. Go to the Debug folder and run Tt.Service.exe.
6. Run the Rest WebApi application. Go to localhost:port/index.html. This will help build the POST query to submit the files.
