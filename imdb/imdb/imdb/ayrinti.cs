using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imdb
{
    public partial class ayrinti : Form
    {
        public ayrinti()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
        public static int film_id = 0;
        public static int sanatci_id = 0;
       
        private void buton_bul2(object sender, EventArgs e)
        {
            Button btn2 = sender as Button;
            sanatci_id = int.Parse(btn2.Tag.ToString());
            Form Sanatcilar = new Sanatcilar();
            Sanatcilar.Show();
        }
        private void label_bul2(object sender, EventArgs e)
        {
            Label lbl2 = sender as Label;
            sanatci_id = int.Parse(lbl2.Tag.ToString());
            Form Sanatcilar = new Sanatcilar();
            Sanatcilar.Text = sanatci_id.ToString();
            Sanatcilar.Show();

        }

        private void buton_bul(object sender, EventArgs e)
        {
            //  baglanti.Open();
            Button btn = sender as Button;
          
            Form1.filmId = int.Parse(btn.Tag.ToString());
            Form film = new film();
            film.Show();
        }

        private void label_bul(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            Form1.filmId = int.Parse(lbl.Tag.ToString());
            Form film = new film();
            film.Text = film_id.ToString();
            film.Show();

        }
        
        private void getirFavori()
        {
            int y_konum = 0;
            int x_konum = 0;

            try
            {
                baglanti.Open();
                //   MessageBox.Show("Veri Tabanına Baglantı Başarılı");

                string favori = "select isim,id from Filmler where id in (select fid from favori where kid=@kid)";
                SqlCommand kmtfavori = new SqlCommand(favori, baglanti);
                kmtfavori.Parameters.AddWithValue("@kid", Form1.kid);
                SqlDataReader drfavori = kmtfavori.ExecuteReader();
                while (drfavori.Read())
                {
                    Panel pnl = new Panel();

                    pnl.Size = new System.Drawing.Size(267, 60);
                    pnl.Location = new Point(0, y_konum);
                    pnl.BorderStyle = BorderStyle.FixedSingle;

                    Label lblfavori = new Label();
                    lblfavori.Location = new Point(65, 20);
                    lblfavori.Text = ((drfavori["isim"]).ToString().ToUpper());
                    lblfavori.AutoSize = true;
                  //  lblfavori.Width = 160;
                  //  lblfavori.Height = 36;
                    lblfavori.Font = new Font("Calibri", 10, FontStyle.Bold);
                    pnl.Controls.Add(lblfavori);
                    lblfavori.Tag = (drfavori["id"]).ToString();
                    lblfavori.Click += new EventHandler(this.label_bul);


                    Button btn = new Button();
                    btn.Tag = (drfavori["id"]).ToString();
                    btn.Location = new Point(0, 0);
                    btn.Size = new Size(60, 60);
                    btn.BackColor = Color.Green;
                    btn.Click += new EventHandler(this.buton_bul);
                    pnl.Controls.Add(btn);



                    int flimId = int.Parse((drfavori["id"]).ToString());
                    SqlConnection foto = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                    foto.Open();
                    string img = "Select foto from FilmFoto where fid='" + flimId + "'";
                    SqlCommand kmtimg = new SqlCommand(img, foto);
                    SqlDataReader drFoto = kmtimg.ExecuteReader();
                    Image filmFoto = null;
                    byte[] resim;
                    if (drFoto.Read())
                    {
                        resim = (byte[])drFoto[0];
                        MemoryStream ms = new MemoryStream(resim, 0, resim.Length);
                        ms.Write(resim, 0, resim.Length);
                        filmFoto = Image.FromStream(ms, true);
                        Image icon = filmFoto;
                        Bitmap bmp = new Bitmap(icon, new Size(60, 60));
                        btn.Image = bmp;
                    }
                    drFoto.Close();
                    foto.Close();



                    y_konum += 65;
                    panel1.Controls.Add(pnl);
                }
                drfavori.Close();

            
                baglanti.Close();
                
            }


            catch (Exception hata)
            {
                baglanti.Close();
                MessageBox.Show("Hata Oluştu !\n" + hata.Message);
                // throw;
            }


        }

        private void getirIzlenen()
        {
            int y_konum = 0;
            //int x_konum = 0;

            try
            {
                baglanti.Open();
                //   MessageBox.Show("Veri Tabanına Baglantı Başarılı");

                string favori = "select isim,id from Filmler where id in (select fid from izlenen where kid=@kid)";
                SqlCommand kmtfavori = new SqlCommand(favori, baglanti);
                kmtfavori.Parameters.AddWithValue("@kid", Form1.kid);
                SqlDataReader drfavori = kmtfavori.ExecuteReader();
                while (drfavori.Read())
                {
                    Panel pnl = new Panel();

                    pnl.Size = new System.Drawing.Size(267, 60);
                    pnl.Location = new Point(0, y_konum);
                    pnl.BorderStyle = BorderStyle.FixedSingle;

                    Label lblfavori = new Label();
                    lblfavori.Location = new Point(65, 20);
                    lblfavori.Text = ((drfavori["isim"]).ToString().ToUpper());
                    lblfavori.AutoSize = true;
                    //  lblfavori.Width = 160;
                    //  lblfavori.Height = 36;
                    lblfavori.Font = new Font("Calibri", 10, FontStyle.Bold);
                    pnl.Controls.Add(lblfavori);
                    lblfavori.Tag = (drfavori["id"]).ToString();
                    lblfavori.Click += new EventHandler(this.label_bul);


                    Button btn = new Button();
                    btn.Tag = (drfavori["id"]).ToString();
                    btn.Location = new Point(0, 0);
                    btn.Size = new Size(60, 60);
                    btn.BackColor = Color.Green;
                    btn.Click += new EventHandler(this.buton_bul);
                    pnl.Controls.Add(btn);



                    int flimId = int.Parse((drfavori["id"]).ToString());
                    SqlConnection foto = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                    foto.Open();
                    string img = "Select foto from FilmFoto where fid='" + flimId + "'";
                    SqlCommand kmtimg = new SqlCommand(img, foto);
                    SqlDataReader drFoto = kmtimg.ExecuteReader();
                    Image filmFoto = null;
                    byte[] resim;
                    if (drFoto.Read())
                    {
                        resim = (byte[])drFoto[0];
                        MemoryStream ms = new MemoryStream(resim, 0, resim.Length);
                        ms.Write(resim, 0, resim.Length);
                        filmFoto = Image.FromStream(ms, true);
                        Image icon = filmFoto;
                        Bitmap bmp = new Bitmap(icon, new Size(60, 60));
                        btn.Image = bmp;
                    }
                    drFoto.Close();
                    foto.Close();



                    y_konum += 65;
                    panel2.Controls.Add(pnl);
                }
                drfavori.Close();


                baglanti.Close();

            }


            catch (Exception hata)
            {
                baglanti.Close();
                MessageBox.Show("Hata Oluştu !\n" + hata.Message);
                // throw;
            }


        }

        private void getirIzlenecek()
        {
            int y_konum = 0;
            int x_konum = 0;

            try
            {
                baglanti.Open();
                //   MessageBox.Show("Veri Tabanına Baglantı Başarılı");

                string favori = "select isim,id from Filmler where id in (select fid from izlenecekler where kid=@kid)";
                SqlCommand kmtfavori = new SqlCommand(favori, baglanti);
                kmtfavori.Parameters.AddWithValue("@kid", Form1.kid);
                SqlDataReader drfavori = kmtfavori.ExecuteReader();
                while (drfavori.Read())
                {
                    Panel pnl = new Panel();

                    pnl.Size = new System.Drawing.Size(267, 60);
                    pnl.Location = new Point(0, y_konum);
                    pnl.BorderStyle = BorderStyle.FixedSingle;

                    Label lblfavori = new Label();
                    lblfavori.Location = new Point(65, 20);
                    lblfavori.Text = ((drfavori["isim"]).ToString().ToUpper());
                    lblfavori.AutoSize = true;
                    //  lblfavori.Width = 160;
                    //  lblfavori.Height = 36;
                    lblfavori.Font = new Font("Calibri", 10, FontStyle.Bold);
                    pnl.Controls.Add(lblfavori);
                    lblfavori.Tag = (drfavori["id"]).ToString();
                    lblfavori.Click += new EventHandler(this.label_bul);


                    Button btn = new Button();
                    btn.Tag = (drfavori["id"]).ToString();
                    btn.Location = new Point(0, 0);
                    btn.Size = new Size(60, 60);
                    btn.BackColor = Color.Green;
                    btn.Click += new EventHandler(this.buton_bul);
                    pnl.Controls.Add(btn);



                    int flimId = int.Parse((drfavori["id"]).ToString());
                    SqlConnection foto = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                    foto.Open();
                    string img = "Select foto from FilmFoto where fid='" + flimId + "'";
                    SqlCommand kmtimg = new SqlCommand(img, foto);
                    SqlDataReader drFoto = kmtimg.ExecuteReader();
                    Image filmFoto = null;
                    byte[] resim;
                    if (drFoto.Read())
                    {
                        resim = (byte[])drFoto[0];
                        MemoryStream ms = new MemoryStream(resim, 0, resim.Length);
                        ms.Write(resim, 0, resim.Length);
                        filmFoto = Image.FromStream(ms, true);
                        Image icon = filmFoto;
                        Bitmap bmp = new Bitmap(icon, new Size(60, 60));
                        btn.Image = bmp;
                    }
                    drFoto.Close();
                    foto.Close();



                    y_konum += 65;
                    panel3.Controls.Add(pnl);
                }
                drfavori.Close();


                baglanti.Close();

            }


            catch (Exception hata)
            {
                baglanti.Close();
                MessageBox.Show("Hata Oluştu !\n" + hata.Message);
                // throw;
            }


        }

        private void ayrinti_Load(object sender, EventArgs e)
        {
            getirFavori();
            getirIzlenen();
            getirIzlenecek();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="" || (textBox2.Text==""))
            {
                MessageBox.Show("GEREKLİ ALANLARI DOLDURUN");
                return;
            }

            if(textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("ŞİFRELER AYNI OLMALIDIR");
                return;
            }

            try
            {
                baglanti.Open();
                string kmtSifre = "UPDATE Kullanici SET şifre=@sifre where id=@id";
                SqlCommand sifre = new SqlCommand(kmtSifre, baglanti);
                sifre.Parameters.AddWithValue("@sifre", textBox1.Text);
                sifre.Parameters.AddWithValue("@id", Form1.kid);
                sifre.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("ŞİFRE GÜNCELLENDİ");
            }
            catch (Exception hata)
            {                
                baglanti.Close();
                MessageBox.Show("Hata Oluştu !\n" + hata.Message);
               // throw;
            }
            



        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || (textBox4.Text == ""))
            {
                MessageBox.Show("GEREKLİ ALANLARI DOLDURUN");
                return;
            }

            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("MAİLLER AYNI OLMALIDIR");
                return;
            }

            try
            {
                baglanti.Open();
                string kmtSifre = "UPDATE Kullanici SET mail=@mail where id=@id";
                SqlCommand sifre = new SqlCommand(kmtSifre, baglanti);
                sifre.Parameters.AddWithValue("@mail", textBox3.Text);
                sifre.Parameters.AddWithValue("@id", Form1.kid);
                sifre.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("MAİL GÜNCELLENDİ");
            }
            catch (Exception hata)
            {
                baglanti.Close();
                MessageBox.Show("Hata Oluştu !\n" + hata.Message);
                // throw;
            }
        }

       
    }
}