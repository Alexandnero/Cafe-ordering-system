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
    public partial class findwaiter : Form
    {
        public findwaiter()
        {
            InitializeComponent();
        }
        private void 更新()
        {
            DataSet setkh = new DataSet();
            string kh = "select WID as 编号,Wname as 服务生姓名,Wsex as 性别,Wphone as 手机号 from Waiter";
            SqlDataAdapter da1 = new SqlDataAdapter(kh, DBHelper.con);
            setkh.Clear();
            da1.Fill(setkh, "Waiter");
            DataGridView1.DataSource = setkh.Tables[0];
            TextBox1.Text = "";
            TextBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            String kh = "select WID as 编号,Wname as 服务生姓名,Wsex as 性别,Wphone as 手机号 from Waiter where 1=1";
            DataSet setkh = new DataSet();
            if (checkBox1.Checked == true)
            {
                kh = kh + " and  WID=" + comboBox2.Text;
            }
            else
            {
                kh = kh + " and 1=1";
            }
            if (checkBox2.Checked == true)
            {
                kh = kh + " and  Wname like '%" + TextBox1.Text + "%'";
            }
            else
            {
                kh = kh + " and 1=1";
            }
            if (checkBox3.Checked == true)
            {
                kh = kh + " and  Wsex like '%" + comboBox1.Text + "%'";
            }
            else
            {
                kh = kh + " and 1=1";
            }
            if (checkBox4.Checked == true)
            {
                kh = kh + " and  Wphone like '%" + TextBox2.Text + "%'";
            }
            else
            {
                kh = kh + " and 1=1";
            }
            SqlDataAdapter da1 = new SqlDataAdapter(kh, DBHelper.con);
            setkh.Clear();
            da1.Fill(setkh, "Waiter");
            DataGridView1.DataSource = setkh.Tables[0];
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                comboBox2.Text = DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                TextBox1.Text = DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                comboBox1.Text = DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                TextBox2.Text = DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "确认", MessageBoxButtons.YesNoCancel);
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            更新();
        }

        private void insertwaiter_Load(object sender, EventArgs e)
        {
            更新();
            SqlDataReader dr;
            DBHelper.con.Open();
            SqlCommand cmd = new SqlCommand("select * from Waiter", DBHelper.con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["WID"].ToString());
            }
            dr.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
