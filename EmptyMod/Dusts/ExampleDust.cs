using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Dusts
{
    public class ExampleDust : ModDust
    {
        public override bool Autoload(ref string name, ref string texture)
        {

            throw new NotImplementedException("����ʱ����");
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            throw new NotImplementedException("͸����");
        }
        public override bool MidUpdate(Dust dust)
        {
            throw new NotImplementedException("�����ж�");
        }
        public override void OnSpawn(Dust dust)
        {
            throw new NotImplementedException("ˢ��ʱ����");
        }
        public override void SetDefaults()
        {
            throw new NotImplementedException("��ʼ��");
        }
        public override bool Update(Dust dust)
        {
            throw new NotImplementedException("����ʱ����");
        }
    }
}