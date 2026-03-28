# OpenMS Thermo Raw File Bridge

A minimal C++/CLI bridge that embeds the .NET runtime to read Thermo `.raw` files via the official ThermoFisher vendor DLLs.

## What This Does
- Embeds .NET 9 runtime inside a native C++ DLL
- Loads ThermoFisher.CommonCore vendor DLLs
- Exposes a simple C++ API to read mass spectra from `.raw` files
- Tested on Windows x64

## Architecture
```
Python / C++ caller
       |
ThermoWrapper.dll  (C++/CLI bridge)
       |
ThermoFisher.CommonCore.RawFileReader.dll  (vendor .NET DLL)
       |
.raw file
```

## API
```cpp
RawBridge::GetScanCount(filePath)      // Total number of scans
RawBridge::GetRetentionTime(filePath, scanNum)  // Retention time in minutes
RawBridge::GetScanFilter(filePath, scanNum)     // Scan filter string
RawBridge::GetScan(filePath, scanNum)           // Full spectrum (m/z + intensity arrays)
```

## Requirements
- Windows x64
- .NET 9 runtime
- Visual Studio 2022 (to compile)
- ThermoFisher.CommonCore DLLs (from NuGet: ThermoFisher.CommonCore.RawFileReader)

## Build
```
MSBuild ThermoWrapper.sln /p:Configuration=Release /p:Platform=x64
```

## Test (Python)
```python
import pythonnet
pythonnet.load("coreclr")
import clr, sys
sys.path.append(r"path\to\dlls")
clr.AddReference("ThermoWrapper")
from ThermoWrapper import RawBridge

count = RawBridge.GetScanCount(r"path\to\file.raw")
scan = RawBridge.GetScan(r"path\to\file.raw", 1)
print(scan.Masses, scan.Intensities)
```

## Status
- [x] Windows x64 - Working
- [ ] Linux - In progress
- [ ] macOS - Planned
