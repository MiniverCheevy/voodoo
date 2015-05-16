#if !DNXCORE50
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using Microsoft.Build.Tasks;
using Microsoft.Build.Utilities;
using Xunit;


namespace Voodoo.Tests
{
    
    public class MultiTargetedBuilds
    {
       //TODO: find this programatically
        private FileInfo ProjectPath {
            get
            {
                return new FileInfo(System.Configuration.ConfigurationManager.AppSettings["csprojPath"]);
            }
        }

        public MultiTargetedBuilds()
        {
            
        }


        [Fact]
        public void Build_Net4_IsOk()
        {

            var properties = new Dictionary<string, string> {{"Configuration", "Release net40"}};
            build(ProjectPath, new string[] { "Build" }, properties);

        }
        [Fact]
        public void Build_Net45_IsOk()
        {

            var properties = new Dictionary<string, string> { { "Configuration", "Release net45" } };
            build(ProjectPath, new string[] { "Build" }, properties);

        }
        [Fact]
        public void Build_Net451_IsOk()
        {

            var properties = new Dictionary<string, string> { { "Configuration", "Release net451" } };
            build(ProjectPath, new string[] { "Build" }, properties);

        }
        [Fact]
        public void Build_Net452_IsOk()
        {

            var properties = new Dictionary<string, string> { { "Configuration", "Release net452" } };
            build(ProjectPath, new string[] { "Build" }, properties);

        }
        [Fact]
        public void Build_Net46_IsOk()
        {

            var properties = new Dictionary<string, string> { { "Configuration", "Release net46" } };
            build(ProjectPath, new string[] { "Build" }, properties);

        }
        private bool build(FileInfo msbuildFile, string[] targets = null, IDictionary<string, string> properties = null, LoggerVerbosity loggerVerbosity = LoggerVerbosity.Detailed)
        {
            if (!msbuildFile.Exists) throw new ArgumentException("msbuildFile does not exist");

            if (targets == null)
            {
                targets = new string[] { };
            }
            if (properties == null)
            {
                properties = new Dictionary<string, string>();
            }

            Console.Out.WriteLine("Running {0} targets: {1} properties: {2}, cwd: {3}",
                                  msbuildFile.FullName,
                                  string.Join(",", targets),
                                  string.Join(",", properties),
                                  Environment.CurrentDirectory);
            ProjectCollection.GlobalProjectCollection.UnloadAllProjects();
            var project = new Project(msbuildFile.FullName, properties, "4.0");

            return project.Build(targets, new ILogger[] { new BuildLogger() });
        }

        
    }

  public class BuildLogger : Logger
	{
		public override void Initialize(IEventSource eventSource)
		{
			
			eventSource.ProjectStarted += new ProjectStartedEventHandler(eventSource_ProjectStarted);
			eventSource.TaskStarted += new TaskStartedEventHandler(eventSource_TaskStarted);
			eventSource.MessageRaised += new BuildMessageEventHandler(eventSource_MessageRaised);
			eventSource.WarningRaised += new BuildWarningEventHandler(eventSource_WarningRaised);
			eventSource.ErrorRaised += new BuildErrorEventHandler(eventSource_ErrorRaised);
			eventSource.ProjectFinished += new ProjectFinishedEventHandler(eventSource_ProjectFinished);
		}

		void eventSource_ErrorRaised(object sender, BuildErrorEventArgs e)
		{
			string line = String.Format(": ERROR ({3}){4} {0}({1},{2}): ", e.File, e.LineNumber, e.ColumnNumber, e.Code, e.Message);
			writeLineWithSenderAndMessage(line, e);
            throw new Exception(line);
		}
		
		void eventSource_WarningRaised(object sender, BuildWarningEventArgs e)
		{

			string line = String.Format(": Warning {0}({1},{2}): ", e.File, e.LineNumber, e.ColumnNumber);
			writeLineWithSenderAndMessage(line, e);
		}

		void eventSource_MessageRaised(object sender, BuildMessageEventArgs e)
		{
			if ((e.Importance == MessageImportance.High && IsVerbosityAtLeast(LoggerVerbosity.Minimal))
				|| (e.Importance == MessageImportance.Normal && IsVerbosityAtLeast(LoggerVerbosity.Normal))
				|| (e.Importance == MessageImportance.Low && IsVerbosityAtLeast(LoggerVerbosity.Detailed))				
				)
			{
				writeLineWithSenderAndMessage(String.Empty, e);
			}
		}

		void eventSource_TaskStarted(object sender, TaskStartedEventArgs e)
		{
		}
		
		void eventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
		{
			writeLine(String.Empty, e);
			indent++;
		}

		void eventSource_ProjectFinished(object sender, ProjectFinishedEventArgs e)
		{
			indent--;
			writeLine(String.Empty, e);
		}
		
		private void writeLineWithSenderAndMessage(string line, BuildEventArgs e)
		{			
				writeLine(e.SenderName + ": " + line, e);			
		}
		

		private void writeLine(string line, BuildEventArgs e)
		{
			for (int i = indent; i > 0; i--)
			{
				Console.Write("\t");
			}
            Console.WriteLine(line + e.Message);
		}
	
		public override void Shutdown()
		{
			
		}

		
		private int indent;
	}
}

#endif