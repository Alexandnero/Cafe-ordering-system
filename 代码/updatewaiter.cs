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
    public partial class updatewaiter : Form
    {
        public updatewaiter()
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
            TextBox3.Text = "";
            comboBox1.Text = "";

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox3.Text != "")
            {
                String update = "update Waiter set Wname=@ai,Wsex=@ei,Wphone=@ci";
                update = update + " where WID='" + TextBox3.Text.Trim() + "'";
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(update, DBHelper.con);
                cmd.Parameters.Add("@ai", SqlDbType.Char).Value = TextBox1.Text.Trim();
                cmd.Parameters.Add("@ei", SqlDbType.Char).Value = comboBox1.Text.Trim();
                cmd.Parameters.Add("@ci", SqlDbType.Char).Value = TextBox2.Text.Trim();
                cmd.ExecuteNonQuery();
                MessageBox.Show("修改成功", "提示");
                更新();
                DBHelper.con.Close();
            }
            else
            {
                MessageBox.Show("服务生编号不能为空!", "提示");
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TextBox3.Text = DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
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
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox3.Text != "")
            {
                String delete = "delete from Waiter";
                delete = delete + " where WID='" + TextBox3.Text.Trim() + "'";
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(delete, DBHelper.con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("删除成功", "提示");
                更新();
                DBHelper.con.Close();
            }
            else
            {
                MessageBox.Show("服务生编号不能为空!", "提示");
            }
        }
    }
}
