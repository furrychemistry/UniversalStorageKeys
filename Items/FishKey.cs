using Server.Engines.VeteranRewards;
using Solaris.ItemStore;							//for connection to resource store data objects
using System.Collections.Generic;

namespace Server.Items
{
    //item inherited from BaseResourceKey
    public class FishKey : BaseStoreKey, IRewardItem
    {
        private bool m_IsRewardItem;

        [CommandProperty(AccessLevel.Seer)]
        public bool IsRewardItem
        {
            get => m_IsRewardItem;
            set { m_IsRewardItem = value; InvalidateProperties(); }
        }

        public override int DisplayColumns => 2;

        public override List<StoreEntry> EntryStructure
        {
            get
            {
                List<StoreEntry> entry = new List<StoreEntry>
                {
                    new GenericDistinguishableEntry(typeof(Fish), "ItemID", "0x9CC", "Amount"),
                    new GenericDistinguishableEntry(typeof(Fish), "ItemID", "0x9CD", "Amount"),
                    new GenericDistinguishableEntry(typeof(Fish), "ItemID", "0x9CE", "Amount"),
                    new GenericDistinguishableEntry(typeof(Fish), "ItemID", "0x9CF", "Amount")
                };

                return entry;
            }
        }

        [Constructable]
        public FishKey() : base(0x0)        // hue 1929
        {
            ItemID = 0xFFA;
            Name = "Fish Bucket";
        }

        //this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
        protected override ItemStore GenerateItemStore()
        {
            //load the basic store info
            ItemStore store = base.GenerateItemStore();

            //properties of this storage device
            store.Label = "Fish Storage";

            store.Dynamic = false;
            store.OfferDeeds = false;
            return store;
        }

        //serial constructor
        public FishKey(Serial serial) : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_IsRewardItem)
            {
                list.Add(1076217); // 1st Year Veteran Reward
            }
        }

        //events

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);

            writer.Write(m_IsRewardItem);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            _ = reader.ReadInt();

            m_IsRewardItem = reader.ReadBool();
        }
    }
}
