using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using RIRE;
using Image = RIRE.Image;

namespace RIRETest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer timer;
        private bool timeStrategy = true; // true=composition, false=timer
        private int frameCount = 0;

        private DynamicImage dynamicImage = new DynamicImage(200);
        private Image? image;
        private WriteableBitmap writeableBitmap = new WriteableBitmap(200, 200, 1, 1, PixelFormats.Bgr32, null);

        public MainWindow() {
            InitializeComponent();

            timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Interval = TimeSpan.FromSeconds(1 / 60);
            timer.Tick += TimerTick;

            StartRendering();
        }

        private void StartRendering() {
            if(timeStrategy) {
                CompositionTarget.Rendering += CompositionTick;
            }
            else {
                timer.Start();
            }
        }

        private void CompositionTick(object? sender, EventArgs e) => RenderFrame();

        private void TimerTick(object? sender, EventArgs e) => RenderFrame();

        private void RenderFrame() {
            frameCount++;
            image = dynamicImage.getImageAtTime(frameCount);
            // Turn Image into WriteableBitmap

        }
    }
}
