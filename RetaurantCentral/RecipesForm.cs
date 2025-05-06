// Rename the class or move it to a different namespace to avoid duplication
using System;
using System.Windows.Forms;

namespace RestaurantSystem
{
    internal class RecipesFormDuplicate // Renamed to avoid conflict
    {
        public RecipesFormDuplicate()
        {
        }

        public System.Action<object, object> FormClosed { get; internal set; }

        internal void Show()
        {
            throw new NotImplementedException();
        }

        public static implicit operator Form(RecipesFormDuplicate v)
        {
            throw new NotImplementedException();
        }
    }
}