

REM deletes
rmdir /s /q Publish\Bots\GameManager\Bin
rmdir /s /q Publish\Bots\BotManager\Bin
rmdir /s /q Publish\Bots\EventManager\Bin

rmdir /s /q Publish\Bots\GameManager\Config
rmdir /s /q Publish\Bots\BotManager\Config
rmdir /s /q Publish\Bots\EventManager\Config

rmdir /s /q Publish\Bots\GameManager\Locale


taskkill.exe /F /IM dotnet.exe


REM langues
robocopy.exe GameManager\Locale\   Publish\Bots\GameManager\Locale /E /IS


REM bin
robocopy.exe GameManager\bin\Debug\netcoreapp2.1\   Publish\Bots\GameManager\Bin /E /IS
robocopy.exe BotManager\bin\Debug\netcoreapp2.1\   Publish\Bots\BotManager\Bin /E /IS
robocopy.exe EventManager\bin\Debug\netcoreapp2.1\  Publish\Bots\EventManager\Bin /E /IS

REM config 
robocopy.exe GameManager\Config\   Publish\Bots\GameManager\Config /E /IS
robocopy.exe BotManager\Config\   Publish\Bots\BotManager\Config /E /IS
robocopy.exe EventManager\Config\  Publish\Bots\EventManager\Config /E /IS

REM lib
robocopy.exe Libraries\  Publish\Bots\EventManager\Bin /E /IS
robocopy.exe Libraries\  Publish\Bots\BotManager\Bin /E /IS
robocopy.exe Libraries\   Publish\Bots\GameManager\Bin /E /IS



start Publish\run.bat