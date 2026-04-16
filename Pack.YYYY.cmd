@echo off
setlocal enabledelayedexpansion

:: ============================================================
:: Pack.YYYY.cmd -- {CC}.{RegulationName} (Windows)
:: Builds and packs all YYYY sub-projects (Functional + Data).
:: Output: {subproject}\nupkgs\*.nupkg
::
:: Add one "call :pack <subdir>" line per sub-project.
:: Remove sub-projects that do not exist in this regulation.
:: ============================================================

set YEAR=YYYY
set REPO={CC}.{RegulationName}
set ERRORS=0

echo.
echo === Pack %REPO% %YEAR% ===
echo.

call :pack YYYY
:: call :pack Data.{Source}.YYYY

goto :summary

:: ------------------------------------------------------------
:pack
set SUBDIR=%~1
echo --- %SUBDIR% ---

if not exist "%SUBDIR%\Directory.Build.props" (
    echo [SKIP] Directory.Build.props not found
    echo.
    exit /b 0
)

pushd "%SUBDIR%"

for /f "tokens=2 delims=><" %%v in ('findstr /i "<Version>" Directory.Build.props') do set VER=%%v
echo Version: !VER!

echo !VER! | findstr /i "\.dev" >nul
if not errorlevel 1 (
    echo [WARN] Development version -- skipping pack
    popd
    echo.
    exit /b 0
)

dotnet restore --verbosity quiet
if errorlevel 1 (
    echo [ERROR] restore failed
    set /a ERRORS+=1
    popd & echo. & exit /b 1
)

dotnet build --configuration Release --no-restore --verbosity quiet
if errorlevel 1 (
    echo [ERROR] build failed
    set /a ERRORS+=1
    popd & echo. & exit /b 1
)

dotnet pack --configuration Release --no-build --output nupkgs
if errorlevel 1 (
    echo [ERROR] pack failed
    set /a ERRORS+=1
    popd & echo. & exit /b 1
)

echo [OK]
popd
echo.
exit /b 0

:: ------------------------------------------------------------
:summary
echo.
if %ERRORS% GTR 0 (
    echo === COMPLETED WITH %ERRORS% ERROR(S) ===
    exit /b 1
) else (
    echo === COMPLETED SUCCESSFULLY ===
    echo Packages written to {subproject}\nupkgs\
)
