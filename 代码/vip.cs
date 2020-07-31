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
    public partial class vip : Form
    {
        public vip()
        {
            InitializeComponent();
        }

        private void 增加会员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insertvip insert = new insertvip(this);
            
            insert.ShowDialog();
        }

        private void 会员充值ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            vipchongzhi vip = new vipchongzhi(this);
            vip.ShowDialog();
        }



        public void vip_Load(object sender, EventArgs e)
        {
            show();
        }

        public void show()
        {
            try
            {
                string sql = "select * from VIp";
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                SqlDataReader reader = cmd.ExecuteReader();
                listView1.Items.Clear();
                while (reader.Read())
                {
                    ListViewItem ite = new ListViewItem(reader["VIP"].ToString());
                    listView1.Items.Add(ite);
                    ite.SubItems.AddRange(new string[] { 
                reader ["Vipprice"].ToString ()
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
                if (textBox1.Text != null&&textBox1.Text !="")
                {
                    listView1.Items.Clear();
                    string sql = string.Format("select * from VIp where vip={0}", textBox1.Text.Trim());
                    DBHelper.con.Open();
                    SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem ite = new ListViewItem(reader["VIP"].ToString());
                        listView1.Items.Add(ite);
                        ite.SubItems.AddRange(new string[] {
                reader ["Vipprice"].ToString ()
                });
                    }
                    reader.Close();
                }
                else
                {

                }
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

        private void 查找会员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}