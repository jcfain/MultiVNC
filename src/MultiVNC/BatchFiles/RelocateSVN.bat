
set remoteUser=%~1
set "remotePw=%~2"

rem start /wait "TortoiseProc" "C:\Program Files\TortoiseSVN\bin\TortoiseProc.exe" "/command:relocate /path:"C:\QA.TestComplete\" /closeonend:1"
cd "C:\QA.TestComplete\"
svn switch --relocate http://svn-plano.medassets.com/svn/QA.TestComplete http://svn-ldc.medassets.com/svn/QA.TestComplete --username %remoteUser% --password %remotePw%

GOTO:EOF