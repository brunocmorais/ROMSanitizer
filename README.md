# ROMSanitizer

Simple ROM sanitizer, created to help me organizing and filtering my ROMs. It is intended for removing unnecessary ROM files from your romsets, trying to make a "curated" romset.

This tool works based on hardcoded criteria, focusing on playable official ROMs with english language.

There is a option to give a whitelist of terms that must be excluded from program main criteria. All options are given below:

Use: ROMSanitizer [OPTIONS] -path [path] [-whitelist [terms]]

-path: Main path to ROM files
-whitelist: terms that must be ignored in criteria
-print0: print NUL character instead of EOL during output
-h or --help: show this help