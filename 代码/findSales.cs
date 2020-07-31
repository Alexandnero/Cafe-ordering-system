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
    public partial class findSales : Form
    {
        public findSales()
        {
            InitializeComponent();
        }
        private void 更新()
        {
            DataSet setkh = new DataSet();
            string kh = "select A.CID as 商品编号,A.CName as 商品名称,SUM({ fn IFNULL(B.Dmoney,0)}) as 销售额 from commoidty A LEFT OUTER JOIN Diandan B ON A.CName =B.DName GROUP BY A.CID, A.CName";
            SqlDataAdapter da1 = new SqlDataAdapter(kh, DBHelper.con);
            setkh.Clear();
            da1.Fill(setkh, "commoidty");
            DataGridView1.DataSource = setkh.Tables[0];
            comboBox1.Text = "";

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text != "")
            {
                String kh = "select * from Sales where [商品编号]=" + comboBox1.Text.Split(' ')[0];
                DataSet setkh = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter(kh, DBHelper.con);
                setkh.Clear();
                da1.Fill(setkh, "Sales");
                DataGridView1.DataSource = setkh.Tables[0];
            }
            else
            {
                MessageBox.Show("商品编号不能为空!", "提示");
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                comboBox1.Text = DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                
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
            SqlCommand cmd = new SqlCommand("select * from commoidty", DBHelper.con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["CID"].ToString() + " " + dr["CName"].ToString() + " " + dr["Cprice"].ToString() + " " + dr["Ctype"].ToString());
            }

            dr.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
