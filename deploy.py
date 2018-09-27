import os 

os.system("robocopy.exe bin\\Release\\netcoreapp2.1\\ ..\\Publish\\Bot\\Bin /E /IS")
os.system("robocopy.exe config\\ ..\\Publish\\Bot\\Config /E /IS")
os.system("robocopy.exe Libraries\\ ..\\Publish\\Bot\\Bin /E /IS")
os.system("robocopy.exe Locale\\ ..\\Publish\\Bot\\Locale\\ /E /IS /xf *.cs")
