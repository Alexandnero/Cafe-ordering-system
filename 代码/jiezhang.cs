using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;
namespace index
{
    public partial class jiezhang : Form
    {

        public MainFrom mf = new MainFrom();
        private Form form = null;
        Bitmap memoryImage = null; //����һ��ͼƬ
        public static PrintDocument printDocument = new PrintDocument(); //����һ��Print�ĵ�����
        public jiezhang()
        {
            InitializeComponent();
        }

        public jiezhang(Form f)
        {
            form = f;
            InitializeComponent();
        }

        private void jiezhang_Load(object sender, EventArgs e)
        {
            try
            {
                string sql1 = string.Format("select * from Tables where Ctable={0}", int.Parse(UserTable.talbeNo));
                SqlCommand cmd = new SqlCommand(sql1, DBHelper.con);
                DBHelper.con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label8.Text = reader["Ctable"].ToString();

                    label9.Text = reader["monry"].ToString();

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
            show();
        }

        private void show()
        {
            try
            { 
            DataSet dataset = new DataSet("CoffeeHouse");
            SqlDataAdapter dataadapter;

            string sql1 = string.Format("select * from Diandan where Dtable={0}", int.Parse(label8.Text));
            dataadapter = new SqlDataAdapter(sql1, DBHelper.con);
            dataadapter.Fill(dataset, "Diandan");
            dataGridView1.DataSource = dataset.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            accounts();

        }

        //�ǿ���֤
        private bool readio()
        {
            if (radioButton1.Checked == radioButton2.Checked)
            {
                MessageBox.Show("��ѡ��һ��ʽ", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (radioButton1.Checked == true)
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("�������Ա��������");
                    return false;
                }
                else
                {
                    return true;
                }
               
            }
            else if (radioButton2.Checked == true)
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("������֧�����");
                    return false;
                }
                else
                {
                    return true;
                }
               
            }
            else
            {
                return true;
            }

        }

        //��֤֧����ʽ
        private void accounts()
        {
            if (readio())
            {
                if (radioButton1.Checked == true)
                {
                    int num = 0;
                    double a = 0;  //���ж��;
                    double b = 0;  //���Ѻ���;

                    //�жϿ���������ȷ
                    try
                    {
                        //��ѯ���ݿ����
                        string sql = string.Format("select count(*) from VIp where VIP={0} and VIppass='{1}'", int.Parse(textBox1.Text), textBox2.Text);
                        //����command����
                        SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                        //�����ݿ�
                        DBHelper.con.Open();
                        num = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        DBHelper.con.Close();
                    }
                    //�������Ա

                    if (num == 1)
                    {

                       
                        //��ѯ������ϵ����
                        try
                        {
                            //��ѯ���ݿ����
                            string sql = string.Format("select Vipprice from VIp where VIP={0} and VIppass='{1}'", int.Parse(textBox1.Text), textBox2.Text);
                            //����command����
                            SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                            //�����ݿ�
                            DBHelper.con.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                a = Convert.ToDouble(reader["Vipprice"]);
                                b = a - Convert.ToDouble(label9.Text);
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
                        
                       

                        MessageBox.Show("����ɹ�,ʣ������Ϊ" + b.ToString() + "Ԫ", "�����ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //��ȥ���ѵļ۸�
                        try
                        {
                            //��ѯ���ݿ����
                            string sql = string.Format("update VIp set Vipprice={2} where VIP={0} and VIppass='{1}'", int.Parse(textBox1.Text), textBox2.Text, b);
                            //����command����
                            SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                            //�����ݿ�
                            DBHelper.con.Open();
                            int reader1 = 0;
                            reader1 = cmd.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            DBHelper.con.Close();
                        }

                        update();

                       
                    }

                    else
                    {
                        MessageBox.Show("��Ա�����������,���ʵ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        radioButton1.Checked = false;
                    }

                }
                else if (radioButton2.Checked == true)
                {
                    double zao = 0;  //������;
                    zao = Convert.ToDouble(textBox3.Text) - Convert.ToDouble(label9.Text);
                    label7.Text = zao.ToString();
                    DialogResult result=MessageBox.Show("����" + zao.ToString() + "Ԫ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.OK)
                    {
                        update();
                    }
                }
               
            }
           
        }

        //���º�ɾ�����ݿ�
        private void update()
        {
            //��������״̬
            try
            {
                string sql1 = string.Format("update TableNumber set Tablefettle='false'  where TableId={0}", int.Parse(label8.Text));
                SqlCommand cmd = new SqlCommand(sql1, DBHelper.con);
                DBHelper.con.Open();
                int result1 = 0;
                result1 = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBHelper.con.Close();

            }
            //ɾ������
            try
            {
                //ɾ����������
                string sql1 = string.Format("delete Diandan where Dtable={0}", int.Parse(label8.Text));
                SqlCommand cmd = new SqlCommand(sql1, DBHelper.con);
                DBHelper.con.Open();
                int result1 = 0;
                result1 = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBHelper.con.Close();

            }

            //ɾ����˶���
            try
            {
                string sql1 = string.Format("delete Tables where Ctable={0}", int.Parse(label8.Text));
                SqlCommand cmd = new SqlCommand(sql1, DBHelper.con);
                DBHelper.con.Open();
                int result1 = 0;
                result1 = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBHelper.con.Close();

            }
            this.Close();

        }

        private void jiezhang_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainFrom mf = (MainFrom)form;
            if (mf != null)
            {
                mf.ReloadForm();
            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && readio())
            {
                CaptureScreen();
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = pd1;
                printPreviewDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("��Ա���š����롢֧����ʽ��ʵ�ս���Ϊ�գ�", "��ʾ");
            }

        }
        private void CaptureScreen()
        {
                Graphics mygraphics = this.CreateGraphics();
                memoryImage = new Bitmap(this.Width-40, this.Height-90, mygraphics);
                Graphics memoryGraphics = Graphics.FromImage(memoryImage);
                memoryGraphics.CopyFromScreen(this.Location.X+10, this.Location.Y+30, 0, 0, this.Size);

        }
        private void pd1_PrintPage(Object sender, PrintPageEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

    }

}



















            
     


    
