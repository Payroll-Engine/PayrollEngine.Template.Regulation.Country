#!/bin/bash

# ============================================================
# Pack.YYYY.sh -- {CC}.{RegulationName} (macOS/Linux)
# Builds and packs all YYYY sub-projects (Functional + Data).
# Output: {subproject}/nupkgs/*.nupkg
#
# Add one "pack <subdir>" line per sub-project.
# Remove sub-projects that do not exist in this regulation.
# ============================================================

YEAR=YYYY
REPO={CC}.{RegulationName}
ERRORS=0

echo ""
echo "=== Pack $REPO $YEAR ==="
echo ""

pack() {
    local SUBDIR="$1"
    echo "--- $SUBDIR ---"

    if [ ! -f "$SUBDIR/Directory.Build.props" ]; then
        echo "[SKIP] Directory.Build.props not found"
        echo ""
        return 0
    fi

    pushd "$SUBDIR" > /dev/null

    local VER
    VER=$(grep -i '<Version>' Directory.Build.props | sed 's/.*<Version>\(.*\)<\/Version>.*/\1/' | tr -d ' \r')
    echo "Version: $VER"

    if echo "$VER" | grep -qi '\.dev'; then
        echo "[WARN] Development version -- skipping pack"
        popd > /dev/null
        echo ""
        return 0
    fi

    dotnet restore --verbosity quiet
    if [ $? -ne 0 ]; then
        echo "[ERROR] restore failed"
        ERRORS=$((ERRORS + 1))
        popd > /dev/null
        echo ""
        return 1
    fi

    dotnet build --configuration Release --no-restore --verbosity quiet
    if [ $? -ne 0 ]; then
        echo "[ERROR] build failed"
        ERRORS=$((ERRORS + 1))
        popd > /dev/null
        echo ""
        return 1
    fi

    dotnet pack --configuration Release --no-build --output nupkgs
    if [ $? -ne 0 ]; then
        echo "[ERROR] pack failed"
        ERRORS=$((ERRORS + 1))
        popd > /dev/null
        echo ""
        return 1
    fi

    echo "[OK]"
    popd > /dev/null
    echo ""
}

pack YYYY
# pack Data.{Source}.YYYY

echo ""
if [ $ERRORS -gt 0 ]; then
    echo "=== COMPLETED WITH $ERRORS ERROR(S) ==="
    exit 1
else
    echo "=== COMPLETED SUCCESSFULLY ==="
    echo "Packages written to {subproject}/nupkgs/"
fi
