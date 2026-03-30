using System;
using System.Runtime.InteropServices;
using ThermoFisher.CommonCore.RawFileReader;
using ThermoFisher.CommonCore.Data.Business;

namespace ThermoWrapperManaged
{
    public static class RawBridge
    {
        [UnmanagedCallersOnly(EntryPoint = "GetScanCount")]
        public static int GetScanCount(IntPtr filePathPtr)
        {
            try
            {
                string? filePath = Marshal.PtrToStringUTF8(filePathPtr);
                if (filePath == null) return -1;
                var rawFile = RawFileReaderAdapter.FileFactory(filePath);
                if (rawFile == null) return -1;
                rawFile.SelectInstrument(Device.MS, 1);
                int lastScan = rawFile.RunHeader.LastSpectrum;
                rawFile.Dispose();
                return lastScan;
            }
            catch (Exception)
            {
                return -2;
            }
        }
    }
}
