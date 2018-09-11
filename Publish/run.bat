@echo OFF
echo "Bot Launching"

cd .\Bot
cd .\Bin
start dotnet.exe BotDiscord.dll
echo "Bot Closed"
PAUSE