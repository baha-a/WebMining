using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebMining
{
    public class ItemsDictionary : KeyedCollection<string, Item>
    {
        public ItemsDictionary(List<Item> frequentItems)
        {
            foreach (var item in frequentItems)
                Add(item);
        }

        protected override string GetKeyForItem(Item item)
        {
            return item.Name;
        }
    }
}