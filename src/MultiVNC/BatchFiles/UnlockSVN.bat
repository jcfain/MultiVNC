@ECHO OFF

set remoteUser=%~1
set "remotePw=%~2"

@ECHO ON

rem start /wait "TortoiseProc" "C:\Program Files\TortoiseSVN\bin\TortoiseProc.exe" "/command:update /path:"C:\QA.TestComplete\RCT\Src\" /closeonend:1"
cd "C:\QA.TestComplete\RCT\Src\"
svn unlock --force --username %remoteUser% --password %remotePw%

GOTO:EOF