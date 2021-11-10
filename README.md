# exceptionless_sample

> 請先準備好 ServerUrl 與 ApiKey

## 安裝套件

Exceptionless.AspNetCore

Exceptionless.Extensions.Logging

## 新增 appSetting.json 設定

```json
{
  "Exceptionless": {
    "ApiKey": "{key}",
    "ServerUrl": "{url}"
  }
}
```

## Program.cs 設定

設定 Log 寫入 Exception, 請增加 `ConfigureLogging` 區段

```csharp
using Exceptionless;
```

```csharp             
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddExceptionless(c => c.SetDefaultMinLogLevel(LogLevel.Info));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
```

## Startup.cs 設定

```csharp
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 請放在第一行
            app.UseExceptionless();

            // 以下略
        }
```
