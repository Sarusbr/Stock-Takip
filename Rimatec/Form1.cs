using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Rimatec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region değişkenler
        string txt1 = "Kullanıcı Adı", txt2 = "Şifre";
        int x = 0, y = 0;
        bool windowMove = false;
        string connectionString = "Server=SARUS\\SQLEXPRESS;Database=Rimatec;User Id=adminuser;Password=123;";
        #endregion

        private void textBox1_Enter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.White;
            label1.Visible = true;
            textBox1.ForeColor = Color.White;
            if (textBox1.Text == txt1) textBox1.Clear();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Silver;
            label1.Visible = false;
            textBox1.ForeColor = Color.Silver;
            if (string.IsNullOrWhiteSpace(textBox1.Text)) textBox1.Text = txt1;
            else textBox1.ForeColor = Color.WhiteSmoke;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            panel3.BackColor = Color.White;
            label2.Visible = true;
            textBox2.ForeColor = Color.White;
            if (textBox2.Text == txt2) textBox2.Clear();
            textBox2.UseSystemPasswordChar = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = txt1;
            textBox2.Text = txt2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            windowMove = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (windowMove)
            {
                this.Location = new Point(MousePosition.X-x, MousePosition.Y - y);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("userControl", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@systemName", textBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@systemPass", textBox2.Text.Trim());
            textBox2.Focus();
            textBox2.Clear();
            textBox1.Focus();
            textBox1.Clear();
            SqlParameter outParamater = new SqlParameter("@id", SqlDbType.Int);
            outParamater.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outParamater);
            cmd.ExecuteScalar();
            int id = int.Parse(cmd.Parameters["@id"].Value.ToString());
            if (id == -1) MessageBox.Show("Hatalı Kullanıcı Adı\nVeya\nŞifre");
            else
            {
                Form2 fr2 = new Form2(id);
                fr2.Show();
                this.Hide();
            }
            con.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.PerformClick();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            windowMove = false;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Silver;
            label2.Visible = false;
            textBox2.ForeColor = Color.Silver;
            if (string.IsNullOrWhiteSpace(textBox2.Text)) textBox2.Text = txt2;
            else textBox2.ForeColor = Color.WhiteSmoke;
            if(textBox2.Text == txt2)textBox2.UseSystemPasswordChar = false;
        }
    }
}
