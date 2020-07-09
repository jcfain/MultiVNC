
set remoteUser=%~1
set "remotePw=%~2"

rem start /wait "TortoiseProc" "C:\Program Files\TortoiseSVN\bin\TortoiseProc.exe" "/command:revert -R /path:"C:\Dev\Medassets\QA.TestComplete\RCT\Src" /closeonend:1"

cd "C:\QA.TestComplete\RCT\Src\"
svn revert -R . --username %remoteUser% --password %remotePw%

GOTO:EOF