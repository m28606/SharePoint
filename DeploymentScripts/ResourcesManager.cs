using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DeploymentScripts
{
    class ResourcesManager
    {
        private string[] EmbeddedResources { get; set; }
        private Assembly Assembly { get; set; }

        public List<String> CreatedFilePaths { get; set; }
        public List<String> CreatedFolderPaths { get; set; }


        public ResourcesManager()
        {
            CreatedFilePaths = new List<String>();
            CreatedFolderPaths = new List<String>();

            Assembly = Assembly.GetExecutingAssembly();
            EmbeddedResources = Assembly.GetManifestResourceNames();
        }


        public void WriteEmbeddedResourcesToFiles()
        {
            foreach (String embeddedResourceName in EmbeddedResources)
            {
                // Get embedded resource file path
                // ie. change 'DeploymentScripts.Scripts.Filename.ps1' to '.\Filename.ps1'

                var filePathRelative = embeddedResourceName.Replace("DeploymentScripts.Scripts", "");
                filePathRelative = filePathRelative.Replace('.', '\\');
                filePathRelative = Shared.ReplaceLastOccurrenceString(filePathRelative, "\\", ".");
                var directoryPathRelative = GetDirectoryPathRelative(filePathRelative);

                if (directoryPathRelative != "")
                {
                    if (!CreatedFolderPaths.Contains(directoryPathRelative))
                    {
                        try
                        {
                            Directory.CreateDirectory("." + directoryPathRelative);
                            CreatedFolderPaths.Add(directoryPathRelative);
                            Shared.WriteMessage(String.Format("Creating folder '{0}'", "." + directoryPathRelative), Shared.MessageOptions.Info);
                        }
                        catch
                        {
                            Shared.WriteMessage(string.Format("Creating folder '{0}' failed.", directoryPathRelative), Shared.MessageOptions.Error);
                        }
                    }
                }

                try
                {
                    var file = Assembly.GetManifestResourceStream(embeddedResourceName);
                    //In case the file is .stp read and write it as binary data.
                    if (embeddedResourceName.EndsWith(".stp"))
                    {
                        using (FileStream writeStream = File.OpenWrite("." + filePathRelative))
                        {
                            BinaryReader reader = new BinaryReader(file);
                            BinaryWriter writer = new BinaryWriter(writeStream);
                            // create a buffer to hold the bytes 
                            byte[] buffer = new Byte[1024];
                            int bytesRead;

                            // while the read method returns bytes
                            // keep writing them to the output stream
                            while ((bytesRead =
                                    file.Read(buffer, 0, 1024)) > 0)
                            {
                                writeStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                    else
                    {
                        var fileContent = new StreamReader(file, new UTF8Encoding(true)).ReadToEnd();
                        File.WriteAllText("." + filePathRelative, fileContent, new UTF8Encoding(true));
                    }
                    
                    CreatedFilePaths.Add(filePathRelative);
                    Shared.WriteMessage(String.Format("Creating file '{0}'", "." + filePathRelative), Shared.MessageOptions.Info);
                }
                catch
                {
                    Shared.WriteMessage(string.Format("Writing '{0}' resource to file '{1}' failed.", embeddedResourceName, filePathRelative), Shared.MessageOptions.Error);
                }
            }
        }


        public void DeleteEmbeddedResources()
        {
            foreach (string filePathRelative in CreatedFilePaths)
            {
                try
                {
                    File.Delete("." + filePathRelative);
                    Shared.WriteMessage(String.Format("Deleting file '{0}'", "." + filePathRelative), Shared.MessageOptions.Info);
                }
                catch (Exception ex)
                {
                    Shared.WriteMessage(String.Format("Deleting file '{0}' failed", "." + filePathRelative), Shared.MessageOptions.Error);
                    Shared.WriteMessage(ex.Message, Shared.MessageOptions.Error);
                }
            }

            foreach (var createdFolderPath in CreatedFolderPaths)
            {
                try
                {
                    Directory.Delete("." + createdFolderPath);
                }
                catch (Exception ex)
                {
                    Shared.WriteMessage(ex.Message, Shared.MessageOptions.Error);
                    Console.ReadLine();
                }
            }
        }

        # region private methods

        private static string GetDirectoryPathRelative(string filePathRelative)
        {
            if (filePathRelative.Split('\\').Count() > 2)
            {
                var splitFilePathRelative = filePathRelative.Split('\\');

                var directoryPath = "\\";
                for (var x = 0; x < splitFilePathRelative.Count() - 1; x++)
                {
                    directoryPath += splitFilePathRelative[x];
                }
                return directoryPath;
            }
            return "";
        }

        # endregion
    }
}
