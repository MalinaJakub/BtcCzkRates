using BtcCzkRates.Data;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace BtcCzkRates
{
    public partial class MasterForm : Form
    {
        DatabaseHelper db;
        DateTime nextDownloadFromCnb;
        decimal cnbRate = 25.0M;
        public MasterForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
            nextDownloadFromCnb = DateTime.Today.AddDays(-1);
        }

        private void MasterForm_Load(object sender, EventArgs e)
        {
            // Nastaven� DataGridView
            savedDataGridView.AutoGenerateColumns = false; // Zak�z�n� automatick� generace sloupc�

            // P�edpokl�dan� sloupce
            savedDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id", ReadOnly = true });
            savedDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Timestamp", HeaderText = Localization.Timestamp, DataPropertyName = "Timestamp", ReadOnly = true });
            savedDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "PriceEUR", HeaderText = Localization.PriceEUR, DataPropertyName = "PriceEUR", ReadOnly = true });
            savedDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "PriceCZK", HeaderText = Localization.PriceCZK, DataPropertyName = "PriceCZK", ReadOnly = true });
            savedDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Note", HeaderText = Localization.Note, DataPropertyName = "Note", ReadOnly = false });

            // P�id�n� tla��tek pro aktualizaci a smaz�n�
            DataGridViewButtonColumn updateButtonColumn = new DataGridViewButtonColumn();
            updateButtonColumn.HeaderText = Localization.Update;
            updateButtonColumn.Name = Localization.Update;
            updateButtonColumn.Text = Localization.Save;
            updateButtonColumn.UseColumnTextForButtonValue = true;
            savedDataGridView.Columns.Add(updateButtonColumn);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.HeaderText = Localization.Delete;
            deleteButtonColumn.Text = Localization.Delete;
            deleteButtonColumn.Name = Localization.Delete;
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            savedDataGridView.Columns.Add(deleteButtonColumn);

            savedDataGridView.CellContentClick += dataGridView1_CellContentClick;
            savedDataGridView.AllowUserToAddRows = false;

            liveDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Timestamp", HeaderText = Localization.Timestamp, DataPropertyName = "Timestamp", ReadOnly = true });
            liveDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "PriceEUR", HeaderText = Localization.PriceEUR, DataPropertyName = "PriceEUR", ReadOnly = true });
            liveDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "PriceCZK", HeaderText = Localization.PriceCZK, DataPropertyName = "PriceCZK", ReadOnly = true });
            liveDataGridView.AllowUserToAddRows = false;

            timer1.Start();

            LoadDbData(); // Na�ten� dat do DataGridView
        }

        /// <summary>
        /// Z�sk�n� ceny BTC/EUR
        /// </summary>
        /// <returns></returns>
        public async Task<Tuple<decimal, decimal>> GetBitcoinPriceAsync()
        {
            if (nextDownloadFromCnb < DateTime.Now)
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync("https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt");
                    var lines = response.Split('\n');
                    for (int i = 2; i < lines.Length; i++)
                    {
                        var line = lines[i].Split("|");
                        if (line.Length >= 5 && line[3] == "EUR")
                        {
                            cnbRate = decimal.Parse(line[4]) / decimal.Parse(line[2]);
                            break;
                        }
                    }
                }
                if (DateTime.Now.TimeOfDay < new TimeSpan(14, 30, 0))
                {
                    nextDownloadFromCnb = DateTime.Today.AddHours(14).AddMinutes(30);
                }
                else
                {
                    nextDownloadFromCnb = DateTime.Today.AddDays(1).AddHours(14).AddMinutes(30);
                }
                
            }
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://data-api.coindesk.com/spot/v1/latest/tick?market=coinbase&instruments=BTC-EUR");
                var json = JObject.Parse(response);
                decimal priceEUR = (decimal)json["Data"]["BTC-EUR"]["PRICE"];
                decimal priceCZK = priceEUR * cnbRate;
                return new Tuple<decimal, decimal>(priceEUR, priceCZK);
            }
        }

        private void LoadDbData()
        {
            // Na�ten� dat z datab�ze
            List<BitcoinData> bitcoinDataList = db.LoadData();

            // Vyma�te aktu�ln� obsah DataGridView
            savedDataGridView.Rows.Clear();

            // Napln�n� DataGridView
            foreach (var data in bitcoinDataList)
            {
                savedDataGridView.Rows.Add(data.Id, data.Timestamp, data.PriceEUR, data.PriceCZK, data.Note);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kontrola, zda byla kliknuta bu�ka s tla��tkem pro aktualizaci
            if (e.ColumnIndex == savedDataGridView.Columns[Localization.Update].Index && e.RowIndex >= 0)
            {
                // Z�sk�n� ID z�znamu a pozn�mky
                int id = Convert.ToInt32(savedDataGridView.Rows[e.RowIndex].Cells["Id"].Value);
                string note = savedDataGridView.Rows[e.RowIndex].Cells["Note"].Value?.ToString();

                // Aktualizace pozn�mky v datab�zi
                db.UpdateNoteById(id, note);
                MessageBox.Show(Localization.NoteWasUpdated);

                LoadDbData(); // Obnoven� dat v DataGridView
            }

            // Kontrola, zda byla kliknuta bu�ka s tla��tkem pro smaz�n�
            if (e.ColumnIndex == savedDataGridView.Columns[Localization.Delete].Index && e.RowIndex >= 0)
            {
                // Z�sk�n� ID z�znamu
                int id = Convert.ToInt32(savedDataGridView.Rows[e.RowIndex].Cells["Id"].Value);

                // Maz�n� z�znamu z datab�ze
                db.DeleteData(id);
                MessageBox.Show(Localization.RecordWasDeleted);

                LoadDbData(); // Obnoven� dat v DataGridView
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var prices = await GetBitcoinPriceAsync();
            db.SaveData(prices.Item1, prices.Item2);
            LoadDbData();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            var prices = await GetBitcoinPriceAsync();
            liveDataGridView.Rows.Add(DateTime.Now,prices.Item1, prices.Item2);

            liveDataGridView.FirstDisplayedScrollingRowIndex = liveDataGridView.RowCount - 1;
        }
    }
}
