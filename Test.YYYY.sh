#!/bin/bash
set -euo pipefail

# ============================================================
# Test.YYYY.sh -- {CC}.{RegulationName} (macOS/Linux)
#
# Executes all tests in order:
#   1. Unit tests    (dotnet test -- no PE backend required)
#      Remove this block if the regulation has no unit tests.
#   2. Integration   (.pecmd via open association)
#
# Prerequisites:
#   - .NET SDK on PATH
#   - .pecmd extension registered (register-pecmd.sh)
#   - PE backend running (for integration tests)
# ============================================================

UNIT_PROJECT="YYYY/Tests.Unit/Regulation.{CC}.{RegulationName}.Tests.Unit.YYYY.csproj"

echo ""
echo "=== Test {CC}.{RegulationName} YYYY ==="
echo ""

# --- Unit Tests ---
echo "--- Unit Tests ---"
dotnet test "$UNIT_PROJECT" --configuration Release --verbosity normal
echo "[OK] Unit tests passed"
echo ""

# --- Integration Tests ---
echo "--- Integration Tests ---"
open -W "YYYY/Test.YYYY.pecmd"
echo "[OK] Integration tests passed"
echo ""
echo "=== ALL TESTS PASSED ==="
