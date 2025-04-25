using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba13
{
    public class Journal
    {
        public List<JournalEntry> Entries { get; set; } = new List<JournalEntry>();

        public void CollectionCountChange(object source, CollectionHandlerEventArgs args)
        {
            JournalEntry journalEntry = new JournalEntry(args.CollectionName, args.TypeChange, args.ChangeObj);
            Entries.Add(journalEntry);
        }

        public void CollectionReferenceChange(object source, CollectionHandlerEventArgs args)
        {
            JournalEntry journalEntry = new JournalEntry(args.CollectionName, args.TypeChange, args.ChangeObj);
            Entries.Add(journalEntry);
        }

        public override string ToString()
        {
            return string.Join("\n", Entries);
        }
    }
}
