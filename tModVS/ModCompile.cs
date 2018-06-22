using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zlib;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Shell.Interop;
using Mono.Cecil;
using Newtonsoft.Json;
using AssemblyDef = Mono.Cecil.AssemblyDefinition;

namespace tModVS
{
    internal class TmodProp
    {
        public Version tModLoaderVersion = new Version(0, 10, 1, 4);
        public string name;
        public string author;
        public Version version;
        public string[] modReferences;
        public string[] embedResource;
        public string[] weakReferences;
        public string[] sortAfter;
        public string[] sortBefore;
        public string displayName;
        public string homepage;
        public string description;
        public bool editAndContinue;
        public bool includePdb;
        public byte side = 0;
        public bool allowUnsafe;
        public bool debugBuild;
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
                using (FileStream fileStream = File.Create(Path.Combine(ModCompile.ModProjectFolder, prop.displayName + ".tmod")))
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
    internal static class ModCompile
    {
        private static readonly Regex BuildIgnore = new Regex(
            @"^\.git\\|^\.vs\\|^build\.txt$|^build\.json|^\.gitattributes$|^\.gitignore$|^bin\\|^obj\\|\.cs$|\.csproj$|\.sln$|^Thumbs\.db$|^Exclude\\|\.csproj\.user$|^_|^\.|^References\\", RegexOptions.Compiled);
        private static readonly string[] ModuleReferences =
        {
            @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Core\v4.0_4.0.0.0__b77a5c561934e089\System.Core.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System\v4.0_4.0.0.0__b77a5c561934e089\System.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Windows.Forms\v4.0_4.0.0.0__b77a5c561934e089\System.Windows.Forms.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll",
            @"C:\WINDOWS\Microsoft.Net\assembly\GAC_MSIL\WindowsBase\v4.0_4.0.0.0__31bf3856ad364e35\WindowsBase.dll"
        };
        internal static string ModProjectFolder;
        internal static void Build()
        {
            TmodFile mod = new TmodFile();
            mod.prop = JsonConvert.DeserializeObject<TmodProp>(
                File.ReadAllText(Path.Combine(ModProjectFolder, "build.json")));
            CompileMod(mod.prop, true, out var winDLL, out var pdb);
            if (winDLL == null)
            {
                tModBuild.ShowMsg("Windows平台编译结果为null。", "Compile for Windows fail: result is null"
                    , "tModVS", OLEMSGICON.OLEMSGICON_CRITICAL, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }
            if (winDLL.Length == 0)
            {
                tModBuild.ShowMsg("Windows平台编译结果为空byte[]。", "Compile for Windows fail: result is empty byte[]"
                    , "tModVS", OLEMSGICON.OLEMSGICON_CRITICAL, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }
            if (!VerifyName(mod.prop.name, winDLL))
            {
                tModBuild.ShowMsg("Windows平台Mod名称校验失败。", "Fail to verify Mod name of Windows build"
                    , "tModVS", OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }
            CompileMod(mod.prop, false, out var monoDLL, out _);
            if (monoDLL == null)
            {
                tModBuild.ShowMsg("Mono平台编译结果为空byte[]。", "Compile for Mono fail: result is empty byte[]"
                    , "tModVS", OLEMSGICON.OLEMSGICON_CRITICAL, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }
            if (monoDLL.Length == 0)
            {
                tModBuild.ShowMsg("Mono平台编译结果为空byte[]。", "Compile for Mono fail: result is empty byte[]"
                    , "tModVS", OLEMSGICON.OLEMSGICON_CRITICAL, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }
            if (!VerifyName(mod.prop.name, monoDLL))
            {
                tModBuild.ShowMsg("Mono平台Mod名称校验失败。", "Fail to verify Mod name of Mono build"
                    , "tModVS", OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }
            mod.AddFile("Info", mod.GetInfo());
            // They will never be equal I guess
            mod.AddFile("Windows.dll", winDLL);
            if (mod.prop.includePdb)
            {
                mod.AddFile("Windows.pdb", pdb);
            }
            mod.AddFile("Mono.dll", monoDLL);
            foreach (var resource in Directory.GetFiles(ModProjectFolder, "*", SearchOption.AllDirectories))
            {
                var relPath = resource.Substring(ModProjectFolder.Length + 1);
                if (BuildIgnore.IsMatch(relPath))
                {
                    continue;
                }
                mod.AddResource(relPath, resource);
            }
            mod.Save();
            tModBuild.ShowMsg("Mod打包完毕。", "Mod build finish."
                , "tModVS", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
        private static bool VerifyName(string modName, byte[] dll)
        {
            var asmDef = AssemblyDef.ReadAssembly(new MemoryStream(dll));
            if (modName.Equals("Terraria", StringComparison.InvariantCultureIgnoreCase))
            {
                tModBuild.ShowMsg("Mod名称为Terraria", "Mod named Terraria"
                    , "tModVS", OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return false;
            }
            try
            {
                var modClassType = asmDef.MainModule.Types.Single(x => x.BaseType?.FullName == "Terraria.ModLoader.Mod");
                string topNamespace = modClassType.Namespace.Split('.')[0];
                if (topNamespace != modName)
                {
                    tModBuild.ShowMsg("Mod所在的命名空间和Mod名称不同", "Mod namespace not match its name"
                        , "tModVS", OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                    return false;
                }
            }
            catch
            {
                tModBuild.ShowMsg("没有找到继承Terraria.ModLoader.Mod的类", "No Terraria.ModLoader.Mod class found"
                    , "tModVS", OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return false;
            }

            return true;
        }
        public static List<string> RefItems = new List<string>();

        private static void CompileMod(TmodProp prop, bool win, out byte[] dll, out byte[] pdb)
        {
            RefItems.AddRange(ModuleReferences);
            RefItems.RemoveAll(path =>
            {
                var name = Path.GetFileNameWithoutExtension(path);
                return name == "Terraria" || name.StartsWith("tModLoader") || name == "FNA" || name.StartsWith("Microsoft.Xna.Framework");
            });
            if (win)
            {
                RefItems.Add(Path.Combine(ModProjectFolder, "References", "Microsoft.Xna.Framework.dll"));
                RefItems.Add(Path.Combine(ModProjectFolder, "References", "Microsoft.Xna.Framework.Game.dll"));
                RefItems.Add(Path.Combine(ModProjectFolder, "References", "Microsoft.Xna.Framework.Graphics.dll"));
                RefItems.Add(Path.Combine(ModProjectFolder, "References", "Microsoft.Xna.Framework.Xact.dll"));
            }
            else
            {
                RefItems.Add(Path.Combine(ModProjectFolder, "References", "FNA.dll"));
            }
            var terrariaPath = Path.Combine(ModProjectFolder, "References",
                win ? "tModLoaderWindows.exe" : "tModLoaderMac.exe");
            RefItems.Add(terrariaPath);
            var asm = AssemblyDef.ReadAssembly(terrariaPath);
            foreach (var res in asm.MainModule.Resources.OfType<EmbeddedResource>()
                .Where(res => res.Name.EndsWith(".dll")))
            {
                var path = Path.Combine(ModProjectFolder, "References", "Embedded", Path.GetFileName(res.Name));
                using (Stream s1 = res.GetResourceStream(), file = File.Create(path))
                {
                    s1.CopyTo(file);
                }
                RefItems.Add(path);
            }
            dll = pdb = new byte[0];
            var tempDir = Path.Combine(ModProjectFolder, "compile_temp");
            Directory.CreateDirectory(tempDir);
            var files = Directory.GetFiles(ModProjectFolder, "*.cs", SearchOption.AllDirectories)
                .Where(f => !f.StartsWith("_")).ToArray();
            try
            {
                var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, null, null, null, null,
                    prop.debugBuild ? OptimizationLevel.Debug : OptimizationLevel.Release, false, prop.allowUnsafe,
                    null, null, default(ImmutableArray<byte>), null, Platform.AnyCpu, ReportDiagnostic.Default, 4,
                    null, true, false, null, null, null, DesktopAssemblyIdentityComparer.Default, null);
                var peRef = from string s in RefItems.Distinct()
                    select MetadataReference.CreateFromFile(s);
                var synTree = from f in files
                    select SyntaxFactory.ParseSyntaxTree(File.ReadAllText(f), null, f, Encoding.UTF8);
                var dll1 = new MemoryStream();
                var pdb1 = new MemoryStream();
                EmitResult emitResult = CSharpCompilation.Create(prop.name, synTree, peRef, options)
                    .Emit(dll1, pdb1, manifestResources: prop.embedResource.Select(item =>
                        new ResourceDescription(item, () =>
                                File.OpenRead(Path.Combine(ModProjectFolder, item)),
                            false)));
                var compilerResults = new CompilerResults(new TempFileCollection(tempDir, true));
                foreach (Diagnostic diagnostic in emitResult.Diagnostics)
                {
                    if (diagnostic.Severity == DiagnosticSeverity.Error)
                    {
                        FileLinePositionSpan lineSpan = diagnostic.Location.GetLineSpan();
                        LinePosition startLinePosition = lineSpan.StartLinePosition;
                        compilerResults.Errors.Add(new CompilerError(lineSpan.Path, startLinePosition.Line, startLinePosition.Character, diagnostic.Id, diagnostic.GetMessage(null)));
                    }
                }
                if (compilerResults.Errors.HasErrors)
                {
                    var cer = string.Join("\r\n", compilerResults.Errors.Cast<string>());
                    tModBuild.ShowMsg("编译错误:\r\n" + cer, "Compile error:\r\n" + cer
                        , "tModVS", OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                    return;
                }
                pdb = pdb1.ToArray();
                if (win)
                {
                    dll = dll1.ToArray();
                }
                else
                {
                    var monoAsm = AssemblyDef.ReadAssembly(new MemoryStream(dll));
                    AssemblyNameReference systemCoreRef = null;
                    foreach (var module in monoAsm.Modules)
                    {
                        foreach (var type in module.Types)
                        {
                            foreach (var met in type.Methods)
                            {
                                foreach (var attr in met.CustomAttributes)
                                {
                                    if (attr.AttributeType.FullName == "System.Runtime.CompilerServices.ExtensionAttribute")
                                    {
                                        var assemblyRef = module.AssemblyReferences.SingleOrDefault(r => r.Name == "System.Core");
                                        if (assemblyRef == null)
                                        {
                                            var name = Assembly.GetAssembly(typeof(Enumerable)).GetName();
                                            assemblyRef = new AssemblyNameReference(name.Name, name.Version)
                                            {
                                                Culture = name.CultureInfo.Name,
                                                PublicKeyToken = name.GetPublicKeyToken(),
                                                HashAlgorithm = (Mono.Cecil.AssemblyHashAlgorithm)name.HashAlgorithm
                                            };
                                            module.AssemblyReferences.Add(assemblyRef);
                                        }

                                        attr.AttributeType.Scope =
                                            systemCoreRef ?? (systemCoreRef = assemblyRef);
                                    }
                                }
                            }
                        }
                    }

                    var ret = new MemoryStream();
                    monoAsm.Write(ret);
                    dll = ret.ToArray();
                }
                File.WriteAllBytes(Path.Combine(tempDir, prop.name + ".dll"), dll);
                File.WriteAllBytes(Path.Combine(tempDir, prop.name + ".pdb"), pdb);
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
    }
}
