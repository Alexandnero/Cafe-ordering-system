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
    public partial class Enter : Form
    {
        public Enter()
        {
            InitializeComponent();
        }

        //界面事件
        private void btnExit_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "select UId from admin";
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                DBHelper.con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.cboname.Items.Add(reader["UId"].ToString());
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

        //点击退出事件
        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //点击确认事件
        private void btnenter_Click(object sender, EventArgs e)
        {
            if (get())
            {
                int num=0;
                try{
                    //查询数据库语句
                string sql = string.Format("select count(*) from admin where UId='{0}' and PassWord='{1}'",cboname.Text,txtpassword.Text);
                    //创建command对象
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                    //打开数据库
                DBHelper.con.Open();
                num = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch(Exception ex)
                {
                   MessageBox.Show(ex.Message);
                }
                finally
                {
                    DBHelper.con.Close();
                }


                //判断帐号密码
                if (num == 1)
                {
                    SqlDataReader dr;
                    string sql1 = string.Format("select Role from admin where UId='{0}' and PassWord='{1}'", cboname.Text, txtpassword.Text);
                    SqlCommand cmd1 = new SqlCommand(sql1, DBHelper.con);
                    DBHelper.con.Open();
                    dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        UserTable.role = dr["Role"].ToString();
                    }
                    DBHelper.con.Close();
                    UserTable.name = cboname.Text;
                    MainFrom mf = new MainFrom();
                    mf.Show();
                    this.Visible = false;

                }
                else
                {
                    MessageBox.Show("用户类型密码不匹配;", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //非空验证
        private bool get()
        {
            if (cboname.Text.Trim()=="")
            {
                MessageBox.Show("请选择用户名", "提示");
                cboname.Focus();
                return false;

            }
            else if (txtpassword.Text.Trim() == "")
            {
                MessageBox.Show("请输入密码", "提示");
                txtpassword.Focus() ;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void cboname_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}