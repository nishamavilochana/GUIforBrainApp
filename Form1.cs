using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;
using Newtonsoft.Json;
using System.Timers;
using System.Diagnostics;

namespace GUIforBrainApp
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort; int counter = 0; int dataCount; private Thread closingThread;
        public Form1()
        {
            InitializeComponent();
            
            chartallContrib();
            chart1dash();
            serialCom();
        }
        void connectestablish()
        {
            try
            {
                client = new FirebaseClient(ifc); LiveCall();
            }
            catch {
                MessageBox.Show("Connection did not establish!");
            }
        }
        void channelLits()
        {
            List<string> cha1 = new List<string>(); List<string> cha2 = new List<string>(); List<string> cha3 = new List<string>();
            List<string> cha4 = new List<string>(); List<string> cha5 = new List<string>(); List<string> cha6 = new List<string>();
            List<string> cha7 = new List<string>(); List<string> cha8 = new List<string>(); List<string> cha9 = new List<string>();
            List<string> cha10 = new List<string>(); List<string> cha11 = new List<string>(); List<string> cha12 = new List<string>();
            List<string> cha13 = new List<string>(); List<string> cha14 = new List<string>(); List<string> cha15 = new List<string>();
            List<string> cha16 = new List<string>(); 
        }
        IFirebaseClient client;
        String line;
        private void chartallContrib()
        {
            Series series1 = new Series(); series1.Name = "graph17_1"; series1.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series1);
            Series series2 = new Series(); series2.Name = "graph17_2"; series2.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series2);
            Series series3 = new Series(); series3.Name = "graph17_3"; series3.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series3);
            Series series4 = new Series(); series4.Name = "graph17_4"; series4.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series4);
            Series series5 = new Series(); series5.Name = "graph17_5"; series5.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series5);
            Series series6 = new Series(); series6.Name = "graph17_6"; series6.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series6);
            Series series7 = new Series(); series7.Name = "graph17_7"; series7.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series7);
            Series series8 = new Series(); series8.Name = "graph17_8"; series8.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series8);
            Series series9 = new Series(); series9.Name = "graph17_9"; series9.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series9);
            Series series10 = new Series(); series10.Name = "graph17_10"; series10.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series10);
            Series series11 = new Series(); series11.Name = "graph17_11"; series11.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series11);
            Series series12 = new Series(); series12.Name = "graph17_12"; series12.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series12);
            Series series13 = new Series(); series13.Name = "graph17_13"; series13.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series13);
            Series series14 = new Series(); series14.Name = "graph17_14"; series14.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series14);
            Series series15 = new Series(); series15.Name = "graph17_15"; series15.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series15);
            Series series16 = new Series(); series16.Name = "graph17_16"; series16.ChartType = SeriesChartType.Line;
            chart17.Series.Add(series16);
        }

        void serialCom()
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length == 0)
            {
                var result = MessageBox.Show("Waiting for a COM Port Connection!", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    while (ports.Length == 0)
                    { MessageBox.Show("Waiting for a COM Port Connection!");
                        ports = SerialPort.GetPortNames();
                    }
                    availablePorts_box.Items.AddRange(ports); availablePorts_box.SelectedIndex = 0;
                    string[] bauds = { "9600", "19200", "38400", "57600", "115200" };
                    baudrate_box.Items.AddRange(bauds); baudrate_box.SelectedIndex = 0;

                }
                else
                {
                    availablePorts_box.Items.Add("");
                    string[] bauds = { "9600", "19200", "38400", "57600", "115200" };
                    baudrate_box.Items.Add("");
                }
            }
            else
            {
                availablePorts_box.Items.AddRange(ports); availablePorts_box.SelectedIndex = 0;
                string[] bauds = { "9600", "19200", "38400", "57600", "115200" };
                baudrate_box.Items.AddRange(bauds); baudrate_box.SelectedIndex = 0;
            }

        }

        void serialDataRead()
        {
            serialPort = new SerialPort(availablePorts_box.Text, Convert.ToInt32(baudrate_box.Text)); serialPort.Open();
            serialPort.DataReceived += SerialPort_DataReceived; 
        }
        void serialDataReadClose()
        {
            serialPort = new SerialPort(availablePorts_box.Text, Convert.ToInt32(baudrate_box.Text));
            //serialPort.DataReceived += SerialPort_DataReceived; //serialPort.Open();
            serialPort.Close();
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                /*string data = serialPort.ReadLine();
                apstin.Add(data);
                //for 16 channels
                string[] words = data.Split(',');
                if (words.Length >= 16)
                {
                    for (int k = 0; k < 16; k++)
                    {
                        Invoke(new Action(() => UpdateTextBox(words[k])));
                        Invoke(new Action(() => UpdateChart(words[k], k + 1)));
                    }
                }
                else
                {
                    Console.Write("Bruh!");
                }*/
                //for 8 channels
                string data = serialPort.ReadLine();
                apstin.Add(data);
                string[] words = data.Split(','); 
                 if (words.Length >= 6)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        Invoke(new Action(() => UpdateTextBox(words[k])));
                        Invoke(new Action(() => UpdateChart(words[k], k + 1)));
                    }
                }
                else
                {
                    Console.Write("Bruh!");
                }
                 
                //For 1 channel

                /*
                 Invoke(new Action(() => UpdateTextBox(data)));
                 Invoke(new Action(() => UpdateChart(data, 1)));
                */
            }
            catch
            {
                Console.WriteLine("Bruh");
            }
            
            
             

        }
        List<string> apstin = new List<string>();
        int jk = 0;
        private void fileRead()
        {
            if (filePath == "")
            {

                MessageBox.Show("Select a file in Settings -> Local Visualization Settings");
            }
            else
            {
                StreamReader sr = new StreamReader(filePath);
                line = sr.ReadLine();

                
                while (line != null)
                {
                    //write the line to console window
                    jk++; apstin.Add(line);
                    string[] words = line.Split(',');
                    if (words.Length == 16)
                    {
                        Invoke(new Action(() => UpdateTextBox(jk.ToString())));
                        for (int k = 0; k < 16; k++)
                        {

                            Invoke(new Action(() => UpdateChart(words[k], k + 1)));

                        }

                    }
                    else
                    {
                        Console.Write("Bruh!");
                    }
                    Thread.Sleep(100);
                    line = sr.ReadLine();
                    if (cancellationTokenSource.IsCancellationRequested)
                    {
                        
                        break;
                    }
                }
            }

        }
        private void chart1dash()
        {


            Series emptySeries = new Series(); emptySeries.Name = "Empty Series";

            chart1.Series.Add(emptySeries); Title chartTitle1 = new Title();
            chartTitle1.Text = "Channel 1"; chartTitle1.ForeColor = Color.Black; chartTitle1.Alignment = ContentAlignment.TopCenter;
            chart1.Titles.Add(chartTitle1); chart1.Series[0].BorderWidth = 3;

            chart2.Series.Add(emptySeries); Title chartTitle2 = new Title(); chartTitle2.Text = "Channel 2";
            chartTitle2.ForeColor = Color.Black; chartTitle2.Alignment = ContentAlignment.TopCenter;
            chart2.Titles.Add(chartTitle2); chart2.Series[0].BorderWidth = 3; chart2.BorderlineColor = Color.Black;

            chart3.Series.Add(emptySeries); Title chartTitle3 = new Title(); chartTitle3.Text = "Channel 3";
            chartTitle3.ForeColor = Color.Black; chartTitle3.Alignment = ContentAlignment.TopCenter;
            chart3.Titles.Add(chartTitle3); chart3.Series[0].BorderWidth = 3;

            chart4.Series.Add(emptySeries); Title chartTitle4 = new Title(); chartTitle4.Text = "Channel 4";
            chartTitle4.ForeColor = Color.Black; chartTitle4.Alignment = ContentAlignment.TopCenter;
            chart4.Titles.Add(chartTitle4); chart4.Series[0].BorderWidth = 3;

            chart5.Series.Add(emptySeries); Title chartTitle5 = new Title(); chartTitle5.Text = "Channel 5";
            chartTitle5.ForeColor = Color.Black; chartTitle5.Alignment = ContentAlignment.TopCenter;
            chart5.Titles.Add(chartTitle5); chart5.Series[0].BorderWidth = 3;

            chart6.Series.Add(emptySeries); Title chartTitle6 = new Title(); chartTitle6.Text = "Channel 6";
            chartTitle6.ForeColor = Color.Black; chartTitle6.Alignment = ContentAlignment.TopCenter;
            chart6.Titles.Add(chartTitle6); chart6.Series[0].BorderWidth = 3;

            chart7.Series.Add(emptySeries); Title chartTitle7 = new Title(); chartTitle7.Text = "Channel 7";
            chartTitle7.ForeColor = Color.Black; chartTitle7.Alignment = ContentAlignment.TopCenter;
            chart7.Titles.Add(chartTitle7); chart7.Series[0].BorderWidth = 3;

            chart8.Series.Add(emptySeries); Title chartTitle8 = new Title(); chartTitle8.Text = "Channel 8";
            chartTitle8.ForeColor = Color.Black; chartTitle8.Alignment = ContentAlignment.TopCenter;
            chart8.Titles.Add(chartTitle8); chart8.Series[0].BorderWidth = 3;

            chart9.Series.Add(emptySeries); Title chartTitle9 = new Title(); chartTitle9.Text = "Channel 9";
            chartTitle9.ForeColor = Color.Black; chartTitle9.Alignment = ContentAlignment.TopCenter;
            chart9.Titles.Add(chartTitle9); chart9.Series[0].BorderWidth = 3;

            chart10.Series.Add(emptySeries); Title chartTitle10 = new Title(); chartTitle10.Text = "Channel 10";
            chartTitle10.ForeColor = Color.Black; chartTitle10.Alignment = ContentAlignment.TopCenter;
            chart10.Titles.Add(chartTitle10); chart10.Series[0].BorderWidth = 3;

            chart11.Series.Add(emptySeries); Title chartTitle11 = new Title(); chartTitle11.Text = "Channel 11";
            chartTitle11.ForeColor = Color.Black; chartTitle11.Alignment = ContentAlignment.TopCenter;
            chart11.Titles.Add(chartTitle11); chart11.Series[0].BorderWidth = 3;

            chart12.Series.Add(emptySeries); Title chartTitle12 = new Title(); chartTitle12.Text = "Channel 12";
            chartTitle12.ForeColor = Color.Black; chartTitle12.Alignment = ContentAlignment.TopCenter;
            chart12.Titles.Add(chartTitle12); chart12.Series[0].BorderWidth = 3;

            chart13.Series.Add(emptySeries); Title chartTitle13 = new Title(); chartTitle13.Text = "Channel 13";
            chartTitle13.ForeColor = Color.Black; chartTitle13.Alignment = ContentAlignment.TopCenter;
            chart13.Titles.Add(chartTitle13); chart13.Series[0].BorderWidth = 3;

            chart14.Series.Add(emptySeries); Title chartTitle14 = new Title(); chartTitle14.Text = "Channel 14";
            chartTitle14.ForeColor = Color.Black; chartTitle14.Alignment = ContentAlignment.TopCenter;
            chart14.Titles.Add(chartTitle14); chart14.Series[0].BorderWidth = 3;

            chart15.Series.Add(emptySeries); Title chartTitle15 = new Title(); chartTitle15.Text = "Channel 15";
            chartTitle15.ForeColor = Color.Black; chartTitle15.Alignment = ContentAlignment.TopCenter;
            chart15.Titles.Add(chartTitle15); chart15.Series[0].BorderWidth = 3;

            chart16.Series.Add(emptySeries); Title chartTitle16 = new Title(); chartTitle16.Text = "Channel 16";
            chartTitle16.ForeColor = Color.Black; chartTitle16.Alignment = ContentAlignment.TopCenter;
            chart16.Titles.Add(chartTitle16); chart16.Series[0].BorderWidth = 3;

            chart17.Series.Add(emptySeries); Title chartTitle17 = new Title(); chartTitle17.Text = "All Channels";
            chartTitle17.ForeColor = Color.Black; chartTitle17.Alignment = ContentAlignment.TopCenter;
            chart17.Titles.Add(chartTitle17); chart17.Series[0].BorderWidth = 2;

            chart18.Series[0].BorderWidth = 3; chart18.Series[0].ChartType = SeriesChartType.Line;



        }

        private void UpdateChart(string data, int g)
        {
            // Update the chart with the received data
            double value;
            List<string> cha1 = new List<string>(); List<string> cha2 = new List<string>(); List<string> cha3 = new List<string>();
            List<string> cha4 = new List<string>(); List<string> cha5 = new List<string>(); List<string> cha6 = new List<string>();
            List<string> cha7 = new List<string>(); List<string> cha8 = new List<string>(); List<string> cha9 = new List<string>();
            List<string> cha10 = new List<string>(); List<string> cha11 = new List<string>(); List<string> cha12 = new List<string>();
            List<string> cha13 = new List<string>(); List<string> cha14 = new List<string>(); List<string> cha15 = new List<string>();
            List<string> cha16 = new List<string>();
            if (double.TryParse(data, out value))
            {

                if (g == 1)
                {
                    chart1.Series[0].Points.AddXY(dataCount, value); dataCount++;
                    cha1.Add(value.ToString());
                    chart1.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart1.ChartAreas[0].AxisY.Maximum = value + 100;


                    chart17.Series["graph17_1"].Points.AddXY(dataCount, value); chart17.Refresh();

                    chart1.Refresh();

                }
                else if (g == 2)
                {
                    chart2.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha2.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart2.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart2.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_2"].Points.AddXY(dataCount, value); chart17.Refresh();
                    // Refresh the chart
                    chart2.Refresh();
                }
                else if (g == 3)
                {
                    chart3.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha3.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart3.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart3.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_3"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart3.Refresh();
                }
                else if (g == 4)
                {
                    chart4.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha4.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart4.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart4.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_4"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart4.Refresh();
                }
                else if (g == 5)
                {
                    chart5.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha5.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart5.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart5.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_5"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart5.Refresh();
                }
                else if (g == 6)
                {
                    chart6.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha6.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart6.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart6.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_6"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart6.Refresh();
                }

                else if (g == 7)
                {

                    chart7.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha7.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart7.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart7.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_7"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart7.Refresh();
                }
                else if (g == 8)
                {
                    chart8.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha8.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart8.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart8.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_8"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart8.Refresh();
                }
                else if (g == 9)
                {
                    chart9.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha9.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart9.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart9.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_9"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart9.Refresh();
                }
                else if (g == 10)
                {
                    chart10.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha10.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart10.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart10.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_10"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart10.Refresh();
                }
                else if (g == 11)
                {
                    chart11.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha11.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart11.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart11.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_11"].Points.AddXY(dataCount, value); chart17.Refresh();
                }
                else if (g == 12)
                {
                    chart12.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha12.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart12.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart12.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_12"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart12.Refresh();
                }
                else if (g == 13)
                {
                    chart13.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha13.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart13.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart13.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_13"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart13.Refresh();
                }
                else if (g == 14)
                {
                    chart14.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha14.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart14.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart14.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_14"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart14.Refresh();
                }
                else if (g == 15)
                {
                    chart15.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha15.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart15.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart15.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_15"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart15.Refresh();
                }
                else if (g == 16)
                {
                    chart16.Series[0].Points.AddXY(dataCount, value);
                    dataCount++;
                    cha16.Add(value.ToString());
                    // Adjust the X-axis range based on the number of data points
                    chart16.ChartAreas[0].AxisY.Minimum = value - 100;
                    chart16.ChartAreas[0].AxisY.Maximum = value + 100;
                    chart17.Series["graph17_16"].Points.AddXY(dataCount, value); chart17.Refresh();
                    chart16.Refresh();
                }
            }
        }

        private void UpdateTextBox(string data)
        {
            // Append the received data to the TextBox control
            textBox1.AppendText(data + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            counter++;
            if (counter == 1)
            {
                button1.Text = "Disconnect"; serialDataRead();
            }
            else
            {
                counter = 0;
                Cursor = Cursors.WaitCursor;
                closingThread = new Thread(closeSerial);
                closingThread.Start();
                Cursor = Cursors.Default;
                button1.Text = "Connect";
            }


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {


        }
        private void closeSerial()
        {
            serialPort.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Close the serial port when the form is closing
            /*if(serialPort.Open)
            {
                serialPort.Close();
            }*/


            base.OnFormClosing(e);
        }




        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Todaysdate = DateTime.Now.ToString("-dd-MM-yyyy-(hh-mm-ss)");
            string subDirectoryPath = Path.Combine(richTextBox2.Text, Todaysdate,"Raw_Dataset.txt");
            Directory.CreateDirectory(subDirectoryPath);
            string filepaths = DateTime.Now.ToString("-dd-MM-yyyy-(hh-mm-ss)");
            System.IO.File.AppendAllLines(subDirectoryPath+ ".txt",apstin);
            for (int i = 1; i < 18; i++)
            {

                string filePath = subDirectoryPath + "Channel" + i + "chart.png";
                ChartImageFormat format = ChartImageFormat.Png;
                switch (i)
                {
                    case 1:
                        chart1.SaveImage(filePath, format);
                        break;
                    case 2:
                        chart2.SaveImage(filePath, format);
                        break;
                    case 3:
                        chart3.SaveImage(filePath, format);
                        break;
                    case 4:
                        chart4.SaveImage(filePath, format);
                        break;
                    case 5:
                        chart5.SaveImage(filePath, format);
                        break;
                    case 6:
                        chart6.SaveImage(filePath, format);
                        break;
                    case 7:
                        chart7.SaveImage(filePath, format);
                        break;
                    case 8:
                        chart8.SaveImage(filePath, format);
                        break;
                    case 9:
                        chart9.SaveImage(filePath, format);
                        break;
                    case 10:
                        chart10.SaveImage(filePath, format);
                        break;
                    case 11:
                        chart11.SaveImage(filePath, format);
                        break;
                    case 12:
                        chart12.SaveImage(filePath, format);
                        break;
                    case 13:
                        chart13.SaveImage(filePath, format);
                        break;
                    case 14:
                        chart14.SaveImage(filePath, format);
                        break;
                    case 15:
                        chart15.SaveImage(filePath, format);
                        break;
                    case 16:
                        chart16.SaveImage(filePath, format);
                        break;
                    case 17:
                        chart17.SaveImage(filePath, format);
                        break;


                }

            }
            Directory.Delete(subDirectoryPath, true);
            MessageBox.Show("Successfully Saved 18 Images and RAW Dataset!");
        }

        private void OpenDirectory(string directoryPath)
        {
            Process.Start("explorer.exe", directoryPath);
        }
        int cunt = 0; private CancellationTokenSource cancellationTokenSource;
        private async void button3_Click(object sender, EventArgs e)
        {
            cunt++;
            cancellationTokenSource = new CancellationTokenSource();
            if (cunt == 1)
            {
                //button3.Enabled = false;
                await Task.Run(() =>
                {
                    button3.BackColor = Color.Red;
                    fileRead();
                });
            }
            else
            {
                cunt = 0; button3.BackColor = Color.Transparent;
                cancellationTokenSource.Cancel();

            }
        }

        private void chart1_MouseEnter(object sender, EventArgs e)
        {

            chart1.BackColor = Color.Beige;

        }
        private void chart1_MouseLeave(object sender, EventArgs e)
        {

            chart1.BackColor = Color.Transparent;
        }

        int cunt3 = 0;
        private async void button4_Click(object sender, EventArgs e)
        {
            cunt3++; cancellationTokenSource = new CancellationTokenSource(); Cursor = Cursors.WaitCursor;Thread.Sleep(500); Cursor = Cursors.Default;
            if (cunt3 == 1)
            {
                await Task.Run(() =>
                {
                    connectestablish();
                });

            }
            else
            {
                cunt3 = 0; cancellationTokenSource.Cancel();
                MessageBox.Show("FirebasePlot Stopped");
            }

        }
        int cunt2 = 0;
        async void LiveCall()
        {
            while (true)
            {
                
                FirebaseResponse res = await client.GetAsync(@"server/EEG_Data/" + cunt2); 
                string data = JsonConvert.DeserializeObject<string>(res.Body.ToString());
                apstin.Add(data);
                UpdateRTB(data); cunt2++;

                if (cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }
            }

        }
        void UpdateRTB(string record)
        {
            Invoke(new Action(() => UpdateTextBox(record)));
            Invoke(new Action(() => UpdateTextBox(" ")));
            string[] words = record.Split(',');
            if (words.Length == 16)
            {

                for (int k = 0; k < 16; k++)
                {

                    Invoke(new Action(() => UpdateChart(words[k], k + 1)));

                }

            }
            else
            {
                Console.Write("Bruh!");
            }
        }

        private void chart32_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            fileOpener();
        }
        String filePath = string.Empty;
        void fileOpener()
        {
            
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    richTextBox1.Clear();
                    richTextBox1.AppendText(filePath);
                    Cursor = Cursors.WaitCursor;Thread.Sleep(100); Cursor = Cursors.Default;
                }
            }

        }

        string AUTHKEYFIRE;
        string BASEPATHFIRE;
        private void button6_Click(object sender, EventArgs e)
        {
            if (AUTHKEYFIRE != null && BASEPATHFIRE != null)
            {
                MessageBox.Show("Under Construction!!!!!");
            }
            else
            {
                MessageBox.Show("Under Construction!!!!!");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            AUTHKEYFIRE = textBox2.Text;
        }
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "x9fhGsffBAnhPpgHesjDWQgkT90tNDt5FKBz0Kxh",
            BasePath = "https://brainees-feb4d-default-rtdb.firebaseio.com/"
        };
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            BASEPATHFIRE = textBox3.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            System.Windows.Forms.Application.Restart();
            Cursor = Cursors.Default;  
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 1)
            {
                chart18.Series.Clear();
                Series series1 = new Series();
                chart18.Series.Add(series1);
                chart18.Series[0].ChartType = SeriesChartType.Line;
            }
            else
            {
                Console.WriteLine("Bruh!");
            }
        }

        private void chart1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);

            foreach (Series series in chart1.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart2.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart3.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart4.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart5.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart6.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart7.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart8.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart9.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart10.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }
        private void chart11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart11.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart12_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart12.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart13_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart13.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart14.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart15.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart16_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart16.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void chart17_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            foreach (Series series in chart17.Series)
            {
                try
                {
                    chart18.Series.Add(series.Name);
                }
                catch
                {
                    foreach (DataPoint dataPoint in series.Points)
                    {

                        chart18.Series[series.Name].Points.AddY(dataPoint.YValues[0]);
                        ChartArea CA = chart18.ChartAreas[0];
                        CA.AxisX.ScaleView.Zoomable = true;
                        CA.AxisY.ScaleView.Zoomable = true;
                        CA.CursorX.AutoScroll = true;
                        CA.CursorY.AutoScroll = true;
                        CA.CursorX.IsUserSelectionEnabled = true;
                        CA.CursorY.IsUserSelectionEnabled = true;
                        chart18.ChartAreas[0].AxisY.Minimum = dataPoint.YValues[0] - 100;
                        chart18.ChartAreas[0].AxisY.Maximum = dataPoint.YValues[0] + 100;
                        chart18.Refresh();
                    }
                }
                chart18.Refresh();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string selectedFolder = folderBrowserDialog.SelectedPath;
                    richTextBox2.Clear();
                    richTextBox2.AppendText(selectedFolder);
                    Cursor = Cursors.WaitCursor; Thread.Sleep(100); Cursor = Cursors.Default;
                }
            }
        }

    }
    }
    

/*I got zero clue Whats happening with this App!!1*/
    
    
    
    
    
    


