using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;
using DeploymentScripts;
using Microsoft.Deployment.Compression.Cab;

namespace TPCIPDeployer
{
    class Program
    {
        private const string PRODUCTION_PORTAL_URL = "http://serviceportal.tdk.dk";
        private const string TEST_WSFTST01_PORTAL_URL = "http://wsftst01.tdk.dk:8080";
        private const string TEST_SPTST02_PORTAL_URL = "http://sptst02:8080";
        private const string STAGING_PORTAL_URL = "http://serviceportalpreprod.tdk.dk";

        private const string NEW_PRODUCTION_PORTAL_URL = "http://serviceportalnew.tdk.dk";
        private const string TEST_SERVICEPORTALTEST_URL = "http://serviceportaltest.tdk.dk";
        private const string TEST_SERVICEPORTALTEST2_URL = "http://serviceportaltest2.tdk.dk";
        private const string STAGING_SERVICEPORTALPET_URL = "http://serviceportalpet.tdk.dk";
        
        private static string _debugPortalUrl { get; set; }

        static void Main(string[] args)
        {
            Shared.WriteMessage("Welcome to TPCIP deployer.", Shared.MessageOptions.Success);
            var preferences = GetUserPreferences();
            //Ajinkya Korade: 29-01-2016: Show Options until user wants to exit
            while (preferences.Action != UserPreferences.ActionSetting.Exit)
            {
                var psParameterString = CreatePowershellArguments(preferences);
                var resourcesManager = new ResourcesManager();
                resourcesManager.WriteEmbeddedResourcesToFiles();
                UpdateConfigurationInWsp(preferences);
                RunPowershellInstance(psParameterString);
                resourcesManager.DeleteEmbeddedResources();
                preferences = GetUserPreferences();
            }
            //Ajinkya Korade: 29-01-2016: Make Console Visible unless and until user presses any key
            Console.WriteLine("You Have Successfully Exited; Please press any key to close console...");
            Console.ReadLine();
        }

        private static void UpdateConfigurationInWsp(UserPreferences preferences)
        {
            try
            {
            var filepath = @".\TPCIP.DeployAll.wsp";
            var tempfolder = @".\tempunpack";
            var cabfile = new CabInfo(filepath);
            if (cabfile == null)
            {
                filepath = @".\TPCIP.Web.wsp";
                cabfile = new CabInfo(filepath);
            }
            cabfile.Unpack(tempfolder);

            var configFileName = string.Empty;
            switch (preferences.TargetEnvironment)
            {
                case UserPreferences.EnvironmentSetting.Test_WSFTST01:
                case UserPreferences.EnvironmentSetting.SERVICEPORTAL_TEST:
                    configFileName = "web.test_wsftst01.config";
                    break;
                case UserPreferences.EnvironmentSetting.Test_SPTST02:
                case UserPreferences.EnvironmentSetting.SERVICEPORTAL_TEST2:
                    configFileName = "web.test_sptst02.config";
                    break;
                case UserPreferences.EnvironmentSetting.Staging:
                case UserPreferences.EnvironmentSetting.SERVICEPORTAL_PET:
                    configFileName = "web.staging.config";
                    break;
                case UserPreferences.EnvironmentSetting.Production:
                case UserPreferences.EnvironmentSetting.NewProduction:
                    configFileName = "web.release.config";
                    break;
                case UserPreferences.EnvironmentSetting.Debug:
                    configFileName = "web.debug.config";
                    break;
                default:
                    break;
            }

            var files = Directory.GetFiles(tempfolder, configFileName, SearchOption.AllDirectories);
            File.Copy(files[0], Path.Combine(Path.GetDirectoryName(files[0]), "web.config"), true);
            var newCabfile = new CabInfo(filepath);
            newCabfile.Pack(tempfolder, true, Microsoft.Deployment.Compression.CompressionLevel.Min, null);
            Directory.Delete(tempfolder, true);
        }
            catch { }
        }

        #region Main functions

        private static UserPreferences GetUserPreferences()
        {
            var preferences = new UserPreferences();
            preferences.Action = PromptUserForInstallUninstall();
            //Ajinkya Korade: 29-01-2016: If User action is not exit then prompt for environment settings
            if (preferences.Action != UserPreferences.ActionSetting.Exit)
            {
                //Ajinkya Korade: 29-01-2016: If Environment settings is debug then prompt for user URL
                preferences.TargetEnvironment = PromptUserForEnvironmentSetting();
            if (preferences.TargetEnvironment == UserPreferences.EnvironmentSetting.Debug)
                _debugPortalUrl = PromptUserForUrl();
            }
            return preferences;
        }

        private static string CreatePowershellArguments(UserPreferences preferences)
        {
            // The original command was something like this:
            // Process.Start("C:\\WINDOWS\\system32\\WindowsPowerShell\\v1.0\\powershell.exe", " -v 2 -noexit \"& '" + Directory.GetCurrentDirectory() + "\\DeployTdcPortalOnTestEnv.ps1'\"");

            string scriptName = "";
            string siteCollectionUrl = "";

            switch (preferences.Action)
            {
                case UserPreferences.ActionSetting.Install:
                    scriptName = "\\DeployTdcTpcipSolution.ps1' \"";
                    break;
                case UserPreferences.ActionSetting.Uninstall:
                    scriptName = "\\UninstallTdcTpcipSolution.ps1' \"";
                    break;
                //Ajinkya Korade: 29-01-2016: Execute Script ServiceDashboardSharepoint.ps1 for Action ServiceDashboard
                case UserPreferences.ActionSetting.ServiceDashboard:
                    scriptName = "\\ServiceDashboardSharepoint.ps1' \"";
                    break;
            }
            
            switch (preferences.TargetEnvironment)
            {
                case UserPreferences.EnvironmentSetting.Production:
                    siteCollectionUrl = PRODUCTION_PORTAL_URL;
                    break;
                case UserPreferences.EnvironmentSetting.NewProduction:
                    siteCollectionUrl = NEW_PRODUCTION_PORTAL_URL;
                    break;
                case UserPreferences.EnvironmentSetting.Test_WSFTST01:
                    siteCollectionUrl = TEST_WSFTST01_PORTAL_URL;
                    break;
                case UserPreferences.EnvironmentSetting.SERVICEPORTAL_TEST:
                    siteCollectionUrl = TEST_SERVICEPORTALTEST_URL;
                    break;
                case UserPreferences.EnvironmentSetting.Test_SPTST02:
                    siteCollectionUrl = TEST_SPTST02_PORTAL_URL;
                    break;
                case UserPreferences.EnvironmentSetting.SERVICEPORTAL_TEST2:
                    siteCollectionUrl = TEST_SERVICEPORTALTEST2_URL;
                    break;
                case UserPreferences.EnvironmentSetting.Staging:
                    siteCollectionUrl = STAGING_PORTAL_URL;
                    break;
                case UserPreferences.EnvironmentSetting.SERVICEPORTAL_PET:
                    siteCollectionUrl = STAGING_SERVICEPORTALPET_URL;
                    break;
                case UserPreferences.EnvironmentSetting.Debug:
                    siteCollectionUrl = _debugPortalUrl;
                    break;
            }

            var arguments = "-v 2 ";

            if (preferences.TargetEnvironment == UserPreferences.EnvironmentSetting.Debug)
            {
                arguments += "-noexit ";
            }

            arguments += "\"& '" + Directory.GetCurrentDirectory() + scriptName;
            arguments += string.Format("-SiteUrl {0} ", siteCollectionUrl);
            arguments += "\"";
            //Console.WriteLine(arguments);
            return arguments;
        }

        private static void RunPowershellInstance(string arguments)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = arguments,
            };

            var process = new Process
            {
                StartInfo = psi,
                EnableRaisingEvents = true,
            };

            process.Start();
            process.WaitForExit();
        }
        #endregion


        #region Console prompts

        private static UserPreferences.ActionSetting PromptUserForInstallUninstall()
        {
            while (true)
            {
                //Ajinkya Korade: 29-01-2016: Prompt Operation options to user.
                Shared.WriteMessage("\n\nSelect Operation to perform from below",Shared.MessageOptions.Title);
                string promptOptions = string.Concat("\t 1. TPCIP Tools Installation"
                            , "\n\t 2. ServiceDashboard Database Installation"
                            , "\n\t 3. TPCIP Tools Uninstallation"
                            , "\n\t 4. Exit");
                Console.WriteLine(promptOptions);
                Console.Write("> Enter Option : ");
                var userInput = Console.ReadLine();
                if (userInput != null)
                {
                    switch (userInput)
                    {
                        case "1":
                            return UserPreferences.ActionSetting.Install;
                        //Ajinkya Korade: 29-01-2016: Added ServiceDashboard & Exit case
                        case "2":
                            return UserPreferences.ActionSetting.ServiceDashboard;
                        case "3":
                            return UserPreferences.ActionSetting.Uninstall;
                        case "4":
                            return UserPreferences.ActionSetting.Exit;
                    }
                }
            }
        }

        private static UserPreferences.EnvironmentSetting PromptUserForEnvironmentSetting()
        {
            while (true)
            {
                //Ajinkya Korade: 29-01-2016: Custom Enviroment Settings Option
                Shared.WriteMessage("\n\nSelect Environment Settings From Below", Shared.MessageOptions.Title);
                string promptString = string.Concat( "\t 1. PRODUCTION environment settings"
                                    , "\n\t 2. TEST(WSFTST01) environment settings"
                                    , "\n\t 3. TEST(SPTST02) environment settings"
                                    , "\n\t 4. STAGING environment settings"
                                    , "\n====================================="
                                    , "\n\t 5. NEW PRODUCTION environment settings"
                                    , "\n\t 6. TEST(SERVICEPORTALTEST) environment settings"
                                    , "\n\t 7. TEST(SERVICEPORTALTEST2) environment settings"
                                    , "\n\t 8. STAGING(SERVICEPORTALPET) environment settings"
                                    , "\n\t 9. CUSTOM environment settings");
                Console.WriteLine(promptString);
                Console.Write("> Enter Option : ");
                var userInput = Console.ReadLine();
                if (userInput != null)
                {
                    switch (userInput)
                    {
                        case "1":
                            return UserPreferences.EnvironmentSetting.Production;
                        case "5":
                            return UserPreferences.EnvironmentSetting.NewProduction;
                        case "2":
                            return UserPreferences.EnvironmentSetting.Test_WSFTST01;
                        case "6":
                            return UserPreferences.EnvironmentSetting.SERVICEPORTAL_TEST;

                        case "3":
                            return UserPreferences.EnvironmentSetting.Test_SPTST02;
                        case "7":
                            return UserPreferences.EnvironmentSetting.SERVICEPORTAL_TEST2;

                        case "4":
                            return UserPreferences.EnvironmentSetting.Staging;
                        case "8":
                            return UserPreferences.EnvironmentSetting.SERVICEPORTAL_PET;

                        case "9":
                            return UserPreferences.EnvironmentSetting.Debug;
                    }
                }
            }
        }

        private static string PromptUserForUrl()
        {
            Console.Write("> Enter custom SiteCollection url :");
            return Console.ReadLine();
        }
        #endregion
    }
}
