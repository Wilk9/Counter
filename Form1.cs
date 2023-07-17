using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace Counter
{
    public partial class Form1 : Form
    {
        const string JsonPath = "count.json";

        private string currentNumber = "0";
        TextBox? textBox = null;

        public Form1()
        {
            InitializeComponent();

            var text = File.ReadAllText(JsonPath);
            Counter counter = JsonConvert.DeserializeObject<Counter>(text)!;

            currentNumber = counter.count.ToString();

            this.MinimumSize = new Size(140, 240);
            this.MaximumSize = new Size(140, 240);

            textBox = new TextBox();
            this.Controls.Add(textBox);
            textBox.Text = currentNumber;
            textBox.Size = new Size(120, 60);
            textBox.Left = (this.ClientSize.Width - textBox.Width) / 2;
            textBox.Enabled = false;
            textBox.Font = new Font("Arial", 30, FontStyle.Regular);
            textBox.TextAlign = HorizontalAlignment.Center;

            Button button = new Button();
            this.Controls.Add(button);
            button.Top = textBox.Bottom;
            button.Size = new Size(120, 120);
            button.Text = "+";
            button.Left = (this.ClientSize.Width - button.Width) / 2;
            button.Click += new EventHandler(button_Click);
            button.Font = new Font("Arial", 30, FontStyle.Regular);
        }

        private void button_Click(object sender, EventArgs e)
        {
            var number = int.Parse(currentNumber);

            number += 1;

            var json = JsonConvert.SerializeObject(new Counter()
            {
                count = number,
            });
            File.WriteAllText(JsonPath, json);

            currentNumber = number.ToString();
            textBox!.Text = currentNumber;
        }
    }

    public class Counter
    {
        public int count { get; set; }
    }
}