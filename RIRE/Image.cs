namespace RIRE
{
    public class Image
    {
        private Color[,] pixels;

        public Color this[int index1, int index2] {
            get => pixels[index1, index2];
            set => pixels[index1, index2] = value;
        }

        public Image(int size, Color color) {
            pixels = new Color[size, size];
            for(int x = 0; x < size; x++) {
                for(int y = 0; y < size; y++) {
                    pixels[x, y] = color;
                }
            }
        }
    }
}
