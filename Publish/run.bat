@echo OFF
echo "Bot Launching"

cd .\Bot
cd .\Bin
dotnet.exe BotDiscord.dll
echo "Bot Closed"
PAUSE