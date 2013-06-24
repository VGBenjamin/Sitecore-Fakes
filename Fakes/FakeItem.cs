using System;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;


namespace Sitecore.Fakes
{
    public class FakeItem : Item
    {
        public const string DefaultitemName = "FakeItem";
        public const string DefaultDatabaseName = "web";

        public FakeItem(string itemName = DefaultitemName)
            : this(ID.NewID,itemName)
        {
        }


        public FakeItem(ID itemId,string itemName=DefaultitemName)
            : this(new FieldList(), itemId,itemName)
        {
        }

        public FakeItem(ID itemId, ID templateID,string itemName = DefaultitemName)
            : this(new FieldList(), itemId, templateID,itemName)
        {
        }
       
        public FakeItem(FieldList fieldList, string itemName = DefaultitemName)
            : this(fieldList, ID.NewID,ID.NewID ,itemName)
        {
        }
        
        public FakeItem(FieldList fieldList, ID itemid, string itemName = DefaultitemName, string databaseName = DefaultDatabaseName)
            : base(
               itemid,
                new ItemData(new ItemDefinition(ID.NewID, itemName, ID.NewID, ID.NewID),
                             Globalization.Language.Invariant, new Data.Version(1), fieldList),
                new Database(databaseName))
        {
            FakeChildren = new ItemList();
           
        }

        public FakeItem(FieldList fieldList, ID itemid, ID templateId, string itemName = DefaultitemName, string databaseName = DefaultDatabaseName)
            : base(
               itemid,
                new ItemData(new ItemDefinition(ID.NewID, itemName, templateId, ID.NewID),
                             Globalization.Language.Invariant, new Data.Version(1), fieldList),
                new Database(databaseName))
        {
            FakeChildren = new ItemList();
           
        }

        
        public virtual void AddChild(Item child)
        {
            ((FakeItem)child).FakeParent = this;
            FakeChildren.Add(child);
        }

      
    private Item fakeParent;
        public Item FakeParent
        {
            get { return fakeParent; }
            set
            {
                fakeParent = value;
                if (fakeParent is FakeItem)
                    // Directy assign the variable to avoid the infinite loops
                    ((FakeItem) fakeParent).fakeChildren.Add(this); 
                
            }
        }

        private ItemList fakeChildren;
        public ItemList FakeChildren
        {
            get { return fakeChildren; } 
            set 
            { 
                fakeChildren = value;
                foreach (var fakeChild in fakeChildren)
                {
                    if (fakeChild is FakeItem)
                        // Directy assign the variable to avoid the infinite loops
                        ((FakeItem)fakeChild).fakeParent = this;
                }
            }
        }

        
    }
}
