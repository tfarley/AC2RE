using AC2RE.Definitions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AC2RE.UICommon.UI {

    public partial class MapWindow : Window {

        public MapWindow(DatReader cellDatReader) {
            InitializeComponent();

            var pixelFormat = PixelFormats.Rgb24;

            int numBlocksX = 256;
            int numBlocksY = 256;
            int numHeightsX = 17;
            int numHeightsY = 17;
            int imageWidth = numBlocksX * numHeightsX;
            int imageHeight = numBlocksY * numHeightsY;
            int stride = (imageWidth * pixelFormat.BitsPerPixel + 7) / 8;
            byte[] pixels = new byte[stride * imageHeight];

            int bytesPerPixel = (pixelFormat.BitsPerPixel + 7) / 8;

            for (int y = 0; y < numBlocksY; y++) {
                for (int x = 0; x < numBlocksX; x++) {
                    DataId blockDid = new DataId(new CellId((byte)x, (byte)y, 0xFF, 0xFF).id);
                    if (cellDatReader.contains(blockDid)) {
                        using (AC2Reader data = cellDatReader.getFileReader(blockDid)) {
                            CLandBlockData landBlockData = new(data);
                            for (int j = 0; j < numHeightsY; j++) {
                                for (int i = 0; i < numHeightsX; i++) {
                                    int dataIndex = (i * numHeightsX) + j;
                                    byte height = landBlockData.heights[dataIndex];
                                    for (int p = 0; p < bytesPerPixel; p++) {
                                        pixels[((((numBlocksY - 1 - y) * numHeightsY) + (numHeightsY - 1 - j)) * (numBlocksX * numHeightsX) + ((x * numHeightsX) + i)) * bytesPerPixel + p] = height;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            mapImage.Source = BitmapSource.Create(imageWidth, imageHeight, 10.0, 1.0, pixelFormat, null, pixels, stride);
        }
    }
}
