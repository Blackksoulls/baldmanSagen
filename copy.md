

robocopy.exe $(SolutionDir)GameManager\bin\Debug\netcoreapp2.1\  $(SolutionDir)\Publish\Bots\GameManager\Bin /E /IS
robocopy.exe $(SolutionDir)GameManager\Config\  $(SolutionDir)\Publish\Bots\GameManager\Config /E /IS
robocopy.exe $(SolutionDir)GameManager\Locale\  $(SolutionDir)\Publish\Bots\GameManager\Locale /E /IS
robocopy.exe $(SolutionDir)Libraries  $(SolutionDir)\Publish\Bots\GameManager\Bin /E /IS

robocopy.exe $(SolutionDir)BotManager\bin\Debug\netcoreapp2.1\  $(SolutionDir)\Publish\Bots\BotManager\Bin /E /IS

robocopy.exe $(SolutionDir)EventManager\bin\Debug\netcoreapp2.1\  $(SolutionDir)\Publish\Bots\EventManager\Bin /E /IS
robocopy.exe $(SolutionDir)EventManager\Config\  $(SolutionDir)\Publish\Bots\EventManager\Config /E /IS
robocopy.exe $(SolutionDir)Libraries\  $(SolutionDir)\Publish\Bots\EventManager\Bin /E /IS

python $(SolutionDir)deploy.py