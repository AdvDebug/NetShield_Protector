using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Writer;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NetShield_Protector
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private string RandomPassword(int PasswordLength)
        {
            StringBuilder MakePassword = new StringBuilder();
            Random MakeRandom = new Random();
            while (0 < PasswordLength--)
            {
                string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ*!@=&?&/abcdefghijklmnopqrstuvwxyz1234567890";
                MakePassword.Append(characters[MakeRandom.Next(characters.Length)]);
            }
            return MakePassword.ToString();
        }

        private string RandomName(int NameLength)
        {
            StringBuilder MakePassword = new StringBuilder();
            Random MakeRandom = new Random();
            while (0 < NameLength--)
            {
                string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                MakePassword.Append(characters[MakeRandom.Next(characters.Length)]);
            }
            return MakePassword.ToString();
        }

        public static string RandomChineseCharacters(int NameLength)
        {
            const string chars = "的是有为也而要你可生家发如成起经";
            return new string(Enumerable.Repeat(chars, NameLength)
                .Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray());
        }

        private string RandomChineseCharacters_SameName(int NameLength)
        {
            StringBuilder MakePassword = new StringBuilder();
            Random MakeRandom = new Random();
            while (0 < NameLength--)
            {
                string characters = "的是有为也而要你可生家发如成起经";
                MakePassword.Append(characters[MakeRandom.Next(characters.Length)]);
            }
            return MakePassword.ToString();
        }

        public class Block { public Block() { Instructions = new List<Instruction>(); } public List<Instruction> Instructions { get; set; } public int Number { get; set; } }

        public static List<Block> GetMethod(MethodDef method) { var blocks = new List<Block>(); var block = new Block(); var id = 0; var usage = 0; block.Number = id; block.Instructions.Add(Instruction.Create(OpCodes.Nop)); blocks.Add(block); block = new Block(); var handlers = new Stack<ExceptionHandler>(); foreach (var instruction in method.Body.Instructions) { foreach (var eh in method.Body.ExceptionHandlers) { if (eh.HandlerStart == instruction || eh.TryStart == instruction || eh.FilterStart == instruction) handlers.Push(eh); } foreach (var eh in method.Body.ExceptionHandlers) { if (eh.HandlerEnd == instruction || eh.TryEnd == instruction) handlers.Pop(); } instruction.CalculateStackUsage(out var stacks, out var pops); block.Instructions.Add(instruction); usage += stacks - pops; if (stacks == 0) { if (instruction.OpCode != OpCodes.Nop) { if ((usage == 0 || instruction.OpCode == OpCodes.Ret) && handlers.Count == 0) { block.Number = ++id; blocks.Add(block); block = new Block(); } } } } return blocks; }

        private string XOREncryptionKeys(string KeyToEncrypt, string Key)
        {
            StringBuilder DecryptEncryptionKey = new StringBuilder();
            for (int c = 0; c < KeyToEncrypt.Length; c++)
                DecryptEncryptionKey.Append((char)((uint)KeyToEncrypt[c] ^ (uint)Key[c % 4]));
            return DecryptEncryptionKey.ToString();
        }

        private void PackAndEncrypt(string FileToPack, string Output)
        {
            var Options = new Dictionary<string, string>();
            Options.Add("CompilerVersion", "v4.0");
            Options.Add("language", "c#");
            var codeProvider = new CSharpCodeProvider(Options);
            CompilerParameters parameters = new CompilerParameters();
            parameters.CompilerOptions = "/target:winexe /unsafe";
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            parameters.IncludeDebugInformation = false;
            parameters.TreatWarningsAsErrors = false;
            string[] Librarys = { "System", "System.Windows.Forms", "System.Management", "System.Net", "System.Core", "System.Net.Http", "System.Runtime", "System.Runtime.InteropServices" };
            foreach (string Library in Librarys)
            {
                parameters.ReferencedAssemblies.Add(Library + ".dll");
            }
            byte[] CodeToProtect = File.ReadAllBytes(FileToPack);
            string RandomIV = RandomName(16);
            string RandomKey = RandomPassword(17);
            string RandomXORKey = RandomPassword(4);
            string EncryptedKey = XOREncryptionKeys(RandomKey, RandomXORKey);
            string EncryptedIV = XOREncryptionKeys(RandomIV, RandomXORKey);
            string Final = Encryption.AesTextEncryption(Convert.ToBase64String(CodeToProtect).Replace("A", ".").Replace("B", "*").Replace("S", @"_"), EncryptedKey, EncryptedIV);
            string PackStub = Resource1.PackStub;
            string NewPackStub = PackStub.Replace("DecME", Final).Replace("THISISIV", RandomIV).Replace("THISISKEY", RandomKey);
            string TotallyNewPackStub = NewPackStub.Replace("decryptkeyencryption", Convert.ToBase64String(Encoding.UTF8.GetBytes(RandomXORKey))).Replace("decryptkeyiv", Convert.ToBase64String(Encoding.UTF8.GetBytes(RandomXORKey))).Replace("PackStub", "namespace " + RandomName(12));
            CompilerResults cr = codeProvider.CompileAssemblyFromSource(parameters, TotallyNewPackStub);
            if (cr.Errors.Count > 0)
            {
                foreach (CompilerError ce in cr.Errors)
                {
                    MessageBox.Show("Errors building: " + ce.ErrorText + ", in line: " + ce.Line);
                }
            }
        }

        private void ObfuscasteCode(string ToProtect)
        {
            byte[] AssemblyToProtect = File.ReadAllBytes(ToProtect);
            ModuleContext ModuleCont = ModuleDefMD.CreateModuleContext();
            ModuleDefMD FileModule = ModuleDefMD.Load(AssemblyToProtect, ModuleCont);
            AssemblyDef Assembly1 = FileModule.Assembly;

            if (AntiDe4DotCheckBox.Checked)
            {
                for (int i = 200; i < 300; i++)
                {
                    InterfaceImpl Interface = new InterfaceImplUser(FileModule.GlobalType);
                    TypeDef typedef = new TypeDefUser("", $"Form{i.ToString()}", FileModule.CorLibTypes.GetTypeRef("System", "Attribute"));
                    InterfaceImpl interface1 = new InterfaceImplUser(typedef);
                    FileModule.Types.Add(typedef);
                    typedef.Interfaces.Add(interface1);
                    typedef.Interfaces.Add(Interface);
                }
            }

            string[] FakeObfuscastionsAttributes = { "ConfusedByAttribute", "YanoAttribute", "NetGuard", "DotfuscatorAttribute", "BabelAttribute", "ObfuscatedByGoliath", "dotNetProtector" };
            if (FakeObfuscatorAttributesCheckBox.Checked)
            {
                for (int i = 0; i < FakeObfuscastionsAttributes.Length; i++)
                {
                    var FakeObfuscastionsAttribute = new TypeDefUser(FakeObfuscastionsAttributes[i], FileModule.CorLibTypes.Object.TypeDefOrRef);
                    FileModule.Types.Add(FakeObfuscastionsAttribute);
                }
            }

            if (RenamingCheckBox.Checked)
            {
                foreach (TypeDef type in FileModule.Types)
                {
                    FileModule.Name = RandomName(12);
                    if (type.IsGlobalModuleType || type.IsRuntimeSpecialName || type.IsSpecialName || type.IsWindowsRuntime || type.IsInterface)
                    {
                        continue;
                    }
                    else
                    {
                        for (int i = 200; i < 300; i++)
                        {
                            foreach (PropertyDef property in type.Properties)
                            {
                                if (property.IsRuntimeSpecialName) continue;
                                property.Name = RandomName(20) + i + RandomName(10) + i;
                            }
                            foreach (FieldDef fields in type.Fields)
                            {
                                fields.Name = RandomName(20) + i + RandomName(10) + i;
                            }
                            foreach (EventDef eventdef in type.Events)
                            {
                                eventdef.Name = RandomName(20) + i + RandomName(10) + i;
                            }
                            foreach (MethodDef method in type.Methods)
                            {
                                if (method.IsConstructor || method.IsRuntimeSpecialName || method.IsRuntime || method.IsStaticConstructor || method.IsVirtual) continue;
                                method.Name = RandomName(20) + i + RandomName(10) + i;
                            }
                            foreach (MethodDef method in type.Methods)
                            {
                                foreach (Parameter RenameParameters in method.Parameters)
                                {
                                    RenameParameters.Name = RandomName(10);
                                }
                            }
                        }
                    }
                    foreach (ModuleDefMD module in FileModule.Assembly.Modules)
                    {
                        module.Name = RandomName(13);
                        module.Assembly.Name = RandomName(14);
                    }
                }

                foreach (TypeDef type in FileModule.Types)
                {
                    foreach (MethodDef GetMethods in type.Methods)
                    {
                        for (int i = 200; i < 300; i++)
                        {
                            if (GetMethods.IsConstructor || GetMethods.IsRuntimeSpecialName || GetMethods.IsRuntime || GetMethods.IsStaticConstructor) continue;
                            GetMethods.Name = RandomName(15) + i;
                        }
                    }
                }

                Dictionary<string, string> AssemblyNames = new Dictionary<string, string>();
                foreach (var type in FileModule.GetTypes())
                {
                    if (type.IsGlobalModuleType) continue;
                    if (type.Namespace == "")
                        continue;
                    if (AssemblyNames.TryGetValue(type.Namespace, out var nameValue))
                        type.Namespace = nameValue;
                    else
                    {
                        string newName = null;
                        for (int i = 0; i < 200; i++)
                        {
                            newName = RandomName(11) + RandomName(3) + RandomName(11) + RandomName(11) + i;
                        }
                        AssemblyNames.Add(type.Namespace, newName);
                        type.Namespace = newName;
                    }
                }
                foreach (var type in FileModule.GetTypes())
                {
                    if (type.IsGlobalModuleType) continue;

                    if (type.Name == "GeneratedInternalTypeHelper" || type.Name == "Resources" || type.Name == "Settings")
                        continue;
                    if (AssemblyNames.TryGetValue(type.Name, out var nameValue))
                        type.Name = nameValue;
                    else
                    {
                        string newName = null;
                        for (int i = 0; i < 200; i++)
                        {
                            newName = RandomName(11) + RandomName(3) + RandomName(11) + RandomName(11) + i;
                        }
                        AssemblyNames.Add(type.Name, newName);
                        type.Name = newName;
                    }
                }
                foreach (var resource in FileModule.Resources)
                {
                    foreach (var item in AssemblyNames.Where(item => resource.Name.Contains(item.Key)))
                    {
                        resource.Name = resource.Name.Replace(item.Key, item.Value);
                    }
                }
                foreach (var type in FileModule.GetTypes())
                {
                    foreach (var property in type.Properties)
                    {
                        if (property.Name != "ResourceManager")
                            continue;

                        var instr = property.GetMethod.Body.Instructions;
                    }
                }
            }

            if (JunkCheckBox.Checked)
            {
                for (int i = 0; i < 200; i++)
                {
                    var Junk = new TypeDefUser("A" + RandomChineseCharacters(10) + RandomChineseCharacters(10) + RandomChineseCharacters(10) + i, FileModule.CorLibTypes.Object.Namespace);
                    FileModule.Types.Add(Junk);
                }

                for (int i = 0; i < 200; i++)
                {
                    var Junk = new TypeDefUser(RandomChineseCharacters(10) + RandomChineseCharacters(10) + RandomChineseCharacters(10) + i, FileModule.CorLibTypes.Object.TypeDefOrRef);
                    FileModule.Types.Add(Junk);
                }

                for (int i = 0; i < 200; i++)
                {
                    var Junk = new TypeDefUser(RandomChineseCharacters(10) + RandomChineseCharacters(2) + RandomChineseCharacters(10) + RandomChineseCharacters(10) + i, FileModule.CorLibTypes.Object.TypeDefOrRef);
                    var Junk2 = new TypeDefUser(RandomChineseCharacters(11) + RandomChineseCharacters(3) + RandomChineseCharacters(11) + RandomChineseCharacters(11) + i, FileModule.CorLibTypes.Object.TypeDefOrRef);
                    FileModule.Types.Add(Junk);
                    FileModule.Types.Add(Junk2);
                }

                for (int i = 0; i < 200; i++)
                {
                    var Junk = new TypeDefUser(RandomChineseCharacters(10) + RandomChineseCharacters(4) + RandomChineseCharacters(10) + RandomChineseCharacters(10) + i, FileModule.CorLibTypes.Object.Namespace);
                    var Junk2 = new TypeDefUser(RandomChineseCharacters(11) + RandomChineseCharacters(5) + RandomChineseCharacters(11) + RandomChineseCharacters(11) + i, FileModule.CorLibTypes.Object.Namespace);
                    FileModule.Types.Add(Junk);
                    FileModule.Types.Add(Junk2);
                }
            }

            if (Base64EncodeCheckBox.Checked)
            {
                foreach (TypeDef type in FileModule.Types)
                {
                    foreach (MethodDef method in type.Methods)
                    {
                        if (method.Body == null) continue;
                        method.Body.SimplifyBranches();
                        for (int i = 0; i < method.Body.Instructions.Count; i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                            {
                                string EncodedString = method.Body.Instructions[i].Operand.ToString();
                                string InsertEncodedString = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(EncodedString));
                                method.Body.Instructions[i].OpCode = OpCodes.Nop;
                                method.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, FileModule.Import(typeof(Encoding).GetMethod("get_UTF8", new Type[] { }))));
                                method.Body.Instructions.Insert(i + 2, new Instruction(OpCodes.Ldstr, InsertEncodedString));
                                method.Body.Instructions.Insert(i + 3, new Instruction(OpCodes.Call, FileModule.Import(typeof(Convert).GetMethod("FromBase64String", new Type[] { typeof(string) }))));
                                method.Body.Instructions.Insert(i + 4, new Instruction(OpCodes.Callvirt, FileModule.Import(typeof(Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) }))));
                                i += 4;
                            }
                        }
                    }
                }
            }

            if (INTConfusionCheckBox.Checked)
            {
                foreach (var type in FileModule.GetTypes())
                {
                    if (type.IsGlobalModuleType) continue;
                    foreach (var method in type.Methods)
                    {
                        if (!method.HasBody) continue;
                        {
                            for (var i = 0; i < method.Body.Instructions.Count; i++)
                            {
                                if (!method.Body.Instructions[i].IsLdcI4()) continue;
                                var numorig = new Random(Guid.NewGuid().GetHashCode()).Next();
                                var div = new Random(Guid.NewGuid().GetHashCode()).Next();
                                var num = numorig ^ div;
                                var nop = OpCodes.Nop.ToInstruction();
                                var local = new Local(method.Module.ImportAsTypeSig(typeof(int)));
                                method.Body.Variables.Add(local);
                                method.Body.Instructions.Insert(i + 1, OpCodes.Stloc.ToInstruction(local));
                                method.Body.Instructions.Insert(i + 2, Instruction.Create(OpCodes.Ldc_I4, method.Body.Instructions[i].GetLdcI4Value() - sizeof(float)));
                                method.Body.Instructions.Insert(i + 3, Instruction.Create(OpCodes.Ldc_I4, num));
                                method.Body.Instructions.Insert(i + 4, Instruction.Create(OpCodes.Ldc_I4, div));
                                method.Body.Instructions.Insert(i + 5, Instruction.Create(OpCodes.Xor));
                                method.Body.Instructions.Insert(i + 6, Instruction.Create(OpCodes.Ldc_I4, numorig));
                                method.Body.Instructions.Insert(i + 7, Instruction.Create(OpCodes.Bne_Un, nop));
                                method.Body.Instructions.Insert(i + 8, Instruction.Create(OpCodes.Ldc_I4, 2));
                                method.Body.Instructions.Insert(i + 9, OpCodes.Stloc.ToInstruction(local));
                                method.Body.Instructions.Insert(i + 10, Instruction.Create(OpCodes.Sizeof, method.Module.Import(typeof(float))));
                                method.Body.Instructions.Insert(i + 11, Instruction.Create(OpCodes.Add));
                                method.Body.Instructions.Insert(i + 12, nop);
                                i += 12;
                            }
                            method.Body.SimplifyBranches();
                        }
                    }
                }
            }

            if (AntiILDasmCheckBox.Checked)
            {
                foreach (ModuleDef module in FileModule.Assembly.Modules)
                {
                    TypeRef attrRef = FileModule.CorLibTypes.GetTypeRef("System.Runtime.CompilerServices", "SuppressIldasmAttribute");
                    var ctorRef = new MemberRefUser(module, ".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void), attrRef);
                    var attr = new CustomAttribute(ctorRef);
                    module.CustomAttributes.Add(attr);
                }
            }

            if (CtrlFlowCheckBox.Checked)
            {
                foreach (var tDef in FileModule.Types)
                {
                    if (tDef == FileModule.GlobalType) continue;
                    foreach (var mDef in tDef.Methods)
                    {
                        if (mDef.Name.StartsWith("get_") || mDef.Name.StartsWith("set_")) continue;
                        if (!mDef.HasBody || mDef.IsConstructor) continue;
                        mDef.Body.SimplifyBranches();
                        mDef.Body.SimplifyMacros(mDef.Parameters);
                        var blocks = GetMethod(mDef);
                        var ret = new List<Block>();
                        foreach (var group in blocks)
                        {
                            Random rnd = new Random();
                            ret.Insert(rnd.Next(0, ret.Count), group);
                        }
                        blocks = ret;
                        mDef.Body.Instructions.Clear();
                        var local = new Local(mDef.Module.CorLibTypes.Int32);
                        mDef.Body.Variables.Add(local);
                        var target = Instruction.Create(OpCodes.Nop);
                        var instr = Instruction.Create(OpCodes.Br, target);
                        var instructions = new List<Instruction> { Instruction.Create(OpCodes.Ldc_I4, 0) };
                        foreach (var instruction in instructions)
                            mDef.Body.Instructions.Add(instruction);
                        mDef.Body.Instructions.Add(Instruction.Create(OpCodes.Stloc, local));
                        mDef.Body.Instructions.Add(Instruction.Create(OpCodes.Br, instr));
                        mDef.Body.Instructions.Add(target);
                        foreach (var block in blocks.Where(block => block != blocks.Single(x => x.Number == blocks.Count - 1)))
                        {
                            mDef.Body.Instructions.Add(Instruction.Create(OpCodes.Ldloc, local));
                            var instructions1 = new List<Instruction> { Instruction.Create(OpCodes.Ldc_I4, block.Number) };
                            foreach (var instruction in instructions1)
                                mDef.Body.Instructions.Add(instruction);
                            mDef.Body.Instructions.Add(Instruction.Create(OpCodes.Ceq));
                            var instruction4 = Instruction.Create(OpCodes.Nop);
                            mDef.Body.Instructions.Add(Instruction.Create(OpCodes.Brfalse, instruction4));

                            foreach (var instruction in block.Instructions)
                                mDef.Body.Instructions.Add(instruction);

                            var instructions2 = new List<Instruction> { Instruction.Create(OpCodes.Ldc_I4, block.Number + 1) };
                            foreach (var instruction in instructions2)
                                mDef.Body.Instructions.Add(instruction);

                            mDef.Body.Instructions.Add(Instruction.Create(OpCodes.Stloc, local));
                            mDef.Body.Instructions.Add(instruction4);
                        }
                        mDef.Body.Instructions.Add(Instruction.Create(OpCodes.Ldloc, local));
                        var instructions3 = new List<Instruction> { Instruction.Create(OpCodes.Ldc_I4, blocks.Count - 1) };
                        foreach (var instruction in instructions3)
                            mDef.Body.Instructions.Add(instruction);
                        mDef.Body.Instructions.Add(Instruction.Create(OpCodes.Ceq));
                        mDef.Body.Instructions.Add(Instruction.Create(OpCodes.Brfalse, instr));
                        mDef.Body.Instructions.Add(Instruction.Create(OpCodes.Br, blocks.Single(x => x.Number == blocks.Count - 1).Instructions[0]));
                        mDef.Body.Instructions.Add(instr);

                        foreach (var lastBlock in blocks.Single(x => x.Number == blocks.Count - 1).Instructions)
                            mDef.Body.Instructions.Add(lastBlock);
                    }
                }
            }

            if (AntiDebugCheckBox.Checked)
            {
                var typeModule = ModuleDefMD.Load(typeof(Classes.AntiDebug).Module);
                var cctor = FileModule.GlobalType.FindOrCreateStaticConstructor();
                var typeDef = typeModule.ResolveTypeDef(MDToken.ToRID(typeof(Classes.AntiDebug).MetadataToken));
                var members = InjectHelper.Inject(typeDef, FileModule.GlobalType, FileModule);
                var init = (MethodDef)members.Single(method => method.Name == "AntiDebugCheck");
                cctor.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Call, init));
                foreach (var md in FileModule.GlobalType.Methods)
                {
                    if (md.Name != ".ctor") continue;
                    FileModule.GlobalType.Remove(md);
                    break;
                }
            }

            if(AntiVMCheckBox.Checked)
            {
                var typeModule = ModuleDefMD.Load(typeof(Classes.AntiVM).Module);
                var cctor = FileModule.GlobalType.FindOrCreateStaticConstructor();
                var typeDef = typeModule.ResolveTypeDef(MDToken.ToRID(typeof(Classes.AntiVM).MetadataToken));
                var members = InjectHelper.Inject(typeDef, FileModule.GlobalType, FileModule);
                var init = (MethodDef)members.Single(method => method.Name == "AntiVMCheck");
                cctor.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Call, init));
                foreach (var md in FileModule.GlobalType.Methods)
                {
                    if (md.Name != ".ctor") continue;
                    FileModule.GlobalType.Remove(md);
                    break;
                }
            }
            
            if(AntiDecompilerCheckBox.Checked)
            {
                foreach (TypeDef typeDef in FileModule.GetTypes())
                {
                    foreach (MethodDef methodDef in typeDef.Methods)
                    {
                        methodDef.Body.KeepOldMaxStack = true;
                        if (methodDef.HasBody && methodDef.Body.HasInstructions)
                        {
                            methodDef.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Box, methodDef.Module.Import(typeof(Math))));
                        }
                    }
                }
            }

            if (File.Exists(Environment.CurrentDirectory + @"\Obfuscasted.exe") == false)
            {
                FileModule.Write(Environment.CurrentDirectory + @"\Obfuscasted.exe");
                if (PackingCheckBox.Checked)
                {
                    string RandomAssemblyName = RandomChineseCharacters(12);
                    PackAndEncrypt(Environment.CurrentDirectory + @"\Obfuscasted.exe", Environment.CurrentDirectory + @"\" + RandomAssemblyName + ".tmp");
                    File.Delete(Environment.CurrentDirectory + @"\Obfuscasted.exe");
                    File.Move(Environment.CurrentDirectory + @"\" + RandomAssemblyName + ".tmp", Environment.CurrentDirectory + @"\Obfuscasted.exe");
                }
            }
            else
            {
                MessageBox.Show("Please Delete or move the file: " + Environment.CurrentDirectory + @"\Obfuscasted.exe" + " first to Obfuscaste your file", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private static string HashingHardwareID(string ToHash)
        {
            byte[] KeyToHashWith = Encoding.ASCII.GetBytes("bAI!J6XwWO&A");
            HMACSHA256 SHA256Hashing = new HMACSHA256();
            SHA256Hashing.Key = KeyToHashWith;
            var TheHash = SHA256Hashing.ComputeHash(UTF8Encoding.UTF8.GetBytes(ToHash));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < TheHash.Length; i++)
            {
                builder.Append(TheHash[i].ToString("x2"));
            }
            string FinalHash = builder.ToString();
            return FinalHash;
        }

        private static string GetHardwareID()
        {
            ManagementObjectSearcher CPU = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            ManagementObjectCollection GetCPU = CPU.Get();
            string CPUID = null;
            foreach (ManagementObject CPUId in GetCPU)
            {
                CPUID = CPUId["ProcessorType"].ToString() + CPUId["ProcessorId"].ToString();
            }
            ManagementObjectSearcher BIOS = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            ManagementObjectCollection GetBIOS = BIOS.Get();
            string GPUID = null;
            foreach (ManagementObject BIOSId in GetBIOS)
            {
                GPUID = BIOSId["Manufacturer"].ToString() + BIOSId["Version"].ToString();
            }
            return HashingHardwareID(CPUID + GPUID);
        }

        private static string GetUSBHardwareID(string USBPath)
        {
            DriveInfo[] GetDrives = DriveInfo.GetDrives();
            foreach (DriveInfo GetInfo in GetDrives)
            {
                if (GetInfo.RootDirectory.ToString() == USBPath)
                {
                    ManagementObjectSearcher USB = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                    ManagementObjectCollection GetUSB = USB.Get();
                    foreach (ManagementObject USBHardwareID in GetUSB)
                    {
                        if (USBHardwareID["MediaType"].ToString() == "Removable Media")
                        {
                            return HashingHardwareID(GetInfo.TotalSize + USBHardwareID["SerialNumber"].ToString() + USBHardwareID["PNPDeviceID"].ToString());
                        }
                    }
                }
            }
            return null;
        }

        private void HWIDPacking(string FileToPack, string Output)
        {
            var Options = new Dictionary<string, string>();
            Options.Add("CompilerVersion", "v4.0");
            Options.Add("language", "c#");
            var codeProvider = new CSharpCodeProvider(Options);
            CompilerParameters parameters = new CompilerParameters();
            parameters.CompilerOptions = "/target:winexe";
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            parameters.IncludeDebugInformation = false;
            string[] Librarys = { "System", "System.Windows.Forms", "System.Management", "System.Core", "System.Runtime", "System.Runtime.InteropServices" };
            foreach (string Library in Librarys)
            {
                parameters.ReferencedAssemblies.Add(Library + ".dll");
            }
            byte[] CodeToProtect = File.ReadAllBytes(FileToPack);
            string RandomIV = RandomPassword(16);
            string RandomKey = RandomPassword(4);
            StringBuilder ROT13Encoding = new StringBuilder();
            Regex regex = new Regex("[A-Za-z]");
            foreach (char KSXZ in XOREncryptionKeys(textBox2.Text, RandomKey))
            {
                if (regex.IsMatch(KSXZ.ToString()))
                {
                    int C = ((KSXZ & 223) - 52) % 26 + (KSXZ & 32) + 65;
                    ROT13Encoding.Append((char)C);
                }
            }
            string EncryptedKey = UTF8Encoding.UTF8.GetString(MD5.Create().ComputeHash(UTF8Encoding.UTF8.GetBytes(ROT13Encoding.ToString())));
            var GetTextToHEX = Encoding.Unicode.GetBytes(EncryptedKey);
            var BuildHEX = new StringBuilder();
            foreach (var FinalHEX in GetTextToHEX)
            {
                BuildHEX.Append(FinalHEX.ToString("X2"));
            }
            StringBuilder sb = new StringBuilder(); foreach (char c in BuildHEX.ToString().ToCharArray()) { sb.Append(Convert.ToString(c, 2).PadLeft(8, '0')); }
            byte[] keys = MD5.Create().ComputeHash(UTF8Encoding.UTF8.GetBytes(sb.ToString()));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < keys.Length; i++)
            {
                builder.Append(keys[i].ToString("x2"));
            }
            string Final = Encryption.AesTextEncryption(Convert.ToBase64String(CodeToProtect), builder.ToString(), RandomIV);
            string HWIDPacker = Resource1.HWIDPacker;
            string NewHWIDPackerCode = HWIDPacker.Replace("DecME", Final).Replace("THISISIV", RandomIV).Replace("HWIDPacker", "namespace " + RandomName(14));
            string MyShinyNewPacker = NewHWIDPackerCode.Replace("SOS12", Convert.ToBase64String(Encoding.UTF8.GetBytes(RandomKey)));
            codeProvider.CompileAssemblyFromSource(parameters, MyShinyNewPacker);
        }

        private void LicensePacking(string FileToPack, string Output)
        {
            var Options = new Dictionary<string, string>();
            Options.Add("CompilerVersion", "v4.0");
            Options.Add("language", "c#");
            var codeProvider = new CSharpCodeProvider(Options);
            CompilerParameters parameters = new CompilerParameters();
            parameters.CompilerOptions = "/target:winexe";
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            parameters.IncludeDebugInformation = false;
            string[] Librarys = { "System", "System.Windows.Forms", "System.Management", "System.Net", "System.Core", "System.Net.Http", "System.Runtime", "System.Runtime.InteropServices" };
            foreach (string Library in Librarys)
            {
                parameters.ReferencedAssemblies.Add(Library + ".dll");
            }
            byte[] CodeToProtect = File.ReadAllBytes(FileToPack);
            string RandomIV = RandomPassword(16);
            string RandomKey = RandomPassword(4);
            Random rnd = new Random();
            int RandomINT = rnd.Next(6, 13);
            string RandomHashingKey = RandomPassword(RandomINT);
            StringBuilder ROT13Encoding = new StringBuilder();
            Regex regex = new Regex("[A-Za-z]");
            foreach (char KSXZ in XOREncryptionKeys(textBox3.Text, RandomKey))
            {
                if (regex.IsMatch(KSXZ.ToString()))
                {
                    int C = ((KSXZ & 223) - 52) % 26 + (KSXZ & 32) + 65;
                    ROT13Encoding.Append((char)C);
                }
            }
            StringBuilder sb = new StringBuilder(); foreach (char c in ROT13Encoding.ToString().ToCharArray()) { sb.Append(Convert.ToString(c, 2).PadLeft(8, '0')); }
            var GetTextToHEX = Encoding.Unicode.GetBytes(sb.ToString());
            var BuildHEX = new StringBuilder();
            foreach (var FinalHEX in GetTextToHEX)
            {
                BuildHEX.Append(FinalHEX.ToString("X2"));
            }
            string HashedKey = UTF8Encoding.UTF8.GetString(MD5.Create().ComputeHash(UTF8Encoding.UTF8.GetBytes(BuildHEX.ToString())));
            HMACMD5 HMACMD = new HMACMD5();
            HMACMD.Key = UTF8Encoding.UTF8.GetBytes("LXSO12".Replace(UTF8Encoding.UTF8.GetString(Convert.FromBase64String("TFhTTzEy")), RandomHashingKey));
            string HashedKey2 = UTF8Encoding.UTF8.GetString(HMACMD.ComputeHash(UTF8Encoding.UTF8.GetBytes(HashedKey)));
            string Final = Encryption.AesTextEncryption(Convert.ToBase64String(CodeToProtect), HashedKey2.ToString(), RandomIV);
            string LicensePacker = Resource1.LicensePacker;
            string NewLicensePackerCode = LicensePacker.Replace("DecME", Final).Replace("THISISIV", RandomIV).Replace("LicensePacker", "namespace " + RandomName(14));
            string MyShinyNewPacker = NewLicensePackerCode.Replace("decryptkeyencryption", Convert.ToBase64String(Encoding.UTF8.GetBytes(RandomKey))).Replace("SOS13", textBox4.Text).Replace(UTF8Encoding.UTF8.GetString(Convert.FromBase64String("TFhTTzEy")), RandomHashingKey);
            codeProvider.CompileAssemblyFromSource(parameters, MyShinyNewPacker);
        }

        private void USBPacking(string FileToPack, string Output)
        {
            var Options = new Dictionary<string, string>();
            Options.Add("CompilerVersion", "v4.0");
            Options.Add("language", "c#");
            var codeProvider = new CSharpCodeProvider(Options);
            CompilerParameters parameters = new CompilerParameters();
            parameters.CompilerOptions = "/target:winexe";
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            parameters.IncludeDebugInformation = false;
            string[] Librarys = { "System", "System.Windows.Forms", "System.Management", "System.Net", "System.Core", "System.Net.Http", "System.Runtime", "System.Runtime.InteropServices" };
            foreach (string Library in Librarys)
            {
                parameters.ReferencedAssemblies.Add(Library + ".dll");
            }
            byte[] CodeToProtect = File.ReadAllBytes(FileToPack);
            string RandomIV = RandomPassword(16);
            string RandomKey = RandomPassword(4);
            string USBHWID = GetUSBHardwareID(USBComboBox.Text);
            string EncryptedKey = XOREncryptionKeys(USBHWID, RandomKey);
            string Final = Encryption.AesTextEncryption(Convert.ToBase64String(CodeToProtect), Convert.ToBase64String(Encoding.UTF8.GetBytes(EncryptedKey)), RandomIV);
            string USBPacker = Resource1.USBPacker;
            string NewUSBPackerCode = USBPacker.Replace("DecME", Final).Replace(GetUSBHardwareID(USBComboBox.Text), USBPacker).Replace("THISISIV", RandomIV).Replace("USBPacker", "namespace " + RandomName(14)).Replace("USBSADASAS", HashingHardwareID(USBComboBox.Text));
            string MyShinyNewPacker = NewUSBPackerCode.Replace("SOS12", Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(RandomKey)));
            codeProvider.CompileAssemblyFromSource(parameters, MyShinyNewPacker);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Please Select a file to protect.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    bool IsAnythingSelected = false;
                    if (Base64EncodeCheckBox.Checked || AntiDe4DotCheckBox.Checked || FakeObfuscatorAttributesCheckBox.Checked || JunkCheckBox.Checked || CtrlFlowCheckBox.Checked || RenamingCheckBox.Checked || INTConfusionCheckBox.Checked || AntiILDasmCheckBox.Checked || AntiDebugCheckBox.Checked)
                    {
                        IsAnythingSelected = true;
                        ObfuscasteCode(textBox1.Text);
                    }

                    if (LockToHWIDCheckBox.Checked)
                    {
                        if (string.IsNullOrEmpty(textBox2.Text))
                        {
                            MessageBox.Show("Please Enter your hardware id or the buyer hardware id.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            IsAnythingSelected = true;
                            if (Base64EncodeCheckBox.Checked || AntiDe4DotCheckBox.Checked || FakeObfuscatorAttributesCheckBox.Checked || JunkCheckBox.Checked || CtrlFlowCheckBox.Checked || RenamingCheckBox.Checked || INTConfusionCheckBox.Checked || AntiILDasmCheckBox.Checked)
                            {
                                HWIDPacking(Environment.CurrentDirectory + @"\Obfuscasted.exe", Environment.CurrentDirectory + @"\Packed.exe");
                                File.Delete(Environment.CurrentDirectory + @"\Obfuscasted.exe");
                            }
                            else
                            {
                                HWIDPacking(textBox1.Text, Environment.CurrentDirectory + @"\Packed.exe");
                            }
                        }
                    }

                    if (USBHWIDCheckBox.Checked)
                    {
                        if (string.IsNullOrEmpty(USBComboBox.Text))
                        {
                            MessageBox.Show("Please Choose a USB.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            IsAnythingSelected = true;
                            if (Base64EncodeCheckBox.Checked || AntiDe4DotCheckBox.Checked || FakeObfuscatorAttributesCheckBox.Checked || JunkCheckBox.Checked || CtrlFlowCheckBox.Checked || RenamingCheckBox.Checked || INTConfusionCheckBox.Checked || AntiILDasmCheckBox.Checked)
                            {
                                USBPacking(Environment.CurrentDirectory + @"\Obfuscasted.exe", Environment.CurrentDirectory + @"\Packed.exe");
                                File.Delete(Environment.CurrentDirectory + @"\Obfuscasted.exe");
                            }
                            else
                            {
                                USBPacking(textBox1.Text, Environment.CurrentDirectory + @"\Packed.exe");
                            }
                        }
                    }

                    if (checkBox4.Checked)
                    {
                        if (string.IsNullOrEmpty(textBox3.Text))
                        {
                            MessageBox.Show("Please Enter the license you want to license your program with.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            IsAnythingSelected = true;
                            if (Base64EncodeCheckBox.Checked || AntiDe4DotCheckBox.Checked || FakeObfuscatorAttributesCheckBox.Checked || JunkCheckBox.Checked || CtrlFlowCheckBox.Checked || RenamingCheckBox.Checked || INTConfusionCheckBox.Checked || AntiILDasmCheckBox.Checked)
                            {
                                if (LockToHWIDCheckBox.Checked)
                                {
                                    MessageBox.Show("Sorry but you can't use Hardware ID Licensing with Normal Licensing.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    LicensePacking(Environment.CurrentDirectory + @"\Obfuscasted.exe", Environment.CurrentDirectory + @"\Packed.exe");
                                    File.Delete(Environment.CurrentDirectory + @"\Obfuscasted.exe");
                                }
                            }
                            else
                            {
                                LicensePacking(textBox1.Text, Environment.CurrentDirectory + @"\Packed.exe");
                            }
                        }
                    }
                    if (IsAnythingSelected == true)
                    {
                        MessageBox.Show("Done.", "Done", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = GetHardwareID();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog GetFileToProtect = new OpenFileDialog();
            GetFileToProtect.Title = "Select File To Protect";
            if (GetFileToProtect.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = GetFileToProtect.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hardware ID Licensing (HWID For Shortcut) are a type of licensing which gets the Hardware Information and then hashes it to get a unique hash for your pc, and there's two types of copy protection software that uses this method: the one who just compares the hardware id with yours and decrypt the program if it found that the hardware id matches which are horrible in terms of security cause the program can get fooled by editing code to make it think that the hardware id matches (patching in memory or disk), and the another type is the one who encrypt your program on the stub executable based on your hardware id and try to decrypt it using your hardware id or the buyer hardware id and if exception thrown then we know that the one that tries to run the program are not authorized to use it which are good for security and that what we use.", "Explaination", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                if (LockToHWIDCheckBox.Checked || USBHWIDCheckBox.Checked)
                {
                    MessageBox.Show("Sorry, but only one registration method supported.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    checkBox4.Checked = false;
                }
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            DriveInfo[] GetDrives = DriveInfo.GetDrives();
            foreach (DriveInfo GetUSB in GetDrives)
            {
                if (GetUSB.DriveType == DriveType.Removable)
                {
                    USBComboBox.Items.Clear();
                    USBComboBox.Items.Add(GetUSB.RootDirectory);
                }
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (USBHWIDCheckBox.Checked)
            {
                if (checkBox4.Checked || LockToHWIDCheckBox.Checked)
                {
                    MessageBox.Show("Sorry, but only one registration method supported.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    USBHWIDCheckBox.Checked = false;
                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (LockToHWIDCheckBox.Checked)
            {
                if (checkBox4.Checked || USBHWIDCheckBox.Checked)
                {
                    MessageBox.Show("Sorry, but only one registration method supported.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    LockToHWIDCheckBox.Checked = false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("you choose a usb from the list and choose a program to protect and then the program will be encrypted based on the usb hardware id and if no usb found or there were no vaild usb the program will show a message that says that you are not authorized to use this program and only works if you entered a vaild USB making it impossible for someone which doesn't have your USB to recover the program code or access it, and please make sure to enter one USB while protecting the program or running it, cause errors may occur.", "Explaination", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void checkBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip ShowInfo = new ToolTip();
            ShowInfo.SetToolTip(Base64EncodeCheckBox, "Encode Strings Inside The Protected .NET Executable To Prevent Easy String Access or to identify a function based on string.");
        }

        private void checkBox2_MouseHover(object sender, EventArgs e)
        {
            ToolTip ShowInfo = new ToolTip();
            ShowInfo.SetToolTip(AntiDe4DotCheckBox, "Prevent De4dot from processing the protected .NET File.");
        }

        private void checkBox5_MouseHover(object sender, EventArgs e)
        {
            ToolTip ShowInfo = new ToolTip();
            ShowInfo.SetToolTip(FakeObfuscatorAttributesCheckBox, "Prevent Identifying Obfuscastor Which Obfuscasted This File and it can cause automated deobfuscastors tools to corrupt the .NET executable file.");
        }

        private void checkBox6_MouseHover(object sender, EventArgs e)
        {
            ToolTip ShowInfo = new ToolTip();
            ShowInfo.SetToolTip(JunkCheckBox, "Adding Junk Namespaces and Methods.");
        }

        private void checkBox7_MouseHover(object sender, EventArgs e)
        {
            ToolTip ShowInfo = new ToolTip();
            ShowInfo.SetToolTip(CtrlFlowCheckBox, "Control Flow Edits the program in such a way so it returns the same result and mangling the code, and it can confuse the one who are trying to read the source code.");
        }

        private void checkBox8_MouseHover(object sender, EventArgs e)
        {
            ToolTip ShowInfo = new ToolTip();
            ShowInfo.SetToolTip(RenamingCheckBox, "renaming assembly name, methods and functions to the same name so that the one who try to decompile it and tries to identify a function or a string he can't easily.");
        }

        private void checkBox10_MouseHover(object sender, EventArgs e)
        {
            ToolTip ShowInfo = new ToolTip();
            ShowInfo.SetToolTip(INTConfusionCheckBox, "This Protection adds junk INT Comparsion, sizeof's and float's, making it more confusing.");
        }

        private void checkBox11_MouseHover(object sender, EventArgs e)
        {
            ToolTip ShowInfo = new ToolTip();
            ShowInfo.SetToolTip(AntiILDasmCheckBox, "Prevent Decompiling .NET Assembly by adding SuppressIldasmAttribute Attribute to it, probably you will never need this option but you can add it as an extra.");
        }

        private void checkBox12_MouseHover(object sender, EventArgs e)
        {
            ToolTip ShowInfo = new ToolTip();
            ShowInfo.SetToolTip(PackingCheckBox, "Encrypting Your .NET Executable Inside of another one that will gonna be decrypted in memory, but keep in mind that this are not AV Friendly.");
        }

        private void checkBox13_MouseHover(object sender, EventArgs e)
        {
            ToolTip ShowInfo = new ToolTip();
            ShowInfo.SetToolTip(AntiDebugCheckBox, "Convert Calls to Calli.");
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void checkBox13_MouseHover_1(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(AntiDebugCheckBox, "Injects Anti-Debugging Code into the protected application.");
        }

        private void checkBox14_MouseHover(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(AntiVMCheckBox, "Injects Anti-VM Code into the protected application.");
        }
    }
}