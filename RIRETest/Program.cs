using RIRE;

class Program
{
    private static void Main(string[] args) {
        RIRE.DynamicImage dynamicImage = new RIRE.DynamicImage();
        dynamicImage.Test(nameof(dynamicImage));
        System.Console.WriteLine(typeof(DynamicImage));
    }
}