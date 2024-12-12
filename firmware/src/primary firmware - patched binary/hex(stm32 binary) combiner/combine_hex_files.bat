:: Usage: combine_hex_files source1.hex source2.hex combined.hex
 
@ECHO OFF
 
IF "%1"=="" GOTO BAD_ARGUMENTS
IF "%2"=="" GOTO BAD_ARGUMENTS
IF "%3"=="" GOTO BAD_ARGUMENTS
 
FINDSTR /R /V "^:00000001FF" "%1" > "%3"
TYPE "%2" >> "%3"
GOTO COMPLETE
 
:BAD_ARGUMENTS
ECHO ERROR: incorrect number of arguments
ECHO.
ECHO Usage:
ECHO   combine_hex_files.bat source1.hex source2.hex combined.hex
 
:COMPLETE