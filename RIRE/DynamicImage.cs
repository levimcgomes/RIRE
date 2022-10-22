namespace RIRE
{
    public class DynamicImage
    {
        private Image image;

        public DynamicImage(int size) {
            image = new Image(size, new Color(1, 1, 1));
        }

        public Image getImageAtTime(int t) {
            image[t%image.size, t%image.size]=new Color((t%100)/100, 0, 0);
            return image;
        }
    }
}
