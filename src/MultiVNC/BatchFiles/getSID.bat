@ECHO OFF

rem share dir required

FOR /F "usebackq" %%i IN (`hostname`) DO SET HOSTNAME=%%i
SET filePath=\\crp40ppfs07.medassets.com\XMD21PPSHARE01\J\TCfolders\Automation\LabStatus\SID\%HOSTNAME%_SID.txt
for /f "tokens=4 delims= " %%G in ('tasklist /FI "IMAGENAME eq tasklist.exe" /NH') do SET RDP_SESSION=%%G
echo %RDP_SESSION% > %filePath%