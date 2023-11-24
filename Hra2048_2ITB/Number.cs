using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hra2048_2ITB
{
    public class Number : Label
    {
        private int currentValue;

        private int size = 200;

        public int CurrentValue {
            get { return currentValue; }
            set {
                currentValue = value;
                if(currentValue == 0) {
                    Text = "";
                } else {
                    Text = currentValue.ToString();
                }
                BackColor = colors[currentValue];
            }
        }

        private static Dictionary<int, Color> colors = new Dictionary<int, Color>() {
            { 0, Color.White },
            { 2, Color.Magenta },
            { 4, Color.LawnGreen },
            { 8, Color.OrangeRed },
            { 16, Color.Thistle },
            { 32, Color.Turquoise },
            { 64, Color.Wheat },
            { 128, Color.Moccasin },
            { 256, Color.Gold },
            { 512, Color.Gainsboro },
            { 1024, Color.Chocolate },
            { 2048, Color.Cornsilk },
            { 4096, Color.Coral },
            { 8192, Color.Tomato },
        };
    
        public void Setup(int x, int y) {

            AutoSize = false;
            Size = new Size(size, size);
            TextAlign = ContentAlignment.MiddleCenter;
            Font = new Font("Comic Sans MS", 32f);
            BorderStyle = BorderStyle.FixedSingle;
            Location = new Point(x * size, y * size);

            CurrentValue = 0;
        }

    }
}
