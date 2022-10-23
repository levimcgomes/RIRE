namespace RIRE
{
    public class DynamicImage
    {
        private Image image;
        private int frame;
        private TimeSpan elapsedTime;
        private DateTime startTime;

        public DynamicImage(int size) {
            image = new Image(size, new Color(1, 1, 1));
        }

        public DynamicImage(int size, Color color) {
            image = new Image(size, color);
        }

        public void StartPlaying() {
            startTime = DateTime.Now;
            elapsedTime = DateTime.Now - startTime;
            frame = 0;
        }

        public Image getNextFrame() {
            frame++;
            elapsedTime = DateTime.Now - startTime;
            image[elapsedTime.Milliseconds%image.size, elapsedTime.Milliseconds % image.size]=new Color((elapsedTime.Milliseconds % 100)/100, 0, 0);
            return image;
        }
    }
}
