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
    public partial class uye_giris : Form
    {
        public uye_giris()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");

        private void button1_Click(object sender, EventArgs e)
        {
            


        }

        private void uye_giris_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if ((soyadtext.Text == "") || (nicktext.Text == "") || (şifretext.Text == "") || (mailtext.Text == "") || isimtext.Text=="")
            {
                MessageBox.Show("LÜTFEN TÜM ALANLARI DOLDURUN");
                return;
            }
            try
            {
                baglanti.Open();
                string checkcommand = "SELECT mail,nick FROM Kullanici WHERE mail=@mail OR nick=@nick";
                SqlCommand checkcmd = new SqlCommand(checkcommand, baglanti);
                checkcmd.Parameters.AddWithValue("@mail", mailtext.Text);
                checkcmd.Parameters.AddWithValue("@nick", nicktext.Text);
                SqlDataReader dr = checkcmd.ExecuteReader();

                if (dr.Read())
                {
                    MessageBox.Show("GİRMİŞ OLDUGUNUZ MAİLE YADA NİCKE AİT HESAP BULUNMAKTADIR!");
                    dr.Close();
                    baglanti.Close();
                    this.Close();

                }
                else
                {
                    dr.Close();
                    string kayit = "insert into Kullanici(isim,soyad,nick,mail,şifre) values (@isim,@soyad,@nick,@mail,@şifre)";
                    checkcmd = new SqlCommand(kayit, baglanti);
                    checkcmd.Parameters.AddWithValue("@isim", isimtext.Text);
                    checkcmd.Parameters.AddWithValue("@soyad", soyadtext.Text);
                    checkcmd.Parameters.AddWithValue("@nick", nicktext.Text);
                    checkcmd.Parameters.AddWithValue("@mail", mailtext.Text);
                    checkcmd.Parameters.AddWithValue("@şifre", şifretext.Text);

                    checkcmd.ExecuteNonQuery();
                    MessageBox.Show("Üye Kayıt İşlemi Gerçekleşti.");
                    baglanti.Close();
                    this.Close();

                }

            }
            catch (Exception hata)
            {
                MessageBox.Show("hata oluştu" + "  " + hata.Message);
            }
        }
    }
}
