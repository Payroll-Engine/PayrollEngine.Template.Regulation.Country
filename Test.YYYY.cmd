@echo off
setlocal

:: ============================================================
:: Test.YYYY.cmd -- {CC}.{RegulationName} (Windows)
::
:: Executes all tests in order:
::   1. Unit tests    (dotnet test -- no PE backend required)
::      Remove this block if the regulation has no unit tests.
::   2. Integration   (.pecmd via registered file association)
::
:: Prerequisites:
::   - .NET SDK on PATH
::   - .pecmd extension registered (Register-PecmdExtension.ps1)
::   - PE backend running (for integration tests)
:: ============================================================

set UNIT_PROJECT=YYYY\Tests.Unit\Regulation.{CC}.{RegulationName}.Tests.Unit.YYYY.csproj

echo.
echo === Test {CC}.{RegulationName} YYYY ===
echo.

:: --- Unit Tests ---
echo --- Unit Tests ---
dotnet test "%UNIT_PROJECT%" --configuration Release --verbosity normal
if errorlevel 1 (
    echo.
    echo [FAILED] Unit tests failed -- integration tests skipped
    exit /b 1
)
echo [OK] Unit tests passed
echo.

:: --- Integration Tests ---
echo --- Integration Tests ---
pushd YYYY
start /b /wait "" "Test.YYYY.pecmd"
set _ERR=%ERRORLEVEL%
popd
if %_ERR% neq 0 (
    echo.
    echo [FAILED] Integration tests failed
    exit /b 1
)
echo [OK] Integration tests passed
echo.
echo === ALL TESTS PASSED ===
