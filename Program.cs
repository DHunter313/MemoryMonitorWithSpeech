using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using SpeechLib;

namespace MemoryMonitorWithSpeech
{
    class Program
    {
        static void Main(string[] args)
        {

            //// Need to add a speech synthesizer to make it talk
            // possibly add a voice-gender change as well            
            //SpeechSynthesizer synth = new SpeechSynthesizer();
            //synth.Speak("Greetings I am Jarvis");

            #region My Performance Counters
            //this will pull the CPU usage
            PerformanceCounter perfCPU = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            perfCPU.NextValue();
            //This will pull the current available memory in MegaByytes
            PerformanceCounter perfMem = new PerformanceCounter("Memory", "Available MBytes");
            perfMem.NextValue();

            //This will get us System up time (in seconds)
            PerformanceCounter perfUpTimeCount = new PerformanceCounter("System", "System Up Time");
            perfUpTimeCount.NextValue();

            #endregion
            TimeSpan upTimeSpan = TimeSpan.FromSeconds(perfUpTimeCount.NextValue());
            string systemUpTimeMessage = string.Format("The current uptime is {0} days, {1} hours, {2} minutes, {3} seconds.",
                (int)upTimeSpan.TotalDays,
                (int)upTimeSpan.Hours,
                (int)upTimeSpan.Minutes,
                (int)upTimeSpan.Seconds
                );

            // Tell the user what the current system uptime is.
            // synth.Speak(systemUpTimeMessage);

            // Infinite While Loop
            while (true)
            {
                int currentCpuPercentage = (int)perfCPU.NextValue();
                int currentAvailableMemory = (int)perfMem.NextValue();

                //Every 1 second print the CPU % load to the console
                Console.WriteLine("CPU Load: {0}%", currentCpuPercentage);
                Console.WriteLine("Available Memory: {0}MB", currentAvailableMemory);
                Thread.Sleep(1000);

                // String created to Speak to the user with text too speech with values.
                string cpuLoadVocalMessage = string.Format("The current CPU load is {0}", currentCpuPercentage);
                string availableMemoryMessage = string.Format("The current memory available is {0} megabytes.", currentAvailableMemory);

                #region Speech: Tell us about current CPU and Memory state
                // Speak to the user with text too speech with values.
                //only tell us when % is above 80%
                if (currentCpuPercentage > 80)
                {
                    if (currentCpuPercentage == 100)
                    {
                        string vocalCpuMessage = string.Format("zholy Crap!!! You're CPU is about to catch on fire!");
                    }
                    else
                    {
                    // synth.Speak(cpuLoadVocalMessage);     Perform once text to speech plug-in is installed

                    }
                }
                //only tell us when Available memory is less that 300 megabytes.
                if (currentAvailableMemory < 300)
                {
                    if (currentAvailableMemory < 100)
                    {                     
                        string vocalMemoryMessage = string.Format("Holy Crap!!! You're computer is about to freeze up!");
                        //synth.Speak(vocalMemoryMessage);

                    }
                    //synth.Speak(availableMemoryMessage);
                }
                #endregion

            } // End of Loop

        }

    }
}
