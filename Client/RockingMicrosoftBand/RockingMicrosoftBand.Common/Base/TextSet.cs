using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace RockingMicrosoftBand.Common.Base
{
    public class TextSet
    {
        public TextSet()
        {
            this.FontFamily = FontFamilies.Stencil;
            this.Text = string.Empty;
        }

        public FontFamily FontFamily { get; set; }

        public string Text { get; set; }
    }
}
