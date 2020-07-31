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
    public partial class accession : Form
    {
        public accession()
        {
            InitializeComponent();
        }

        //点击添加按钮事件
        private void button1_Click(object sender, EventArgs e)
        {
            if (get())
            {
                int result = 0;
                try
                {
                    //数据库查询语句
                    string sql = string.Format("insert into commoidty (CName,Cprice,Ctype) values ('{0}',{1},'{2}')",
                               txtname.Text, txtprice.Text, cbotype.Text);
                    SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                    DBHelper.con.Open();
                     result = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    DBHelper.con.Close();
                }
                //判断添加是否成功
                if (result == 1)
                {
                    MessageBox.Show("添加成功", "提示");
                    txtname.Text = "";
                    txtprice.Text = "";
                    cbotype.Text = "";
                }
                else
                {
                    MessageBox.Show("添加失败", "提示");
                }
            }

        }

        //非空验证
        private bool get()
        {
            if (txtname.Text.Trim() == "")
            {
                MessageBox.Show("请输入商品名", "提示");
                txtname.Focus();
                return false;
            }
            else if (txtprice.Text.Trim() == "")
            {
                MessageBox.Show("请输入价格.", "提示");
                txtprice.Focus();
                return false;
            }
            else if (cbotype.Text.Trim() == "")
            {
                MessageBox.Show("请输入商品类型");
                cbotype.Focus();
                return false;

            }
            else
            {
                return true;
            }
        }
        //清空事件按钮
        private void btoclean_Click(object sender, EventArgs e)
        {
            txtname.Text = "";
            txtprice.Text = "";
            cbotype.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void accession_Load(object sender, EventArgs e)
        {

        }

    }
}