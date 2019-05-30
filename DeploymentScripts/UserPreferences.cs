using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeploymentScripts
{
    class UserPreferences
    {
        public enum EnvironmentSetting
        {
            Test_WSFTST01,
            Test_SPTST02,
            Staging,
            Production,
            Debug,
            NewProduction,            
            SERVICEPORTAL_TEST,
            SERVICEPORTAL_TEST2,
            SERVICEPORTAL_PET,
        }

        public enum ActionSetting
        {
            Install,
            Uninstall,
            //Ajinkya Korade: 29-01-2016: Added ServiceDashboard & Exit enum.
            ServiceDashboard,
            Exit
        }

        public EnvironmentSetting TargetEnvironment { get; set; }
        public ActionSetting Action { get; set; }
    }
}
