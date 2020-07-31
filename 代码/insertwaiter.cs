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
    public partial class insertwaiter : Form
    {
        public insertwaiter()
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

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                String insert = "insert into Waiter(Wname,Wsex,Wphone)";
                insert = insert + " values(@ai,@ei,@ci)";
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(insert, DBHelper.con);
                cmd.Parameters.Add("@ai", SqlDbType.Char).Value = TextBox1.Text.Trim();
                cmd.Parameters.Add("@ei", SqlDbType.Char).Value = comboBox1.Text.Trim();
                cmd.Parameters.Add("@ci", SqlDbType.Char).Value = TextBox2.Text.Trim();
                cmd.ExecuteNonQuery();
                MessageBox.Show("添加成功", "提示");
                更新();
                DBHelper.con.Close();
            }
            else
            {
                MessageBox.Show("服务生姓名不能为空!", "提示");
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
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
    }
}
