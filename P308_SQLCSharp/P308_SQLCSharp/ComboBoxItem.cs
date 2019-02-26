using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P308_SQLCSharp
{
    public class ComboBoxItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        /// <summary>
        /// This class was designed for Inserting Id and Name in Comboboxes.
        /// </summary>
        /// <param name="id">Id of the item</param>
        /// <param name="name">Name of the item</param>
        public ComboBoxItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
