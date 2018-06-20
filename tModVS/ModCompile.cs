//using AssemblyHashAlgorithm = Mono.Cecil.AssemblyHashAlgorithm;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using Ionic.Zlib;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json;
//using Mono.Cecil;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using CustomAttribute = dnlib.DotNet.CustomAttribute;

namespace tModVS
{
    internal class TmodProp
    {
        public Version tModLoaderVersion = new Version(0, 10, 1, 4);
        public string name;
        public string author;
        public Version version;
        public string[] dllReferences;
        public string[] modReferences;
        public string[] weakReferences;
        public string[] sortAfter;
        public string[] sortBefore;
        public string displayName;
        public string homepage;
        public string description;
        public bool editAndContinue;
        public byte side = 0;
        public string compileoption;
    }
    internal class TmodFile
    {
        public Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
        public TmodProp prop;
        public byte[] hash;
        public byte[] signature = new byte[256];
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
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    if (prop.dllReferences.Length != 0)
                    {
                        binaryWriter.Write("dllReferences");
                        foreach (var t in prop.dllReferences)
                        {
                            binaryWriter.Write(t);
                        }
                        binaryWriter.Write("");
                    }
                    if (prop.modReferences.Length != 0)
                    {
                        binaryWriter.Write("modReferences");
                        foreach (var t in prop.modReferences)
                        {
                            binaryWriter.Write(t);
                        }
                        binaryWriter.Write("");
                    }
                    if (prop.weakReferences.Length != 0)
                    {
                        binaryWriter.Write("weakReferences");
                        foreach (var t in prop.weakReferences)
                        {
                            binaryWriter.Write(t);
                        }
                        binaryWriter.Write("");
                    }
                    if (prop.sortAfter.Length != 0)
                    {
                        binaryWriter.Write("sortAfter");
                        foreach (var t in prop.sortAfter)
                        {
                            binaryWriter.Write(t);
                        }
                        binaryWriter.Write("");
                    }
                    if (prop.sortBefore.Length != 0)
                    {
                        binaryWriter.Write("sortBefore");
                        foreach (var t in prop.sortBefore)
                        {
                            binaryWriter.Write(t);
                        }
                        binaryWriter.Write("");
                    }
                    if (prop.author.Length > 0)
                    {
                        binaryWriter.Write("author");
                        binaryWriter.Write(prop.author);
                    }
                    binaryWriter.Write("version");
                    binaryWriter.Write(prop.version.ToString());
                    if (prop.displayName.Length > 0)
                    {
                        binaryWriter.Write("displayName");
                        binaryWriter.Write(prop.displayName);
                    }
                    if (prop.homepage.Length > 0)
                    {
                        binaryWriter.Write("homepage");
                        binaryWriter.Write(prop.homepage);
                    }
                    if (prop.description.Length > 0)
                    {
                        binaryWriter.Write("description");
                        binaryWriter.Write(prop.description);
                    }
                    if (prop.editAndContinue)
                    {
                        binaryWriter.Write("editAndContinue");
                    }
                    if (prop.side != 0)
                    {
                        binaryWriter.Write("side");
                        binaryWriter.Write(prop.side);
                    }
                    binaryWriter.Write("");
                }
                return memoryStream.ToArray();
            }
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
                        binaryWriter.Write(prop.name);
                        binaryWriter.Write(prop.version.ToString());
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
                hash = SHA1.Create().ComputeHash(array);
                using (FileStream fileStream = File.Create(Path.Combine(ModCompile.ModProjectFolder, "build.tmod")))
                {
                    using (BinaryWriter binaryWriter2 = new BinaryWriter(fileStream))
                    {
                        binaryWriter2.Write(Encoding.ASCII.GetBytes("TMOD"));
                        binaryWriter2.Write(prop.tModLoaderVersion.ToString());
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
        private static readonly Regex buildIgnore = new Regex(
            @"^\.git\\|^\.vs\\|^build\.txt$|^build\.json|^\.gitattributes$|^\.gitignore$|^bin\\|^obj\\|\.cs$|\.csproj$|\.sln$|^Thumbs\.db$|^Exclude\\", RegexOptions.Compiled);

        private static readonly string[] moduleReferences =
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
        internal static string ModProjectFolder;
        internal static bool Build()
        {
            byte[] winDLL = null;
            byte[] monoDLL = null;
            TmodFile mod = new TmodFile();
            mod.prop = JsonConvert.DeserializeObject<TmodProp>(
                File.ReadAllText(Path.Combine(ModProjectFolder, "build.json")));
            CompileMod(mod.prop, true, ref winDLL);
            if (winDLL == null)
            {
                Debug.WriteLine("Win dll == null");
                return false;
            }
            CompileMod(mod.prop, false, ref monoDLL);
            if (monoDLL == null)
            {
                Debug.WriteLine("Mono dll == null");
                return false;
            }
            if (!VerifyName(mod.prop.name, winDLL) || !VerifyName(mod.prop.name, monoDLL))
            {
                Debug.WriteLine("Verify name fail");
                return false;
            }
            mod.AddFile("Info", mod.GetInfo());
            if (Equal(winDLL, monoDLL))
            {
                mod.AddFile("All.dll", winDLL);
            }
            else
            {
                mod.AddFile("Windows.dll", winDLL);
                mod.AddFile("Mono.dll", monoDLL);
            }
            foreach (var resource in Directory.GetFiles(ModProjectFolder, "*", SearchOption.AllDirectories))
            {
                var relPath = resource.Substring(ModProjectFolder.Length + 1);
                if (buildIgnore.IsMatch(relPath))
                {
                    continue;
                }
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
                if (exceptionArgs.Exception.Source == "MP3Sharp")
                {
                    return;
                }
                if (exceptionArgs.Exception.TargetSite.Name.StartsWith("doColors_Mode"))
                {
                    return;
                }
                var stack = new System.Diagnostics.StackTrace(true);
                Debug.WriteLine(stack);
            };
        }
        private static bool VerifyName(string modName, byte[] dll)
        {
            var asmDef = dnlib.DotNet.AssemblyDef.Load(new MemoryStream(dll));
            var asmName = asmDef.Name;
            if (asmName != modName)
            {
                Debug.WriteLine("tModLoader.BuildErrorModNameDoesntMatchAssemblyName");
                return false;
            }

            if (modName.Equals("Terraria", StringComparison.InvariantCultureIgnoreCase))
            {
                Debug.WriteLine("tModLoader.BuildErrorModNamedTerraria");
                return false;
            }

            // Verify that folder and namespace match up
            try
            {
                var modClassType = asmDef.ManifestModule.Types.Single(x => x.BaseType?.FullName == "Terraria.ModLoader.Mod");
                string topNamespace = modClassType.Namespace.String.Split('.')[0];
                if (topNamespace != modName)
                {
                    Debug.WriteLine("tModLoader.BuildErrorNamespaceFolderDontMatch");
                    return false;
                }
            }
            catch
            {
                Debug.WriteLine("tModLoader.BuildErrorNoModClass");
                return false;
            }

            return true;
        }
        private static bool Equal(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
        public static List<string> refItems = new List<string>();
        private static void CompileMod(TmodProp prop, bool win, ref byte[] dll)
        {
            //collect all dll references
            var tempDir = Path.Combine(ModProjectFolder, "compile_temp");
            Directory.CreateDirectory(tempDir);
            //everything used to compile the tModLoader for the target platform

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
                OutputAssembly = Path.Combine(tempDir, prop.name + ".dll"),
                GenerateExecutable = false,
                GenerateInMemory = false,
                TempFiles = new TempFileCollection(tempDir, true),
                IncludeDebugInformation = true,
                CompilerOptions = prop.compileoption
            };
            GetTerrariaReferences(win);
            compileOptions.ReferencedAssemblies.AddRange(refItems.ToArray());
            var files = Directory.GetFiles(ModProjectFolder, "*.cs", SearchOption.AllDirectories)
                .Where(f => !f.StartsWith("_")).ToArray();

            try
            {
                var (results, dat) = Compile(compileOptions, files);

                if (results.Errors.HasErrors)
                {
                    Debug.WriteLine("Compile Error: " + results.Errors[0]);
                    return;
                }
                // FIXME: move AsmRef of ExtensionAttribute to System.Core.dll for Mono
                // But why...
                // Using dnlib to avoid Pdb problem but I can not change Scope...
                dll = PostProcess(dat, win);
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
        private static void GetTerrariaReferences(bool forWindows)
        {
            var mainModulePath = Path.Combine(ModProjectFolder, "References",
                forWindows ? "tModLoaderWindows.exe" : "tModLoaderMac.exe");
            refItems.AddRange(moduleReferences);
            refItems.RemoveAll(path =>
            {
                var name = Path.GetFileNameWithoutExtension(path);
                return name == "Terraria" || name.StartsWith("tModLoader");
            });
            if (!forWindows)
            {
                refItems.RemoveAll(path =>
                {
                    var name = Path.GetFileName(path);
                    return name == "FNA.dll" || name.StartsWith("Microsoft.Xna.Framework");
                });
                refItems.Add(Path.Combine(ModProjectFolder, "References", "FNA.dll"));
            }

            refItems.Add(mainModulePath);
            var asm = AssemblyDef.Load(mainModulePath);
            foreach (var res in asm.ManifestModule.Resources.OfType<EmbeddedResource>().Where(res => res.Name.EndsWith(".dll")))
            {
                var path = Path.Combine(ModProjectFolder, "References", "Embedded", Path.GetFileName(res.Name));
                using (Stream s = res.CreateReader().AsStream(), file = File.Create(path))
                {
                    s.CopyTo(file);
                }
                refItems.Add(path);
            }
        }
        public static (CompilerResults, byte[]) Compile(CompilerParameters args, string[] files)
        {
            string name = Path.GetFileNameWithoutExtension(args.OutputAssembly);
            CSharpCompilationOptions options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, null, null, null, null, OptimizationLevel.Debug, false, false, null, null, default(ImmutableArray<byte>), null, Platform.AnyCpu, ReportDiagnostic.Default, 4, null, true, false, null, null, null, null, null).WithOptimizationLevel(OptimizationLevel.Debug).WithAssemblyIdentityComparer(DesktopAssemblyIdentityComparer.Default);
            IEnumerable<PortableExecutableReference> references = from string s in args.ReferencedAssemblies
                                                                  select MetadataReference.CreateFromFile(s);
            IEnumerable<SyntaxTree> syntaxTrees = from f in files
                                                  select SyntaxFactory.ParseSyntaxTree(File.ReadAllText(f), null, f, Encoding.UTF8);
            MemoryStream ms = new MemoryStream();
            EmitResult emitResult = CSharpCompilation.Create(name, syntaxTrees, references, options).Emit(ms);
            CompilerResults compilerResults = new CompilerResults(args.TempFiles);
            foreach (Diagnostic diagnostic in emitResult.Diagnostics)
            {
                if (diagnostic.Severity == DiagnosticSeverity.Error)
                {
                    FileLinePositionSpan lineSpan = diagnostic.Location.GetLineSpan();
                    LinePosition startLinePosition = lineSpan.StartLinePosition;
                    compilerResults.Errors.Add(new CompilerError(lineSpan.Path, startLinePosition.Line, startLinePosition.Character, diagnostic.Id, diagnostic.GetMessage(null)));
                }
            }
            return (compilerResults, ms.ToArray());
        }
        //private static AssemblyNameReference GetOrAddSystemCore(AssemblyDef asm)
        //{
        //    var assemblyRef = asm.ManifestModule.GetAssemblyRefs().SingleOrDefault(r => r.Name == "System.Core");
        //    if (assemblyRef == null)
        //    {
        //        Importer importer = new Importer(asm.ManifestModule);
        //        ITypeDefOrRef ienum = importer.Import(typeof(Enumerable));
        //        IMethod writeLine = importer.Import(typeof(System.Console).GetMethod("WriteLine"));
        //        asm.ManifestModule.MDToken
        //    }
        //    return assemblyRef;
        //}
        private static byte[] PostProcess(byte[] dll, bool forWindows)
        {
            if (forWindows)
            {
                return dll;
            }

            var asm = AssemblyDef.Load(new MemoryStream(dll));
            Importer importer = new Importer(asm.ManifestModule);
            importer.Import(typeof(ExtensionAttribute));
            var imethod = (ICustomAttributeType) importer.Import(typeof(ExtensionAttribute).TypeInitializer);
            foreach (var module in asm.Modules)
            {
                foreach (var type in module.Types)
                {
                    foreach (var met in type.Methods)
                    {
                        for (var index = 0; index < met.CustomAttributes.Count; index++)
                        {
                            if (met.CustomAttributes[index].AttributeType.FullName == "System.Runtime.CompilerServices.ExtensionAttribute")
                            {
                                met.CustomAttributes[index] = new CustomAttribute(imethod);
                                //attr.AttributeType.Scope = SystemCoreRef ?? (SystemCoreRef = GetOrAddSystemCore(module));
                            }
                        }
                    }
                }
            }

            var ret = new MemoryStream();
            asm.Write(ret);
            return ret.ToArray();
        }
    }
}
