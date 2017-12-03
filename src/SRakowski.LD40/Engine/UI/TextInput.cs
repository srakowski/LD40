using System;
using System.Collections.Generic;
using System.Text;

namespace SRakowski.LD40.Engine.UI
{
    class TextInput : FormElement
    {
        public string Label { get; set; }

        public string Text { get; set; }

        public TextInput(Form form, string label)
            : base(form)
        {
        }
    }
}
