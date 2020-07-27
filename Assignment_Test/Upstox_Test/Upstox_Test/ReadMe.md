#Packages Installed List

    <PackageReference Include="Microsoft.Extensions.Configuration" Version="3.1.6" />
    <PackageReference Include="Microsoft.Extensions.Configuration.FileExtensions" Version="3.1.6" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="3.1.6" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="3.1.6" />
    <PackageReference Include="Microsoft.Extensions.Logging" Version="3.1.6" />
    <PackageReference Include="Microsoft.Extensions.Logging.Console" Version="3.1.6" />
    <PackageReference Include="Serilog" Version="2.9.0" />
    <PackageReference Include="Serilog.Extensions.Logging" Version="3.0.1" />
    <PackageReference Include="Serilog.Settings.AppSettings" Version="2.2.2" />
    <PackageReference Include="Serilog.Settings.Configuration" Version="3.1.0" />
    <PackageReference Include="Serilog.Sinks.Console" Version="3.1.1" />
    <PackageReference Include="Serilog.Sinks.File" Version="4.1.0" />
    <PackageReference Include="Serilog.Sinks.Literate" Version="3.0.0" />
    <PackageReference Include="Serilog.Sinks.RollingFile" Version="3.3.0" />
    <PackageReference Include="Topshelf" Version="4.2.1" />
 

  #Packages Installed List Ended

  #Steps
  1. Created the Console application Upstox_Test in .Net Core 2.1
  2. Created the dll for Upstox_Service in .Net Standard 2.0
  3. For Logging  the files installed "Microsoft.Extensions.Logging and Serilog
  4. Created Helper/ServiceHelper in which inject the interfaces 
  5. Created Monitor/ProcessBarChatMonitor class in which we can monitor "Bar chart" Logs as per requirements
  6.Created the Method "GetDataFromJsonFile" in ProcessBarChatMonitor class which method contains to fetch the Json Data from folder SampleJson/applicationstructure.json.
    Method the Response from this method in one glabl variable
  7.Using this "FSM" method we can increase the "Bar Number" after every 15 Seconds and Update the logs file
  8.Created this "LogsOHLC" to display the log for OHLC data
  #Steps End here

  #Exception handling
  1. In this project "Upstox_Test" maintianing the Log which is located in "c\\logs\\" and file name is "Test_Assignment{Datewise}.log"
  2. In every method keep the "try and catch" exceptions which will write in log file "Test_Assignment{Datewise}.log".
  #Exception Handling

    
