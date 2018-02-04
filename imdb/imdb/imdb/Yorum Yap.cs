using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imdb
{
    public partial class Yorum_Yap : Form
    {
        public Yorum_Yap()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            
            SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
            baglanti.Open();
            
            if (Form1.fsyc == 1)
            {
                int puan = int.Parse(comboBox1.SelectedItem.ToString());
                string yorum = "insert into FilmYorum(fid,kid,tarih,yorum,oy) values (@fid,@kid,@tarih,@yorum,@oy)";
                
                SqlCommand kmtYorum = new SqlCommand(yorum, baglanti);
                kmtYorum.Parameters.AddWithValue("@fid", Form1.filmId);
                kmtYorum.Parameters.AddWithValue("kid", Form1.kid);
                kmtYorum.Parameters.AddWithValue("@tarih", dateTimePicker1.Value.ToShortDateString());
                kmtYorum.Parameters.AddWithValue("@yorum", textBox1.Text.ToString());
                kmtYorum.Parameters.AddWithValue("@oy", puan);

                kmtYorum.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yorum Yapıldı");
                this.Close();
            }

            else if (Form1.fsyc == 2)
            {
               
                string cevap = "insert into FilmCevap(fid,kid,kcid,tarih,cevap) values (@fid,@kid,@kcid,@fctarih,@cevap)";                
                SqlCommand kmtCevap = new SqlCommand(cevap, baglanti);
                kmtCevap.Parameters.AddWithValue("@fid", Form1.filmId);
                kmtCevap.Parameters.AddWithValue("@kid", Sanatcilar.yorumKid);
                kmtCevap.Parameters.AddWithValue("@fctarih", dateTimePicker1.Value.ToShortDateString());
                kmtCevap.Parameters.AddWithValue("@cevap", textBox1.Text.ToString());
                kmtCevap.Parameters.AddWithValue("@kcid", Form1.kid);

                kmtCevap.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Cevap Verildi");
                this.Close();
            }

            else if (Form1.fsyc == 3)
            {
                string cevap = "insert into SanatciCevap(sid,kid,kcid,tarih,cevap) values (@sid,@kid,@kcid,@tarih,@cevap)";
                SqlCommand kmtCevap = new SqlCommand(cevap, baglanti);
                kmtCevap.Parameters.AddWithValue("@sid", Form1.santciId);
                kmtCevap.Parameters.AddWithValue("@kid", Sanatcilar.yorumKid);
                kmtCevap.Parameters.AddWithValue("@tarih", dateTimePicker1.Value.ToShortDateString());
                kmtCevap.Parameters.AddWithValue("@cevap", textBox1.Text.ToString());
                kmtCevap.Parameters.AddWithValue("@kcid", Form1.kid);

                kmtCevap.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Cevap Verildi");
                this.Close();
            }

            else if (Form1.fsyc == 4)
            {
                string yorum = "insert into SanatciYorum(sid,kid,tarih,yorum) values (@sid,@kid,@tarih,@yorum)";

                SqlCommand kmtYorum = new SqlCommand(yorum, baglanti);
                kmtYorum.Parameters.AddWithValue("@sid", Form1.santciId);
                kmtYorum.Parameters.AddWithValue("kid", Form1.kid);
                kmtYorum.Parameters.AddWithValue("@tarih", dateTimePicker1.Value.ToShortDateString());
                kmtYorum.Parameters.AddWithValue("@yorum", textBox1.Text.ToString());
                

                kmtYorum.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yorum Yapıldı");
                this.Close();
            }

            

        }

        private void Yorum_Yap_Load(object sender, EventArgs e)
        {
            if (Form1.fsyc == 1)
            {
                label1.Visible = true;
                comboBox1.Visible = true;
            }

            else if (Form1.fsyc == 2)
            {
                comboBox1.Visible = false;
                label1.Visible = false;
                this.Text = "Cevap Ver";
            }

            else if(Form1.fsyc == 3)
            {
                label1.Visible = false;
                comboBox1.Visible = false;
                this.Text = "Cevap Ver";
            }

            else if(Form1.fsyc == 4)
            {
                label1.Visible = false;
                comboBox1.Visible = false;
             
            }
        }
    }
}
