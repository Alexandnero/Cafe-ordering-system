using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace index
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void admin_Load(object sender, EventArgs e)
        {
            show();
        }

        public void show()
        {
            try
            {

                string sql = "select * from admin";
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                SqlDataReader reader = cmd.ExecuteReader();
                listView2.Items.Clear();
                while (reader.Read())
                {
                    ListViewItem ite = new ListViewItem(reader["ID"].ToString());
                    listView2.Items.Add(ite);
                    ite.SubItems.AddRange(new string[] {
                reader ["UId"].ToString ()
                });
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBHelper.con.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                listView2.Items.Clear();
                string sql = string.Format("select * from admin where admin={0}", textBox1.Text);
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem ite = new ListViewItem(reader["admin"].ToString());
                    listView2.Items.Add(ite);
                    ite.SubItems.AddRange(new string[] {
                reader ["UId"].ToString ()
                });
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBHelper.con.Close();
            }
        }


    }
}
