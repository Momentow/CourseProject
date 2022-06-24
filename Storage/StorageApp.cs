using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;

namespace Storage
{
    public partial class StorageApp : Form
    {
        
        public StorageApp()
        {
            InitializeComponent();
            string fileName = "StorageList.json";
            string json = File.ReadAllText(fileName);
            List<Product> list = JsonSerializer.Deserialize<List<Product>>(json);
            DataTable table = new DataTable();
            table.Columns.Add("Type");
            table.Columns.Add("Name");
            table.Columns.Add("Income");
            table.Columns.Add("Outcome");
            table.Columns.Add("Owner");
            foreach (var item in list)
            {
                table.Rows.Add(item.Type, item.Name, item.Income, item.Outcome, item.Owner);
            }
            StorageListViev.DataSource = table;
        }

        private void StorageListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            List<Product> list = new List<Product> { };
            string[,] str = new string[40, 10];
            for (int r = 0; r < StorageListViev.Rows.Count-1; r++)
            {
                for (int c = 0; c < StorageListViev.Columns.Count; c++)
                {
                    var a = StorageListViev.Rows[r].Cells[c].Value;
                    if (a != null)
                    {
                        str[r, c] = a.ToString(); ;
                    }

                }
                list.Add(new Product()
                {
                    Type = str[r, 0],
                    Name = str[r, 1],
                    Income = str[r, 2],
                    Outcome = str[r, 3],
                    Owner = str[r, 4]
                });
            }
            string fileName = "StorageList.json";
            string json = JsonSerializer.Serialize(list);
            File.WriteAllText(fileName, json);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you wish to exit?",
                                               "Exit Confirmation", 
                                               MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.StorageListViev.SelectedRows)
            {
                StorageListViev.Rows.RemoveAt(item.Index);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StorageListViev.Sort(StorageListViev.Columns[2], ListSortDirection.Ascending);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StorageListViev.Sort(StorageListViev.Columns[0], ListSortDirection.Ascending);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StorageListViev.Sort(StorageListViev.Columns[1], ListSortDirection.Ascending);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           StorageListViev.Sort(StorageListViev.Columns[3], ListSortDirection.Ascending);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StorageListViev.Sort(StorageListViev.Columns[4], ListSortDirection.Ascending);
        }
    }
}
