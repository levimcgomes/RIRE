namespace RIRE
{
    public class Color
    {
        private float[] rgb;

        public float this[int index] {
            get => rgb[index];
            set => rgb[index] = value;
        }

        public Color(float[] rgb) {
            this.rgb = rgb;
        }

        public Color(float r = 0, float g = 0, float b = 0) {
            this.rgb = new float[] { r, g, b };
        }
    }
}
