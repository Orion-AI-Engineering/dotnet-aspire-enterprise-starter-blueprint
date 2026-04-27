#!/usr/bin/env bash
set -euo pipefail
for path in README.md src tests infra scripts; do
  test -e "$path"
done
echo "Repository structure looks valid."
