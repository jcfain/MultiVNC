@setlocal enableextensions enabledelayedexpansion

set FullTestName=%~1
set Assembly=%~2
set LoginId=%~3
set OpenSummary=%~4
set VerboseLogging=%~5
set SendEmail=%~6
set BrowserKeepOpen=%~7
set NUnitLocation=%8
rem set LoginFacs=%~6

%NUnitLocation% /run "%FullTestName%" "%Assembly%"
rem pause