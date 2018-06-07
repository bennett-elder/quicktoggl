using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickToggl
{
    public class KeyVerification
    {
        public static void VerifyKeyAndSaveIt(string key)
        {
            var toggl = new Toggl.Api.TogglClient(key);
            var request = toggl.Users.GetCurrent();
            if (request.IsCompleted)
            {
                if (request.IsFaulted)
                {
                    if (request.Exception.InnerExceptions != null)
                    {
                        if (request.Exception.InnerException?.Message ==
                            "The remote server returned an error: (403) Forbidden.")
                        {
                            throw new Exception("Bad API key");
                        }

                        throw request.Exception.InnerException;
                    }
                    else
                    {
                        throw request.Exception;
                    }
                }

                Environment.SetEnvironmentVariable("TOGGL_COM_API_KEY", key, EnvironmentVariableTarget.User);

                if (Environment.GetEnvironmentVariable("TOGGL_COM_API_KEY", EnvironmentVariableTarget.User).Length == 0)
                {
                    throw new Exception("Blank key stored in environment variable - fill in your key based on what your Toggl.com profile says.");
                }
            }
        }
    }

}
