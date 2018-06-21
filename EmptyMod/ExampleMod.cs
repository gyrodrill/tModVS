using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria.Graphics;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExampleMod
{
    public class ExampleMod : Mod
	{
	    static ExampleMod()
	    {
            // Load dllReferences from EmbeddedResource
	        AppDomain.CurrentDomain.AssemblyResolve += (o, args) =>
	        {
	            var name = new AssemblyName(args.Name).Name + ".dll";
	            string text = Array.Find(typeof(ExampleMod).Assembly.GetManifestResourceNames(),
	                (element) => element.EndsWith(name));
	            if (text != null)
	            {
	                using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(text))
	                {
	                    byte[] array = new byte[manifestResourceStream.Length];
	                    manifestResourceStream.Read(array, 0, array.Length);
	                    return Assembly.Load(array);
	                }
	            }
	            return  null;
	        };
        }

	    internal static ExampleMod instance;
        public override string Name
        {
            get
            {
                return "Mod名称";
            }
        }

	    public override Version tModLoaderVersion => base.tModLoaderVersion;
        public override Version Version => base.Version;
        public override void AddRecipeGroups()
        {
            base.AddRecipeGroups();
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
        }
        public override object Call(params object[] args)
        {
            return base.Call(args);
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            base.HandlePacket(reader, whoAmI);
        }
        public override bool HijackGetData(ref byte messageType, ref BinaryReader reader, int playerNumber)
        {
            return base.HijackGetData(ref messageType, ref reader, playerNumber);
        }
        public override bool HijackSendData(int whoAmI, int msgType, int remoteClient, int ignoreClient, NetworkText text, int number, float number2, float number3, float number4, int number5, int number6, int number7)
        {
            return base.HijackSendData(whoAmI, msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, number5, number6, number7);
        }
        public override void HotKeyPressed(string name)
        {
            base.HotKeyPressed(name);
        }
        public override void Load()
        {
            base.Load();
        }
        public override void LoadResourceFromStream(string path, int len, BinaryReader reader)
        {
            base.LoadResourceFromStream(path, len, reader);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            base.ModifyInterfaceLayers(layers);
        }
        public override void ModifyLightingBrightness(ref float scale)
        {
            base.ModifyLightingBrightness(ref scale);
        }
        public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {
            base.ModifySunLightColor(ref tileColor, ref backgroundColor);
        }
        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            base.ModifyTransformMatrix(ref Transform);
        }
        public override void PostAddRecipes()
        {
            base.PostAddRecipes();
        }
        public override void PostDrawFullscreenMap(ref string mouseText)
        {
            base.PostDrawFullscreenMap(ref mouseText);
        }
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            base.PostDrawInterface(spriteBatch);
        }
        public override void PostSetupContent()
        {
            base.PostSetupContent();
        }
        public override void PostUpdateInput()
        {
            base.PostUpdateInput();
        }
        public override void PreSaveAndQuit()
        {
            base.PreSaveAndQuit();
        }
        public override void Unload()
        {
            base.Unload();
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            base.UpdateMusic(ref music, ref priority);
        }
        public override void UpdateMusic(ref int music)
        {
            base.UpdateMusic(ref music);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
        }
    }
}

