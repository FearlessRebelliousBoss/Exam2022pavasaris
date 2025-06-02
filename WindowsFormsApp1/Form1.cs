using System;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private IWebDriver driver = null;
        private string lastSearchUrl = "";

        public Form1()
        {
            InitializeComponent();

            // Palaid Firefox un atver ebay.com
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("https://www.ebay.com/");

            // Piesaisti pogu klikšķu eventus
            button1.Click += button1_Click; // Search
            button2.Click += button2_Click; // Close browser
            button3.Click += button3_Click; // Back
        }

        // Poga "Search"
        private void button1_Click(object sender, EventArgs e)
        {
            if (driver == null)
            {
                driver = new FirefoxDriver();
                driver.Navigate().GoToUrl("https://www.ebay.com/");
            }

            string searchTerm = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Ievadi meklēšanas frāzi!");
                return;
            }

            try
            {
                // Aizver visas liekās cilnes, ja ir vairāk par vienu
                var tabs = driver.WindowHandles;
                if (tabs.Count > 1)
                {
                    for (int i = tabs.Count - 1; i > 0; i--)
                    {
                        driver.SwitchTo().Window(tabs[i]);
                        driver.Close();
                    }
                    driver.SwitchTo().Window(tabs[0]);
                }

                string searchUrl = $"https://www.ebay.com/sch/i.html?_nkw={Uri.EscapeDataString(searchTerm)}";
                driver.Navigate().GoToUrl(searchUrl);

                System.Threading.Thread.Sleep(2000); // pauze lapas ielādei

                lastSearchUrl = driver.Url;
                textBox2.Text = lastSearchUrl;
                richTextBox1.AppendText(lastSearchUrl + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kļūda meklēšanas laikā: " + ex.Message);
            }
        }

        // Poga "Back"
        private void button3_Click(object sender, EventArgs e)
        {
            if (driver != null)
            {
                try
                {
                    // Aizver visas liekās cilnes, lai atgrieztos uz sākotnējo
                    var tabs = driver.WindowHandles;
                    if (tabs.Count > 1)
                    {
                        for (int i = tabs.Count - 1; i > 0; i--)
                        {
                            driver.SwitchTo().Window(tabs[i]);
                            driver.Close();
                        }
                        driver.SwitchTo().Window(tabs[0]);
                    }

                    // Atver ebay sākumlapu
                    driver.Navigate().GoToUrl("https://www.ebay.com/");
                    System.Threading.Thread.Sleep(2000);

                    // Notīra meklēšanas laukus
                    textBox1.Clear();
                    textBox2.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kļūda atpakaļ navigācijā: " + ex.Message);
                }
            }
        }

        // Poga "Close browser"
        private void button2_Click(object sender, EventArgs e)
        {
            if (driver != null)
            {
                try
                {
                    driver.Quit();
                    driver = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kļūda aizverot pārlūkprogrammu: " + ex.Message);
                }
            }
        }

        // Nodrošina pārlūkprogrammas aizvēršanu, kad forma aizveras
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
            base.OnFormClosing(e);
        }
    }
}
