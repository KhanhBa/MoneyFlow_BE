# Sử dụng image .NET SDK để build project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Đặt thư mục làm việc
WORKDIR /src

# Sao chép từng project để thực hiện restore riêng
COPY ["MoneyFlow.API/MoneyFlow.APIs.csproj", "MoneyFlow.API/"]
COPY ["ErrorHanding/ErrorHanding.csproj", "ErrorHanding/"]
COPY ["MoneyFlow.Payload/MoneyFlow.Payload.csproj", "MoneyFlow.Payload/"]
COPY ["MoneyFlow.Service/MoneyFlow.Services.csproj", "MoneyFlow.Service/"]
COPY ["MoneyFlow.Repository/MoneyFlow.Repositories.csproj", "MoneyFlow.Repository/"]

# Restore các packages
RUN dotnet restore "MoneyFlow.API/MoneyFlow.APIs.csproj"

# Copy toàn bộ source vào
COPY . .

# Đặt thư mục làm việc là API
WORKDIR /src/MoneyFlow.API

# Build ứng dụng ở chế độ release
RUN dotnet build "MoneyFlow.APIs.csproj" -c Release -o /app/build

# Publish ứng dụng
RUN dotnet publish "MoneyFlow.APIs.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage để chạy ứng dụng từ image nhẹ hơn
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "MoneyFlow.APIs.dll"]
