using System;
using System.Windows.Forms;
using System.IO;

namespace AutoAbstandsWarner
{
    public partial class Form1 : Form
    {
        string file = "../../sensoren.txt";
        public Form1()
        {
            InitializeComponent();
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            StreamWriter sw = File.AppendText(file);
            sw.Write(txtInput.Text + Environment.NewLine);
            sw.Close();
            txtInput.Text = "";
        }
        private void btnCalc_Click(object sender, EventArgs e)
        {
            StreamReader sr = File.OpenText(file);
            String[] readLine = sr.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            sr.Close();
            lblOutput.Text = CalcOutput(readLine);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Ariks Auto Abstands Rechner";
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private String CalcOutput(String[] input)
        {
            String SensorName = "";
            String Output = "";
            for (int i = 0; i <= input.Length - 2; i++)
            {
                String[] inputs = input[i].Split(';');
                if (inputs[0] == "SL")
                {
                    SensorName = "Sensor Links";
                }
                else if (inputs[0] == "SM")
                {
                    SensorName = "Sensor Mitte";
                }
                else if (inputs[0] == "SR")
                {
                    SensorName = "Sensor Rechts";
                }
                String[] Date = inputs[1].Split('-');
                String Datum = Date[2] + "." + Date[1] + "." + Date[0];
                String Uhrzeit = inputs[2];
                double Geschwindigkeit = Convert.ToDouble(inputs[3]);
                double Abstand = Convert.ToDouble(inputs[4].Replace(".", ","));
                String AbstandGenug = "Genügend Abstand gehalten";
                if (Geschwindigkeit / 2.0 > Abstand)
                {
                    AbstandGenug = "Zu Wenig Abstand" +
                        "\nBenötigter Abstand: " + Geschwindigkeit / 2 + " Meter" +
                        "\nAktueller Abstand: " + Abstand + " Meter";
                    if (chbShAll.Checked)
                    {
                        Output += "Messung " + (i + 1) + ": \nSensor: " + SensorName + "\n"
                        + "Datum: " + Datum
                        + "\nUhrzeit: " + Uhrzeit
                        + "\nGeschwindigkeit: " + Geschwindigkeit + " km/h"
                        + "\nAbstand: " + Abstand + " Meter"
                        + "\n" + AbstandGenug + "\n\n";
                    }
                }
                if (!chbShAll.Checked)
                {
                    Output += "Messung " + (i + 1) + ": \nSensor: " + SensorName + "\n"
                    + "Datum: " + Datum
                    + "\nUhrzeit: " + Uhrzeit
                    + "\nGeschwindigkeit: " + Geschwindigkeit + " km/h"
                    + "\nAbstand: " + Abstand + " Meter"
                    + "\n" + AbstandGenug + "\n\n";
                }
            }
            return Output;
        }
    }
}
