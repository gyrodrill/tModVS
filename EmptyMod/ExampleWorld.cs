using System.Collections.Generic;
using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace ExampleMod
{
    public class ExampleWorld : ModWorld
    {
        public override bool Autoload(ref string name)
        {
            return base.Autoload(ref name);
        }
        public override void ChooseWaterStyle(ref int style)
        {
            base.ChooseWaterStyle(ref style);
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Load(TagCompound tag)
        {
            base.Load(tag);
        }
        public override void LoadLegacy(BinaryReader reader)
        {
            base.LoadLegacy(reader);
        }
        public override void ModifyHardmodeTasks(List<GenPass> list)
        {
            base.ModifyHardmodeTasks(list);
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            base.ModifyWorldGenTasks(tasks, ref totalWeight);
        }
        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
        }
        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
        }
        public override void PostDrawTiles()
        {
            base.PostDrawTiles();
        }
        public override void PostUpdate()
        {
            base.PostUpdate();
        }
        public override void PostWorldGen()
        {
            base.PostWorldGen();
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
        }
        public override void PreWorldGen()
        {
            base.PreWorldGen();
        }
        public override void ResetNearbyTileEffects()
        {
            base.ResetNearbyTileEffects();
        }
        public override TagCompound Save()
        {
            return base.Save();
        }
        public override void TileCountsAvailable(int[] tileCounts)
        {
            base.TileCountsAvailable(tileCounts);
        }
    }
}
