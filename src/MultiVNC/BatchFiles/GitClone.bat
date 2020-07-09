ECHO off

set user=%~1
SETLOCAL ENABLEDELAYEDEXPANSION
set pass=%~2

rem example user: jfain

set rootDir=C:\RCT\Automation

mkdir %rootDir%
cd %rootDir%
rem git clone http://%user%:!pass!@tfs.medassets.com:8080/tfs/RCT/Automation/_git/Tools
rem git clone http://%user%:!pass!@tfs.medassets.com:8080/tfs/RCT/Automation/_git/CentralApps
git clone http://tfs.medassets.com:8080/tfs/RCT/Automation/_git/Tools
git clone http://tfs.medassets.com:8080/tfs/RCT/Automation/_git/CentralApps
pause
