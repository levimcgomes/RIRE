using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Image = RIRE.Image;

namespace RIRETest_Graphic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Time related fields
        private DispatcherTimer timer;
        private bool timeStrategy = true; // true=composition, false=timer
        private int frameCount = 0;

        // Image related fields
        //private DynamicImage dynamicImage = new DynamicImage(200);
        private Image image;
        private WriteableBitmap writeableBitmap = new WriteableBitmap(200, 200, 1, 1, PixelFormats.Bgr32, null);
        public WriteableBitmap WriteableBitmap => writeableBitmap;
        public Image Image => image;

        // Misc
        //public string codeArea;
        //public string consoleArea;

        public MainWindow() {
            InitializeComponent();

            // Setup timer
            timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Interval = TimeSpan.FromSeconds(1 / 60);

            // Initialize image
            RIREWrapper.Setup();
            image = RIREWrapper.dynamicImage.getNextFrame();

            // Start rendering using chosen strategy
            StartRendering();
            // Allow for bindings
            DataContext = this;
        }

        // Wrapper function to start different time strategies
        private void StartRendering() {
            if(timeStrategy) { // Use composition
                CompositionTarget.Rendering += CompositionTick;
            }
            else { // Use DispatchTimer
                timer.Tick += TimerTick;
                timer.Start();
            }
        }

        // Tick functions - indirect approach used for testing internal vs. external timing on the libray side
        private void CompositionTick(object? sender, EventArgs e) => RenderFrame();
        private void TimerTick(object? sender, EventArgs e) => RenderFrame();

        // Wrapper for updating the image and porting it to the writeableBitmap
        private void RenderFrame() {
            frameCount++; // NEEDS REFACTORING - the DynamicImage should keep track of this
            image = RIREWrapper.dynamicImage.getNextFrame(); // Makeshift Image Updating
            // Turn Image into WriteableBitmap
            RenderImage(0, 0, image.size, image.size);
        }

        #region WriteableBitmap Handling -- thanks to Github user 'stevemonaco' for all the help with this!
        protected void RenderImage(int xStart, int yStart, int width, int height) {
            try {
                if(!writeableBitmap.TryLock(new System.Windows.Duration(TimeSpan.FromMilliseconds(500))))
                    throw new TimeoutException($"{nameof(RenderImage)} could not lock the Bitmap for rendering");

                unsafe {
                    for(int y = yStart; y < yStart + height; y++) {
                        var dest = (uint*)writeableBitmap.BackBuffer.ToPointer();
                        dest += y * writeableBitmap.BackBufferStride / 4 + xStart;
                        //var row = Image.GetPixelRowSpan(y);

                        for(int x = 0; x < width; x++) {
                            //var color = row[x + xStart];
                            var color = image[x, y];
                            dest[x] = TranslateColor(color);
                        }
                    }
                }

                writeableBitmap.AddDirtyRect(new System.Windows.Int32Rect(xStart, yStart, width, height));
            }
            finally {
                writeableBitmap!.Unlock();
            }
        }
        private uint TranslateColor(RIRE.Color color) {
            byte rChannel = (byte)(255 * color.r);
            byte gChannel = (byte)(255 * color.g);
            byte bChannel = (byte)(255 * color.b);
            byte aChannel = (byte)0xFF;

            uint uintcolor = (uint)((aChannel << 24) | (rChannel << 16) | (gChannel << 8) | bChannel);
            return uintcolor;
        }
        #endregion
    }
}
