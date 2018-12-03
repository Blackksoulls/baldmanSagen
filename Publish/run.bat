@echo OFF
echo "Bot Launching"

cd .\Bots
cd .\BotManager
cd .\Bin
dotnet.exe BotManager.dll
echo "Bot Closed"

PAUSE
