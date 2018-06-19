using Ionic.Zlib;
using Microsoft.CSharp;
using Mono.Cecil;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AssemblyHashAlgorithm = Mono.Cecil.AssemblyHashAlgorithm;

namespace tModVS
{
    internal class TmodFile
    {
        public Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
        public Version tModLoaderVersion;
        public string name;
        public string path;
        public string author;
        public Version version;
        public byte[] hash;
        public byte[] signature = new byte[256];
        public string[] dllReferences;
        public string[] modReferences;
        public string[] weakReferences;
        public string[] sortAfter;
        public string[] sortBefore;
        public string displayName;
        public string homepage;
        public string description;
        public bool includePDB;
        public bool editAndContinue;
        public byte side = 0;

        internal void AddFile(string fileName, byte[] data)
        {
            this.files[fileName.Replace('\\', '/')] = data;
        }
        private static int GetFileState(string fileName)
        {
            if (fileName == "Info" || fileName == "icon.png")
            {
                return 2;
            }
            if (fileName.EndsWith(".dll") || fileName.EndsWith(".pdb"))
            {
                return 3;
            }
            if (fileName.EndsWith(".png") || fileName.EndsWith(".rawimg") || fileName.EndsWith(".mp3") || fileName.EndsWith(".wav") || fileName.EndsWith(".xnb") || fileName.StartsWith("Streaming/"))
            {
                return 5;
            }
            return 4;
        }
        internal byte[] GetInfo()
        {
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    if (dllReferences.Length != 0)
                    {
                        binaryWriter.Write("dllReferences");
                        foreach (var t in dllReferences)
                        {
                            binaryWriter.Write(t);
                        }
                        binaryWriter.Write("");
                    }
                    if (modReferences.Length != 0)
                    {
                        binaryWriter.Write("modReferences");
                        foreach (var t in modReferences)
                        {
                            binaryWriter.Write(t);
                        }
                        binaryWriter.Write("");
                    }
                    if (weakReferences.Length != 0)
                    {
                        binaryWriter.Write("weakReferences");
                        foreach (var t in weakReferences)
                        {
                            binaryWriter.Write(t);
                        }
                        binaryWriter.Write("");
                    }
                    if (sortAfter.Length != 0)
                    {
                        binaryWriter.Write("sortAfter");
                        foreach (var t in sortAfter)
                        {
                            binaryWriter.Write(t);
                        }
                        binaryWriter.Write("");
                    }
                    if (sortBefore.Length != 0)
                    {
                        binaryWriter.Write("sortBefore");
                        foreach (var t in sortBefore)
                        {
                            binaryWriter.Write(t);
                        }
                        binaryWriter.Write("");
                    }
                    if (author.Length > 0)
                    {
                        binaryWriter.Write("author");
                        binaryWriter.Write(author);
                    }
                    binaryWriter.Write("version");
                    binaryWriter.Write(version.ToString());
                    if (displayName.Length > 0)
                    {
                        binaryWriter.Write("displayName");
                        binaryWriter.Write(displayName);
                    }
                    if (homepage.Length > 0)
                    {
                        binaryWriter.Write("homepage");
                        binaryWriter.Write(homepage);
                    }
                    if (description.Length > 0)
                    {
                        binaryWriter.Write("description");
                        binaryWriter.Write(description);
                    }
                    if (includePDB)
                    {
                        binaryWriter.Write("includePDB");
                    }
                    if (editAndContinue)
                    {
                        binaryWriter.Write("editAndContinue");
                    }
                    if (side != 0)
                    {
                        binaryWriter.Write("side");
                        binaryWriter.Write(side);
                    }
                    binaryWriter.Write("");
                }
                result = memoryStream.ToArray();
            }
            return result;
        }

        internal void AddResource(string relPath, string filePath)
        {
            if (relPath.EndsWith(".png") && relPath != "icon.png")
            {
                using (var fs = File.OpenRead(filePath))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (Bitmap bitmap = new Bitmap(fs))
                        {
                            if (bitmap.Width <= 2048 && bitmap.Height <= 2048)
                            {
                                BitmapData bitmapData = bitmap.LockBits(
                                    new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                                    PixelFormat.Format32bppArgb);
                                int[] array = new int[bitmap.Width * bitmap.Height];
                                Marshal.Copy(bitmapData.Scan0, array, 0, array.Length);
                                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                                binaryWriter.Write(1);
                                binaryWriter.Write(bitmap.Width);
                                binaryWriter.Write(bitmap.Height);
                                foreach (int num in array)
                                {
                                    int num2 = num >> 24 & 255;
                                    int num3 = num >> 16 & 255;
                                    int num4 = num >> 8 & 255;
                                    int num5 = num & 255;
                                    if (num2 == 0)
                                    {
                                        binaryWriter.Write(0);
                                    }
                                    else
                                    {
                                        binaryWriter.Write((byte)num3);
                                        binaryWriter.Write((byte)num4);
                                        binaryWriter.Write((byte)num5);
                                        binaryWriter.Write((byte)num2);
                                    }
                                }
                                AddFile(Path.ChangeExtension(relPath, "rawimg"), memoryStream.ToArray());
                                return;
                            }
                        }
                    }
                }
            }

            AddFile(relPath, File.ReadAllBytes(filePath));
        }
        internal void Save()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (DeflateStream deflateStream = new DeflateStream(memoryStream, 0))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(deflateStream))
                    {
                        binaryWriter.Write(name);
                        binaryWriter.Write(version.ToString());
                        binaryWriter.Write(files.Count);
                        foreach (var keyValuePair in from e in files
                                                     orderby GetFileState(e.Key)
                                                     select e)
                        {
                            binaryWriter.Write(keyValuePair.Key);
                            binaryWriter.Write(keyValuePair.Value.Length);
                            binaryWriter.Write(keyValuePair.Value);
                        }
                    }
                }
                byte[] array = memoryStream.ToArray();
                this.hash = SHA1.Create().ComputeHash(array);
                using (FileStream fileStream = File.Create(this.path))
                {
                    using (BinaryWriter binaryWriter2 = new BinaryWriter(fileStream))
                    {
                        binaryWriter2.Write(Encoding.ASCII.GetBytes("TMOD"));
                        binaryWriter2.Write(new Version(0, 10, 1, 4).ToString());
                        binaryWriter2.Write(hash);
                        binaryWriter2.Write(signature);
                        binaryWriter2.Write(array.Length);
                        binaryWriter2.Write(array);
                    }
                }
            }
        }

    }

    internal class ModCompile
    {
        private static readonly string modCompileDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ModCompile");
        private static readonly Regex buildIgnore = new Regex(
            @"^\.git\\|^\.vs\\|^build\.txt$|^build\.json|^\.gitattributes$|^\.gitignore$|^bin\\|^obj\\|\.cs$|\.csproj$|\.sln$|^Thumbs\.db$|^Exclude\\", RegexOptions.Compiled);

        private static string[] moduleReferences =
        {
            @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Core\v4.0_4.0.0.0__b77a5c561934e089\System.Core.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_32\Microsoft.Xna.Framework\v4.0_4.0.0.0__842cf8be1de50553\Microsoft.Xna.Framework.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_32\Microsoft.Xna.Framework.Graphics\v4.0_4.0.0.0__842cf8be1de50553\Microsoft.Xna.Framework.Graphics.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_32\Microsoft.Xna.Framework.Game\v4.0_4.0.0.0__842cf8be1de50553\Microsoft.Xna.Framework.Game.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System\v4.0_4.0.0.0__b77a5c561934e089\System.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_32\Microsoft.Xna.Framework.Xact\v4.0_4.0.0.0__842cf8be1de50553\Microsoft.Xna.Framework.Xact.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Windows.Forms\v4.0_4.0.0.0__b77a5c561934e089\System.Windows.Forms.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\WindowsBase\v4.0_4.0.0.0__31bf3856ad364e35\WindowsBase.dll"
        };

        private static byte[] ReadIfExists(string path)
        {
            return File.Exists(path) ? File.ReadAllBytes(path) : null;
        }

        internal static string ModProjectFolder;
        private static bool Build(TmodFile mod)
        {
            byte[] winDLL = null;
            byte[] monoDLL = null;
            byte[] winPDB = null;
            //if (mod.properties.noCompile)
            //{
            //    winDLL = monoDLL = ReadIfExists(Path.Combine(mod.path, "All.dll"));
            //    winPDB = ReadIfExists(Path.Combine(mod.path, "All.pdb"));
            //
            //    if (winDLL == null)
            //    {
            //        winDLL = ReadIfExists(Path.Combine(mod.path, "Windows.dll"));
            //        monoDLL = ReadIfExists(Path.Combine(mod.path, "Mono.dll"));
            //        winPDB = ReadIfExists(Path.Combine(mod.path, "Windows.pdb"));
            //    }
            //
            //    if (winDLL == null || monoDLL == null)
            //    {
            //        ErrorLogger.LogDllBuildError(mod.path);
            //        return false;
            //    }
            //}
            //else
            //{
            CompileMod(mod, true, ref winDLL, ref winPDB);
            if (winDLL == null)
                return false;

            CompileMod(mod, false, ref monoDLL, ref winPDB);//the pdb reference won't actually be written to
            if (monoDLL == null)
                return false;
            //}

            if (!VerifyName(mod.name, winDLL) || !VerifyName(mod.name, monoDLL))
                return false;


            mod.AddFile("Info", mod.GetInfo());

            if (Equal(winDLL, monoDLL))
            {
                mod.AddFile("All.dll", winDLL);
                if (winPDB != null) mod.AddFile("All.pdb", winPDB);
            }
            else
            {
                mod.AddFile("Windows.dll", winDLL);
                mod.AddFile("Mono.dll", monoDLL);
                if (winPDB != null) mod.AddFile("Windows.pdb", winPDB);
            }

            foreach (var resource in Directory.GetFiles(ModProjectFolder, "*", SearchOption.AllDirectories))
            {
                var relPath = resource.Substring(ModProjectFolder.Length + 1);
                if (buildIgnore.IsMatch(relPath))
                    continue;

                mod.AddResource(relPath, resource);
            }

            mod.Save();
            ActivateExceptionReporting();
            return true;
        }

        private static bool exceptionReportingActive;
        internal static void ActivateExceptionReporting()
        {
            if (exceptionReportingActive) return;
            exceptionReportingActive = true;
            AppDomain.CurrentDomain.FirstChanceException += delegate (object sender, FirstChanceExceptionEventArgs exceptionArgs)
            {
                if (exceptionArgs.Exception.Source == "MP3Sharp") return;
                if (exceptionArgs.Exception.TargetSite.Name.StartsWith("doColors_Mode")) return;
                var stack = new System.Diagnostics.StackTrace(true);
            };
        }

        private static bool VerifyName(string modName, byte[] dll)
        {
            var asmDef = AssemblyDefinition.ReadAssembly(new MemoryStream(dll));
            var asmName = asmDef.Name.Name;
            if (asmName != modName)
            {
                Debug.WriteLine("tModLoader.BuildErrorModNameDoesntMatchAssemblyName");
                return false;
            }

            if (modName.Equals("Terraria", StringComparison.InvariantCultureIgnoreCase))
            {
                Debug.WriteLine(("tModLoader.BuildErrorModNamedTerraria"));
                return false;
            }

            // Verify that folder and namespace match up
            try
            {
                var modClassType = asmDef.MainModule.Types.Single(x => x.BaseType?.FullName == "Terraria.ModLoader.Mod");
                string topNamespace = modClassType.Namespace.Split('.')[0];
                if (topNamespace != modName)
                {
                    Debug.WriteLine(("tModLoader.BuildErrorNamespaceFolderDontMatch"));
                    return false;
                }
            }
            catch
            {
                Debug.WriteLine(("tModLoader.BuildErrorNoModClass"));
                return false;
            }

            return true;
        }

        private static bool Equal(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return false;

            return true;
        }

        private static void CompileMod(TmodFile mod, bool generatePDB,
                ref byte[] dll, ref byte[] pdb)
        {

            //collect all dll references
            var tempDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "My Games", "Terraria", "ModLoader", "compile_temp");
            Directory.CreateDirectory(tempDir);
            var refs = new List<string>();

            //everything used to compile the tModLoader for the target platform
            refs.AddRange(GetTerrariaReferences(tempDir, generatePDB));

            //libs added by the mod
            refs.AddRange(mod.dllReferences.Select(refDll => Path.Combine(ModProjectFolder, "lib/" + refDll + ".dll")));

            //all dlls included in all referenced mods
            //foreach (var refMod in refMods)
            //{
            //    var path = Path.Combine(tempDir, refMod + ".dll");
            //    File.WriteAllBytes(path, refMod.modFile.GetMainAssembly(forWindows));
            //    refs.Add(path);
            //
            //    foreach (var refDll in refMod.properties.dllReferences)
            //    {
            //        path = Path.Combine(tempDir, refDll + ".dll");
            //        File.WriteAllBytes(path, refMod.modFile.GetFile("lib/" + refDll + ".dll"));
            //        refs.Add(path);
            //    }
            //}

            var compileOptions = new CompilerParameters
            {
                OutputAssembly = Path.Combine(tempDir, mod + ".dll"),
                GenerateExecutable = false,
                GenerateInMemory = false,
                TempFiles = new TempFileCollection(tempDir, true),
                IncludeDebugInformation = generatePDB,
                CompilerOptions = "/optimize"
            };

            compileOptions.ReferencedAssemblies.AddRange(refs.ToArray());
            var files = Directory.GetFiles(ModProjectFolder, "*.cs", SearchOption.AllDirectories)
                .Where(f => !f.StartsWith("_")).ToArray();

            try
            {
                CompilerResults results;
                {
                    var options = new Dictionary<string, string> { { "CompilerVersion", "v7.0" } };
                    results = new CSharpCodeProvider(options).CompileAssemblyFromFile(compileOptions, files);
                }

                if (results.Errors.HasErrors)
                {
                    Debug.WriteLine("Compile Error: " + results.Errors[0]);
                    return;
                }

                dll = File.ReadAllBytes(compileOptions.OutputAssembly);
                dll = PostProcess(dll, generatePDB);
                if (generatePDB)
                    pdb = File.ReadAllBytes(Path.Combine(tempDir, mod + ".pdb"));
            }
            finally
            {
                int numTries = 10;
                while (numTries > 0)
                {
                    try
                    {
                        Directory.Delete(tempDir, true);
                        numTries = 0;
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(1);
                        numTries--;
                    }
                }
            }
        }

        private static IEnumerable<string> GetTerrariaReferences(string tempDir, bool forWindows)
        {
            var refs = new List<string>(moduleReferences);

            if (forWindows)
            {
                var terrariaModule = Assembly.GetExecutingAssembly();
                refs.Add(terrariaModule.Location);

                //extract embedded resource dlls
                foreach (var resName in terrariaModule.GetManifestResourceNames().Where(n => n.EndsWith(".dll")))
                {
                    var path = Path.Combine(tempDir, Path.GetFileName(resName));
                    using (Stream res = terrariaModule.GetManifestResourceStream(resName), file = File.Create(path))
                        res.CopyTo(file);

                    refs.Add(path);
                }

            }
            else
            {
                //remove framework references
                refs.RemoveAll(path =>
                {
                    var name = Path.GetFileName(path);
                    return name == "FNA.dll" || name.StartsWith("Microsoft.Xna.Framework");
                });
                //replace with ModCompile contents
                var mainModulePath = Path.Combine(modCompileDir, forWindows ? "tModLoaderWindows.exe" : "tModLoaderMac.exe");
                refs.Add(mainModulePath);

                var frameworkNames = forWindows
                    ? new[]
                    {
                        "Microsoft.Xna.Framework.dll",
                        "Microsoft.Xna.Framework.Game.dll",
                        "Microsoft.Xna.Framework.Graphics.dll",
                        "Microsoft.Xna.Framework.Xact.dll"
                    }
                    : new[]
                    {
                        "FNA.dll"
                    };
                refs.AddRange(frameworkNames.Select(f => Path.Combine(modCompileDir, f)));

                //extract embedded resource dlls
                var asm = AssemblyDefinition.ReadAssembly(mainModulePath);
                foreach (var res in asm.MainModule.Resources.OfType<EmbeddedResource>().Where(res => res.Name.EndsWith(".dll")))
                {
                    var path = Path.Combine(tempDir, Path.GetFileName(res.Name));
                    using (Stream s = res.GetResourceStream(), file = File.Create(path))
                        s.CopyTo(file);

                    refs.Add(path);
                }
            }

            return refs;
        }

        /// <summary>
        /// Invoke the Roslyn compiler via reflection to avoid a .NET 4.5 dependency
        /// </summary>
        private static CompilerResults RoslynCompile(CompilerParameters compileOptions, string[] files)
        {
            var terrariaDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var modCompileDir = Path.Combine(terrariaDir, "ModCompile");
            var asm = Assembly.LoadFile(Path.Combine(modCompileDir, "RoslynWrapper.dll"));

            AppDomain.CurrentDomain.AssemblyResolve += (o, args) =>
            {
                var name = new AssemblyName(args.Name).Name;
                var f = Path.Combine(modCompileDir, name + ".dll");
                return File.Exists(f) ? Assembly.LoadFile(f) : null;
            };

            var res = (CompilerResults)asm.GetType("Terraria.ModLoader.RoslynWrapper").GetMethod("Compile")
                .Invoke(null, new object[] { compileOptions, files });

            if (!res.Errors.HasErrors && compileOptions.IncludeDebugInformation)
                asm.GetType("Terraria.ModLoader.RoslynPdbFixer").GetMethod("Fix")
                    .Invoke(null, new object[] { compileOptions.OutputAssembly });

            return res;
        }

        private static FileStream AcquireConsoleBuildLock()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/ModCompile/buildlock";
            bool first = true;
            while (true)
            {
                try
                {
                    return new FileStream(path, FileMode.OpenOrCreate);
                }
                catch (IOException)
                {
                    if (first)
                    {
                        Console.WriteLine("Waiting for other builds to complete");
                        first = false;
                    }
                }
            }
        }

        private static AssemblyNameReference GetOrAddSystemCore(ModuleDefinition module)
        {
            var assemblyRef = module.AssemblyReferences.SingleOrDefault(r => r.Name == "System.Core");
            if (assemblyRef == null)
            {
                //System.Linq.Enumerable is in System.Core
                var name = Assembly.GetAssembly(typeof(Enumerable)).GetName();
                assemblyRef = new AssemblyNameReference(name.Name, name.Version)
                {
                    Culture = name.CultureInfo.Name,
                    PublicKeyToken = name.GetPublicKeyToken(),
                    HashAlgorithm = (AssemblyHashAlgorithm)name.HashAlgorithm
                };
                module.AssemblyReferences.Add(assemblyRef);
            }
            return assemblyRef;
        }

        private static byte[] PostProcess(byte[] dll, bool forWindows)
        {
            if (forWindows)
                return dll;

            var asm = AssemblyDefinition.ReadAssembly(new MemoryStream(dll));

            // Extension methods are marked with an attribute which is located in mscorlib on .NET but in System.Core on Mono
            // Find all extension attributes and change their assembly references
            AssemblyNameReference SystemCoreRef = null;
            foreach (var module in asm.Modules)
                foreach (var type in module.Types)
                    foreach (var met in type.Methods)
                        foreach (var attr in met.CustomAttributes)
                            if (attr.AttributeType.FullName == "System.Runtime.CompilerServices.ExtensionAttribute")
                                attr.AttributeType.Scope = SystemCoreRef ?? (SystemCoreRef = GetOrAddSystemCore(module));

            var ret = new MemoryStream();
            asm.Write(ret);
            return ret.ToArray();
        }
    }
}
