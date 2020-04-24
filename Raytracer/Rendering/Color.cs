using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Rendering {
    class Color {

        public static int colorMax = 255;
        public static int colorMin = 0;
        private class ColorChannel {
            public ColorChannel(int value) {
                this.Value = value;
            }
            public int Value {
                get => _value;
                set {
                    if(value < colorMin) {
                        _value = 0;
                    }
                    else if (value > colorMax) {
                        _value = colorMax;
                    }
                    else {
                        _value = value;
                    }
                }
            }
            private int _value;
        }
        public int Red {
            get => _r.Value;
            set => _r.Value = value;
        }
        public int Green {
            get => _g.Value;
            set => _g.Value = value;
        }
        public int Blue {
            get => _b.Value;
            set => _b.Value = value;
        }

        private ColorChannel _r;
        private ColorChannel _g;
        private ColorChannel _b;

        public Color(int r, int g, int b) {
            this._r = new ColorChannel(r);
            this._g = new ColorChannel(g);
            this._b = new ColorChannel(b);
        }

        public override string ToString() {
            return Red.ToString() + " " +  Green.ToString() + " " + Blue.ToString();
        }

        public static Color operator *(float constant, Color color) {
            return new Color((int)(constant * color.Red), (int)(constant * color.Green), (int)(constant * color.Blue));
        }

        public static Color GetWeightedAverage(List<KeyValuePair<float, Color>> colors) {
            float r = 0, g = 0, b = 0;
            float totalWeights = 0;
            foreach (KeyValuePair<float, Color> pair in colors) {
                float weight = pair.Key;
                totalWeights += weight;

                Color color = pair.Value;
                r += color.Red * weight;
                g += color.Green * weight;
                b += color.Blue * weight;
            }
            return new Color((int)(r / totalWeights), (int)(g / totalWeights), (int)(b / totalWeights));
        }

        public static Color operator+(Color a, Color b) {
            return new Color(a.Red + b.Red, a.Green + b.Green, a.Blue + b.Blue);
        }
    }
}
