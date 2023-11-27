using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Net.Mime.MediaTypeNames;
using WebCamLib;
using Image = System.Drawing.Image;

namespace ImageProcessing_Activity
{
    public partial class Form1 : Form
    {
        Bitmap processed, loaded;
        Bitmap imageB, imageA, resultImage;
        Device[] mgaDevice = DeviceManager.GetAllDevices();
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) { }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                using (SaveFileDialog saveFile = new SaveFileDialog())
                {
                    saveFile.Filter = "JPEG Image|*.jpg|PNG Image|*.png|All Files|*.*";
                    saveFile.Title = "Save Image";
                    saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFile.FileName;

                        ImageFormat format = ImageFormat.Jpeg;
                        if (saveFile.FilterIndex == 2)
                        {
                            format = ImageFormat.Png;
                        }

                        pictureBox2.Image.Save(filePath, format);
                    }
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color Pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, Pixel);
                }
            }
            pictureBox2.Image = processed;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            int grey;
            Color Pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Pixel = loaded.GetPixel(x, y);
                    grey = (byte)((Pixel.R + Pixel.G + Pixel.B) / 3);
                    processed.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            }
            pictureBox2.Image = processed;
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color Pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, Color.FromArgb(255 - Pixel.R, 255 - Pixel.G, 255 - Pixel.B));
                }
            }
            pictureBox2.Image = processed;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] blueHistogram = new int[256];
            loaded = new Bitmap(openFileDialog1.FileName);
            Color Pixel;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Pixel = loaded.GetPixel(x, y);
                    blueHistogram[Pixel.B]++;
                }
            }

            // Create a new form to display the chart
            Form histogramForm = new Form();
            histogramForm.Text = "Blue Channel Histogram";

            // Create the Chart control
            Chart chart = new Chart();
            chart.Parent = histogramForm;
            chart.Size = new System.Drawing.Size(600, 400);
            chart.Dock = DockStyle.Fill;

            // Create a chart area and add it to the chart
            ChartArea chartArea = new ChartArea("MainChartArea");
            chart.ChartAreas.Add(chartArea);

            // Create a series for the histogram data
            Series series = new Series("BlueHistogram");
            series.ChartType = SeriesChartType.Column; // Use column chart for histograms

            // Add data points to the series using the blue histogram data
            for (int i = 0; i < blueHistogram.Length; i++)
            {
                series.Points.AddXY(i, blueHistogram[i]);
            }

            // Add the series to the chart
            chart.Series.Add(series);

            // Add the chart to the form and show it
            histogramForm.Controls.Add(chart);
            histogramForm.ShowDialog();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] greenHistogram = new int[256];
            loaded = new Bitmap(openFileDialog1.FileName);
            Color Pixel;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Pixel = loaded.GetPixel(x, y);
                    greenHistogram[Pixel.G]++;
                }
            }

            // Create a new form to display the chart
            Form histogramForm = new Form();
            histogramForm.Text = "Green Channel Histogram";

            // Create the Chart control
            Chart chart = new Chart();
            chart.Parent = histogramForm;
            chart.Size = new System.Drawing.Size(600, 400);
            chart.Dock = DockStyle.Fill;

            // Create a chart area and add it to the chart
            ChartArea chartArea = new ChartArea("MainChartArea");
            chart.ChartAreas.Add(chartArea);

            // Create a series for the histogram data
            Series series = new Series("GreenHistogram");
            series.ChartType = SeriesChartType.Column; // Use column chart for histograms

            // Add data points to the series using the blue histogram data
            for (int i = 0; i < greenHistogram.Length; i++)
            {
                series.Points.AddXY(i, greenHistogram[i]);
            }

            // Add the series to the chart
            chart.Series.Add(series);

            // Add the chart to the form and show it
            histogramForm.Controls.Add(chart);
            histogramForm.ShowDialog();
        }

        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int[] redHistogram = new int[256];
            loaded = new Bitmap(openFileDialog1.FileName);
            Color Pixel;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Pixel = loaded.GetPixel(x, y);
                    redHistogram[Pixel.R]++;
                }
            }

            // Create a new form to display the chart
            Form histogramForm = new Form();
            histogramForm.Text = "Red Channel Histogram";

            // Create the Chart control
            Chart chart = new Chart();
            chart.Parent = histogramForm;
            chart.Size = new System.Drawing.Size(600, 400);
            chart.Dock = DockStyle.Fill;

            // Create a chart area and add it to the chart
            ChartArea chartArea = new ChartArea("MainChartArea");
            chart.ChartAreas.Add(chartArea);

            // Create a series for the histogram data
            Series series = new Series("RedHistogram");
            series.ChartType = SeriesChartType.Column; // Use column chart for histograms

            // Add data points to the series using the blue histogram data
            for (int i = 0; i < redHistogram.Length; i++)
            {
                series.Points.AddXY(i, redHistogram[i]);
            }

            // Add the series to the chart
            chart.Series.Add(series);

            // Add the chart to the form and show it
            histogramForm.Controls.Add(chart);
            histogramForm.ShowDialog();
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color Pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Pixel = loaded.GetPixel(x, y);
                    int red = (int)((Pixel.R * 0.393) + (Pixel.G * 0.769) + (Pixel.B * 0.189));
                    int green = (int)((Pixel.R * 0.349) + (Pixel.G * 0.686) + (Pixel.B * 0.168));
                    int blue = (int)((Pixel.R * 0.272) + (Pixel.G * 0.534) + (Pixel.B * 0.131));

                    red = Math.Min(255, red);
                    green = Math.Min(255, green);
                    blue = Math.Min(255, blue);
                    processed.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            }
            pictureBox2.Image = processed;
        }

        private void loadimage_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog3.FileName);
            pictureBox1.Image = imageA;
        }

        private void selectDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            if (mgaDevice.Length > 0)
            {
                Device d = DeviceManager.GetDevice(0);
                d.ShowWindow(pictureBox1);
            }

        }

        private void closeDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Device d = DeviceManager.GetDevice(0);
            d.Stop();
        }

        private void subtract_Click(object sender, EventArgs e)
        {
            Color mygreen = Color.FromArgb(0, 0, 255);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;
            resultImage = new Bitmap(imageA.Width, imageA.Height);

            for (int x = 0; x < imageB.Width; x++)
            {
                for(int y = 0; y < imageB.Height; y++)
                {
                    Color pixel = imageA.GetPixel(x, y);
                    Color backpixel = imageB.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractvalue = Math.Abs(grey - greygreen);
                    if (subtractvalue < threshold)
                        resultImage.SetPixel(x, y, backpixel);
                    else
                        resultImage.SetPixel(x, y, pixel);
                }
            }
            pictureBox3.Image = resultImage;
        }

        private void loadbackground_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
            pictureBox2.Image = imageB;
        }



    }
}
