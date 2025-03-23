
# 使用官方的 .NET SDK 映像檔來構建應用程式
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# 設定工作目錄
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 複製 csproj 檔案並還原依賴項
COPY *.csproj ./
RUN dotnet restore

# 複製其餘的應用程式檔案
COPY . ./

# 建構應用程式
RUN dotnet publish -c Release -o out

# 使用官方的 .NET 6.0 執行環境來運行應用程式
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# 設定工作目錄
WORKDIR /app

# 複製從 build 階段構建的應用程式檔案
COPY --from=build /app/out .

# 開放 80 端口，供容器內部的應用程式訪問
EXPOSE 80

# 設定容器啟動時執行的命令
ENTRYPOINT ["dotnet", "WebApi.dll"]