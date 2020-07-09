@ECHO off


set rootDir=C:\RCT\Automation\CentralApps
set projDir=%rootDir%\CarePricer\TestComplete\ReleaseNightTests\
cd %rootDir%
git checkout HEAD -- %projDir%
git pull
rem pause
