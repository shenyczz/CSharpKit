
REM Usage: dotnet nuget push [arguments] [options]

REM Arguments:
REM   [root]  指定包路径和 API 密钥，将包推送到服务器。

REM Options:
REM   -h|--help                      Show help information
REM   --force-english-output         使用不变的基于英语的区域性强制应用程序运行。
REM   -s|--source <source>           要使用的包源(URL、UNC/文件夹路径或包源名称)。如果在 NuGet.Config 中指定，则默认为 DefaultPushSource。
REM   -ss|--symbol-source <source>   要使用的符号服务器 URL。
REM   -t|--timeout <timeout>         推送到服务器的超时值(以秒为单位)。默认为 300 秒(5 分钟)。
REM   -k|--api-key <apiKey>          服务器的 API 密钥。
REM   -sk|--symbol-api-key <apiKey>  符号服务器的 API 密钥。
REM   -d|--disable-buffering         推送到 HTTP(S) 服务器时禁用缓存可减少内存使用。
REM   -n|--no-symbols                如果存在符号包，系统不会将该符号包推送到符号服务器。
REM   --no-service-endpoint          请勿将 "api/v2/package" 追加到源 URL。
REM   --interactive                  对于身份验证等操作，允许命令阻止并要求手动操作。
REM   --skip-duplicate               如果包和版本已存在，则跳过它并继续推送中的下一个包(若有)。


REM Example:
echo on
set SOURCE=https://api.nuget.org/v3/index.json
set API_KEY=you api-key

set PACK_DIR=packages
set CSK_SUBPATH=csk
set CSK_VERSION=3.1.1

set CSK_NUPKGS=%PACK_DIR%\%CSK_SUBPATH%\%CSK_VERSION%\CSharpKit.*.nupkg

dotnet nuget push %CSK_NUPKGS% --skip-duplicate --api-key %API_KEY% --source %SOURCE%

