



// All versions of the CSGL (C# OpenGL) software are created by Colin P. Fahey
// ( http://colinfahey.com ) and are declared to be in the public domain.
//
// Therefore, all versions of the CSGL (C# OpenGL) software can be used for any
// purpose (commercial or private), without payment, without restrictions, and
// without obligations.  The code can be modified, or portions reused, without
// restrictions, and without obligations.  This note may be removed from the
// code, and there is no need to mention the original author of this code.
//
// This file is part of CSGL 12 (2009 July 25).








using System.Runtime.InteropServices;








namespace CSGL12
{




    public sealed class PrecisionTime
    {




        private long mPerformanceCounterFrequencyInCountsPerSecond = 0L;

        private long mPerformanceCounterStartCount = 0L;








        public PrecisionTime()
        {
            mPerformanceCounterFrequencyInCountsPerSecond = 0L;
            mPerformanceCounterStartCount = 0L;


            PrecisionTime_Initialize();
        }








        public void PrecisionTime_Initialize()
        {
            // Although this method is called by the PrecisionTime constructor,
            // calling this method again at a later time will update the cached 
            // counter frequency value (usually not necessary) and will update 
            // the start count to the present (i.e., to "now").

            mPerformanceCounterFrequencyInCountsPerSecond = PrecisionTime_GetPerformanceCounterFrequency();
            mPerformanceCounterStartCount = PrecisionTime_GetPerformanceCounterValue();
        }








        public long PrecisionTime_GetPerformanceCounterFrequency()
        {
            long countsPerSecond = 0L;

            bool querySucceeded = false;

            querySucceeded = PrecisionTime.Kernel32_QueryPerformanceFrequency(out countsPerSecond);

            if (false == querySucceeded)
            {
                // QueryPerformanceFrequency() failed.
                countsPerSecond = 0L; // Set to zero to indicate that the value is invalid.
            }

            return (countsPerSecond);
        }








        public long PrecisionTime_GetPerformanceCounterValue()
        {
            long counterValue = 0L;

            bool querySucceeded = false;

            querySucceeded = PrecisionTime.Kernel32_QueryPerformanceCounter(out counterValue);

            if (false == querySucceeded)
            {
                // QueryPerformanceCounter() failed.
                counterValue = 0L; // Set to zero to have a predictable value for this failure case.
            }

            return (counterValue);
        }








        public double PrecisionTime_GetElapsedTimeSeconds()
        {
            if (0L == mPerformanceCounterFrequencyInCountsPerSecond)
            {
                // The counter frequency has not already been cached, which means
                // that the attempt to query and cache the counter frequency in 
                // the PrecisionTime constructor failed, or the most recent attempt
                // to query and cache the counter frequency made by a call to
                // PrecisionTime_Initialize() has failed.  Therefore, we cannot
                // determine the elapsed time.

                return (0.0);
            }


            long currentCountValue = 0L;

            currentCountValue = PrecisionTime_GetPerformanceCounterValue();


            long differenceInCount = (currentCountValue - mPerformanceCounterStartCount);


            double elapsedTimeInSeconds = 0.0;

            if (0L != mPerformanceCounterFrequencyInCountsPerSecond)
            {
                elapsedTimeInSeconds =
                    (double)differenceInCount / (double)mPerformanceCounterFrequencyInCountsPerSecond;
            }


            return (elapsedTimeInSeconds);
        }








        [System.Runtime.InteropServices.DllImport("kernel32", EntryPoint = "QueryPerformanceFrequency"),
        System.Security.SuppressUnmanagedCodeSecurity]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private
        static
        extern
        bool
        Kernel32_QueryPerformanceFrequency
        (
            out long countFrequency
        );








        [System.Runtime.InteropServices.DllImport("kernel32", EntryPoint = "QueryPerformanceCounter"),
        System.Security.SuppressUnmanagedCodeSecurity]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private
        static
        extern
        bool
        Kernel32_QueryPerformanceCounter
        (
            out long countValue
        );




    }




}








