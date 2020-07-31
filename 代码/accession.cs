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

        //�����Ӱ�ť�¼�
        private void button1_Click(object sender, EventArgs e)
        {
            if (get())
            {
                int result = 0;
                try
                {
                    //���ݿ��ѯ���
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
                //�ж�����Ƿ�ɹ�
                if (result == 1)
                {
                    MessageBox.Show("��ӳɹ�", "��ʾ");
                    txtname.Text = "";
                    txtprice.Text = "";
                    cbotype.Text = "";
                }
                else
                {
                    MessageBox.Show("���ʧ��", "��ʾ");
                }
            }

        }

        //�ǿ���֤
        private bool get()
        {
            if (txtname.Text.Trim() == "")
            {
                MessageBox.Show("��������Ʒ��", "��ʾ");
                txtname.Focus();
                return false;
            }
            else if (txtprice.Text.Trim() == "")
            {
                MessageBox.Show("������۸�.", "��ʾ");
                txtprice.Focus();
                return false;
            }
            else if (cbotype.Text.Trim() == "")
            {
                MessageBox.Show("��������Ʒ����");
                cbotype.Focus();
                return false;

            }
            else
            {
                return true;
            }
        }
        //����¼���ť
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