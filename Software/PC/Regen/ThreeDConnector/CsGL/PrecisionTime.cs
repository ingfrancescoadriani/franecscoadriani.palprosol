// Copyright (c) 2013, Colin P. Fahey ( http://colinfahey.com )
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following condition is met:
//
// Redistributions of source code must retain the above copyright notice, 
// this list of conditions, and the following disclaimer:
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.




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








