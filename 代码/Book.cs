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
    public partial class Book : Form
    {
        public Book()
        {
            InitializeComponent();
        }
        private void 更新()
        {
            DataSet setkh = new DataSet();
            string kh = "select BookID as 预约编号,BookVIP as 会员号,BookTable as 桌号,BookTime as 预约时间,InvalidTime as 失效时间 from Book";
            SqlDataAdapter da1 = new SqlDataAdapter(kh, DBHelper.con);
            setkh.Clear();
            da1.Fill(setkh, "Book");
            DataGridView1.DataSource = setkh.Tables[0];
            comboBox1.Text = "";
            comboBox2.Text = "";
            
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != ""&& comboBox2.Text != "")
            {
                String insert = "insert into Book(BookVIP,BookTable,BookTime,InvalidTime)";
                insert = insert + " values(@ai,@ei,@ci,@di)";
                String update = "update TableNumber set Tablebook='true' where TableId=" + comboBox2.Text.ToString().Trim();
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(insert, DBHelper.con);
                cmd.Parameters.Add("@ai", SqlDbType.Char).Value = comboBox1.Text.Trim();
                cmd.Parameters.Add("@ei", SqlDbType.Char).Value = comboBox2.Text.Trim();
                cmd.Parameters.Add("@ci", SqlDbType.Char).Value = System.DateTime.Now;
                cmd.Parameters.Add("@di", SqlDbType.Char).Value = System.DateTime.Now.AddMinutes(+30);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand(update, DBHelper.con);
                cmd1.ExecuteNonQuery();
                MessageBox.Show("预约座位成功", "提示");
                DBHelper.con.Close();
                更新();
                comboBox2.Items.Clear();
                SqlDataReader dr2;
                DBHelper.con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from TableNumber where Tablefettle='false' and Tablebook<>'true'", DBHelper.con);
                dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    comboBox2.Items.Add(dr2["TableId"].ToString());
                }
                dr2.Close();
                DBHelper.con.Close();
                MainFrom.timetf = true;
            }
            else
            {
                MessageBox.Show("会员、桌号不能为空!", "提示");
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                comboBox1.Text = DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                comboBox2.Text = DataGridView1.SelectedRows[0].Cells[2].Value.ToString();

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
            SqlDataReader dr, dr1;
            DBHelper.con.Open();
            SqlCommand cmd = new SqlCommand("select * from VIp", DBHelper.con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["VIP"].ToString());
            }
            dr.Close();
            SqlCommand cmd1 = new SqlCommand("select * from TableNumber where Tablefettle='false' and Tablebook<>'true'", DBHelper.con);
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox2.Items.Add(dr1["TableId"].ToString());
            }
            dr1.Close();
            DBHelper.con.Close();

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
