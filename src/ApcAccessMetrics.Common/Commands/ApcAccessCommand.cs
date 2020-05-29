using System;
using System.Diagnostics;

namespace ApcAccessMetrcs.Common.Commands
{
    public class ApcAccessCommand 
    {
        public string Run()
        {
            using(var process = new Process())
            {
                process.StartInfo.FileName = "apcaccess";
                process.StartInfo.Arguments = "-u"; // -u strips units, e.g. 'thing : 1 percent' is just 'thing : 1'
                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                process.WaitForExit();
                var stdError = process.StandardError.ReadToEnd();
                var stdOut = process.StandardOutput.ReadToEnd();
                if(!string.IsNullOrWhiteSpace(stdError))
                {
                    throw new Exception(stdError);
                }

                return stdOut;
            }
        }
    }
}