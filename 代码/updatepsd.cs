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
    public partial class updatepsd : Form
    {
        public updatepsd()
        {
            InitializeComponent();
        }
        private void 更新()
        {
            DataSet setkh = new DataSet();
            string kh = "select UId as 账号,PassWord as 密码,Role as 权限 from admin";
            SqlDataAdapter da1 = new SqlDataAdapter(kh, DBHelper.con);
            setkh.Clear();
            da1.Fill(setkh, "admin");
            DataGridView1.DataSource = setkh.Tables[0];
            TextBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";

        }
        

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                comboBox2.Text = DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                TextBox1.Text = DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                comboBox1.Text = DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                
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
            SqlCommand cmd = new SqlCommand("select * from admin", DBHelper.con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["UId"].ToString());
            }
            dr.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text!=""&&TextBox1.Text != ""&&comboBox1.Text != "")
            {
                String insert = "insert into admin(UId,PassWord,Role)";
                insert = insert + " values(@ai,@ei,@ci)";
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(insert, DBHelper.con);
                cmd.Parameters.Add("@ai", SqlDbType.Char).Value = comboBox2.Text.Trim();
                cmd.Parameters.Add("@ei", SqlDbType.Char).Value = TextBox1.Text.Trim();
                cmd.Parameters.Add("@ci", SqlDbType.Char).Value = comboBox1.Text.Trim();
                cmd.ExecuteNonQuery();
                MessageBox.Show("添加成功", "提示");
                更新();
                DBHelper.con.Close();
            }
            else
            {
                MessageBox.Show("账号、密码、权限不能为空!", "提示");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "" && TextBox1.Text != "" && comboBox1.Text != "")
            {
                String update = "update admin set UId=@ai,PassWord=@ei,Role=@ci";
                update = update + " where UId='" + comboBox2.Text.Trim() + "'";
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(update, DBHelper.con);
                cmd.Parameters.Add("@ai", SqlDbType.Char).Value = comboBox2.Text.Trim();
                cmd.Parameters.Add("@ei", SqlDbType.Char).Value = TextBox1.Text.Trim();
                cmd.Parameters.Add("@ci", SqlDbType.Char).Value = comboBox1.Text.Trim();
                cmd.ExecuteNonQuery();
                MessageBox.Show("修改成功", "提示");
                更新();
                DBHelper.con.Close();
            }
            else
            {
                MessageBox.Show("账号、密码、权限不能为空!", "提示");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                String delete = "delete from admin";
                delete = delete + " where UId='" + comboBox2.Text.Trim() + "'";
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(delete, DBHelper.con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("删除成功", "提示");
                更新();
                DBHelper.con.Close();
            }
            else
            {
                MessageBox.Show("账号不能为空!", "提示");
            }
        }
    }
}
