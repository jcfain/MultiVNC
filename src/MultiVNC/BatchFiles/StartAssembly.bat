@setlocal enableextensions enabledelayedexpansion

set Assembly=%~1
set LoginId=%~2
set OpenSummary=%~3
set VerboseLogging=%~4
set SendEmail=%~5
set BrowserKeepOpen=%~6
set NUnitLocation=%7
rem set LoginFacs=%~6

%NUnitLocation% %Assembly%
rem pause