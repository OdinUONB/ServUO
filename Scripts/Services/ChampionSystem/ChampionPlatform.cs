using System;
using Server.Items;

namespace Server.Engines.CannedEvil
{
    public class ChampionPlatform : BaseAddon
    {
        private ChampionSpawn m_Spawn;
        public ChampionPlatform(ChampionSpawn spawn)
        {
            this.m_Spawn = spawn;

            for (int x = -2; x <= 2; ++x)
                for (int y = -2; y <= 2; ++y)
                    this.AddComponent(0x750, x, y, -5);

            for (int x = -1; x <= 1; ++x)
                for (int y = -1; y <= 1; ++y)
                    this.AddComponent(0x750, x, y, 0);

            for (int i = -1; i <= 1; ++i)
            {
                this.AddComponent(0x751, i, 2, 0);
                this.AddComponent(0x752, 2, i, 0);

                this.AddComponent(0x753, i, -2, 0);
                this.AddComponent(0x754, -2, i, 0);
            }

            this.AddComponent(0x759, -2, -2, 0);
            this.AddComponent(0x75A, 2, 2, 0);
            this.AddComponent(0x75B, -2, 2, 0);
            this.AddComponent(0x75C, 2, -2, 0);
        }

        public ChampionPlatform(Serial serial)
            : base(serial)
        {
        }

        public void AddComponent(int id, int x, int y, int z)
        {
            AddonComponent ac = new AddonComponent(id);

            ac.Hue = 0x452;

            this.AddComponent(ac, x, y, z);
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            if (this.m_Spawn != null)
                this.m_Spawn.Delete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            writer.Write(this.m_Spawn);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch ( version )
            {
                case 1:
                case 0:
                    {
                        this.m_Spawn = reader.ReadItem() as ChampionSpawn;

                        if (this.m_Spawn == null)
                            this.Delete();

                        break;
                    }
            }

            if (version == 0)
            {
                Hue = 0x452;

                foreach (var comp in Components)
                {
                    comp.Hue = 0x452;

                    if (comp.ItemID == 0x750)
                        comp.ItemID = 0x3EE;

                    if (comp.ItemID == 0x751)
                        comp.ItemID = 0x3EF;

                    if (comp.ItemID == 0x752)
                        comp.ItemID = 0x3F0;

                    if (comp.ItemID == 0x753)
                        comp.ItemID = 0x3F1;

                    if (comp.ItemID == 0x754)
                        comp.ItemID = 0x3F2;

                    if (comp.ItemID == 0x759)
                        comp.ItemID = 0x3F7;

                    if (comp.ItemID == 0x75A)
                        comp.ItemID = 0x3F8;

                    if (comp.ItemID == 0x75B)
                        comp.ItemID = 0x3F9;

                    if (comp.ItemID == 0x75C)
                        comp.ItemID = 0x3FA;
                }
            }
        }
    }
}