using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace crypto.Domain
{
    public class Command
    {
        public void Execute(Parameters parametros)
        {
            int erros = 0;
            string[] arquivos = Directory.GetFiles(parametros.Directory, "*.*", parametros.CryptSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            if (parametros.Type.ToLower() == "crypt")
            {
                Log.Information("Iniciando processo de criptografia");

                var configFulllPath = parametros.Directory + "\\" + @"config.ini";
                var configPrefixName = "VHJvdXhh";
                var config = new IniFile(configFulllPath);
                config.Write("archievename", parametros.CryptArchieveName.ToString());
                config.Write("archieve", parametros.CryptArchieves.ToString());
                config.Write("foldername", parametros.CryptFoldersName.ToString());
                config.Write("subfolders", parametros.CryptSubFolders.ToString());

                var cryptedText = Cryptography.Encrypt(File.ReadAllBytes(configFulllPath), parametros.Key);

                File.WriteAllText(configFulllPath + ".crypted", Convert.ToBase64String(cryptedText));
                File.Delete(configFulllPath);
                File.Move(configFulllPath + ".crypted", configFulllPath);

                var name = Cryptography.Encrypt(Encoding.ASCII.GetBytes(configFulllPath.Substring(configFulllPath.LastIndexOf("\\") + 1, configFulllPath.Length - configFulllPath.LastIndexOf("\\") - 1)), parametros.Key);
                File.Move(configFulllPath, configFulllPath.Substring(0, configFulllPath.LastIndexOf("\\")) + "\\" + configPrefixName + Convert.ToBase64String(name).Replace("/", "-"));

                if (parametros.CryptArchieves)
                {
                    Log.Information("Criptografando o conteúdo dos arquivos");

                    foreach (var arq in arquivos)
                    {
                        try
                        {
                            cryptedText = Cryptography.Encrypt(File.ReadAllBytes(arq), parametros.Key);

                            File.WriteAllText(arq + ".crypted", Convert.ToBase64String(cryptedText));
                            File.Delete(arq);
                            File.Move(arq + ".crypted", arq);
                        }
                        catch (Exception){ erros++; }
                    }
                }

                if (parametros.CryptArchieveName)
                {
                    Log.Information("Criptografando o nome dos arquivos");

                    foreach (var arq in arquivos)
                    {
                        try
                        {
                            name = Cryptography.Encrypt(Encoding.ASCII.GetBytes(arq.Substring(arq.LastIndexOf("\\") + 1, arq.Length - arq.LastIndexOf("\\") - 1)), parametros.Key);
                            File.Move(arq, arq.Substring(0, arq.LastIndexOf("\\")) + "\\" + Convert.ToBase64String(name).Replace("/", "-"));
                        }
                        catch (Exception) { erros++; }
                    }
                }

                if (parametros.CryptFoldersName)
                {
                    Log.Information("Criptografando o nome das pastas");

                    var folders = getDirectories(parametros.Directory);

                    foreach (var folder in folders)
                    {
                        try
                        {
                            name = Cryptography.Encrypt(Encoding.ASCII.GetBytes(folder.Substring(folder.LastIndexOf("\\") + 1, folder.Length - folder.LastIndexOf("\\") - 1)), parametros.Key);
                            Directory.Move(folder, folder.Substring(0, folder.LastIndexOf("\\")) + "\\" + Convert.ToBase64String(name).Replace("/", "-"));
                        }
                        catch (Exception) { erros++; }
                    }
                }

                if (erros > 0) Log.Warning("Ao criptografar, ocorreu {0} erro(s).", erros); else Log.Information("Criptografia finalizada com sucesso!");
            }

            if (parametros.Type.ToLower() == "decrypt")
            {
                Log.Information("Iniciando processo de decriptografia");

                erros = 0;
                var configFile = arquivos.Where(n => n.Contains("VHJvdXhh")).FirstOrDefault();

                if (configFile != null)
                {
                    byte[] readText = File.ReadAllBytes(configFile);

                    File.WriteAllBytes(configFile + ".decrypted", Cryptography.Decrypt(readText, parametros.Key));
                    File.Delete(configFile);
                    File.Move(configFile + ".decrypted", configFile);

                    var config = new IniFile(configFile);
                    parametros.CryptArchieveName = Convert.ToBoolean(config.Read("archievename"));
                    parametros.CryptArchieves = Convert.ToBoolean(config.Read("archieve"));
                    parametros.CryptFoldersName = Convert.ToBoolean(config.Read("foldername"));
                    parametros.CryptSubFolders = Convert.ToBoolean(config.Read("subfolders"));

                    arquivos = arquivos.Where(w => w != configFile).ToArray();

                    File.Delete(configFile);
                }

                if (parametros.CryptArchieves)
                {
                    Log.Information("Decriptografando o conteúdo dos arquivos");

                    foreach (var arq in arquivos)
                    {
                        try
                        {
                            byte[] readText = File.ReadAllBytes(arq);

                            File.WriteAllBytes(arq + ".decrypted", Cryptography.Decrypt(readText, parametros.Key));
                            File.Delete(arq);
                            File.Move(arq + ".decrypted", arq);
                        }
                        catch (Exception) { erros++; }
                    }
                }
                    
                if (parametros.CryptArchieveName)
                {
                    Log.Information("Decriptografando o nome dos arquivos");

                    foreach (var arq in arquivos)
                    {
                        try
                        {
                            var name = Cryptography.Decrypt(Encoding.ASCII.GetBytes((arq.Substring(arq.LastIndexOf("\\") + 1, arq.Length - arq.LastIndexOf("\\") - 1)).Replace("-", "/")), parametros.Key);
                            File.Move(arq, arq.Substring(0, arq.LastIndexOf("\\")) + "\\" + Encoding.UTF8.GetString(name));
                        }
                        catch (Exception) { erros++; }
                    }
                }

                if (parametros.CryptFoldersName)
                {
                    Log.Information("Decriptografando o nome das pastas");

                    var folders = getDirectories(parametros.Directory);

                    try
                    {
                        foreach (var folder in folders)
                        {
                            var name = Cryptography.Decrypt(Encoding.ASCII.GetBytes((folder.Substring(folder.LastIndexOf("\\") + 1, folder.Length - folder.LastIndexOf("\\") - 1)).Replace("-", "/")), parametros.Key);
                            Directory.Move(folder, folder.Substring(0, folder.LastIndexOf("\\")) + "\\" + Encoding.UTF8.GetString(name));
                        }
                    }
                    catch (Exception) { erros++; }
                }

                if (erros > 0) Log.Warning("Ao decriptografar, ocorreu {0} erro(s).", erros); else Log.Information("Decriptografia finalizada com sucesso!");
            }
        }

        private IEnumerable<string> getDirectories(string mainDirectory)
        {
            List<string> diretorio = new List<string>();
            string[] subDiretorios = Directory.GetDirectories(mainDirectory);

            if (subDiretorios.Length > 0)
            {
                foreach (var sub in subDiretorios)
                {
                    diretorio.AddRange(getDirectories(sub));
                    diretorio.Add(sub);
                }
            }

            return diretorio;
        }
    }
}
