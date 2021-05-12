md ImageProcess
cd ./ImageProcess
dotnet new sln
dotnet new classlib -o ImageProcess
dotnet sln add ./ImageProcess/ImageProcess.csproj
dotnet new wpf -o ImageProcessWidget
dotnet sln add ./ImageProcessWidget/ImageProcessWidget.csproj
dotnet new xunit -o ImageProcess.Tests
dotnet sln add ./ImageProcess.Tests/ImageProcess.Tests.csproj
dotnet add ./ImageProcess.Tests/ImageProcess.Tests.csproj reference ./ImageProcess/ImageProcess.csproj
cd ImageProcess/ImageProcess/
dotnet add package System.Drawing.Common
@REM dotnet build
@REM dotnet test