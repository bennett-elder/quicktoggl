using System;
using System.Collections.Generic;
using System.Linq;
using Toggl.Api;
using Toggl.Api.DataObjects;

namespace TogglPlus
{
    class InteractWithToggl
    {
        public Toggl.Api.TogglClient TogglClient { get; private set; }

        private string TogglApiKey
        {
            get {
                string key = Environment.GetEnvironmentVariable("TOGGL_COM_API_KEY", EnvironmentVariableTarget.User);
                if (key?.Length > 0)
                {
                    return key;
                }
                else
                {
                    throw new Exception("No toggl api key assigned to TOGGL_COM_API_KEY environment variable for this user!");
                }
            }
        }

        private List<Toggl.Api.DataObjects.Project> Projects
        {
            get { return TogglClient.Projects.List().Result; }
        }

        private long _currentWorkspace;
        private long CurrentWorkspace
        {
            get
            {
                if (_currentWorkspace > 0)
                {
                    return _currentWorkspace;
                }

                List<Workspace> workspaces = TogglClient.Workspaces.List().Result;
                if (workspaces.Count == 0)
                {
                    throw new Exception("You have no workspaces in your Toggl account - I'd recommend creating one and putting your entries in it so that your team can use Toggl together.");
                }
                if (workspaces.Count > 1)
                {
                    throw new Exception("You have more than one workspace in your Toggl account - I'd recommend either trimming down to one or extending this section to support multiple Toggl workspaces.");
                }

                _currentWorkspace = workspaces[0].Id;
                return _currentWorkspace;
            }
        }

        public InteractWithToggl()
        {
            TogglClient = new TogglClient(TogglApiKey);
        }

        public void StopCurrentTask()
        {
            var current = TogglClient.TimeEntries.Current();

            TogglClient.Workspaces.List();


            if (current.IsCompleted && !current.IsFaulted && current.Result.Start != null && current.Result.Stop == null)
            {
                // currently running
                TogglClient.TimeEntries.Stop(current.Result);
            }
        }

        public void StartNewTask(string text, string project)
        {
            var now = DateTime.Now;
            var te = new TimeEntry
            {
                Description = text,
                WorkspaceId = CurrentWorkspace,
                Start = $"{now.ToShortDateString()} {now.ToShortTimeString()}",
                TagNames = new List<string>(),
                CreatedWith = "QuickToggl",
                ProjectId = FindProjectIdForThisProjectName(project)
            };
            
            
            var request = TogglClient.TimeEntries.Start(te);
        }

        private int? FindProjectIdForThisProjectName(string name)
        {
            var projectsThatMatch = Projects.Where(x => x.Name == name);
            if (projectsThatMatch.Any())
            {
                return projectsThatMatch.FirstOrDefault().Id;
            }
            else
            {
                var p = new Project {Name = name};
                var request = TogglClient.Projects.Add(p);
                return request.Result.Id;
            }
        }
    }
}
