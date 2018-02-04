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
    public partial class top : Form
    {
        public top()
        {
            InitializeComponent();
        }
        private void buton_bul(object sender, EventArgs e)
        {
            //  baglanti.Open();
            Button btn = sender as Button;
            /*   string hky = "Select * from Filmler where id=@id";
               SqlCommand kmtHky = new SqlCommand(hky, baglanti);
               kmtHky.Parameters.AddWithValue("@id", int.Parse(btn.Tag.ToString()));
            
               SqlDataReader dr = kmtHky.ExecuteReader();
               if(dr.Read())
                   MessageBox.Show((dr["hikaye"]).ToString());
               baglanti.Close();*/
            Form1.filmId = int.Parse(btn.Tag.ToString());
            Form film = new film();
            film.Show();
        }

        private void label_bul(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            Form1.filmId = int.Parse(lbl.Tag.ToString());
            Form film = new film();
            film.Text = Form1.filmId.ToString();
            film.Show();

        }
        

        private void top_Load(object sender, EventArgs e)
        {
            int yKonum = 0;
            int i = 0;
            try
            {
                
               
                SqlConnection baglanti1 = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                string top = "SELECT fy.fid,AVG(cast(fy.oy as float)),f.isim FROM FilmYorum fy, Filmler f Where fy.fid = f.id group by fy.fid,f.isim order by (AVG(cast(fy.oy as float))) desc";
                baglanti1.Open();
                SqlCommand kmttop = new SqlCommand(top, baglanti1);
                // kmtöneri.Parameters.AddWithValue("@kid", Form1.kid);
                SqlDataReader drTop = kmttop.ExecuteReader();
                while (drTop.Read() && i<5)
                {
                    i++;
                    Panel pnl = new Panel();

                    pnl.Size = new System.Drawing.Size(318, 74);
                    pnl.Location = new Point(0, yKonum);
                    pnl.BorderStyle = BorderStyle.FixedSingle;

                    Label lblfavori = new Label();
                    lblfavori.Location = new Point(60, 5);
                    lblfavori.Text =" "+ ((drTop[2]).ToString().ToUpper());
                    lblfavori.AutoSize = true;                 
                    lblfavori.Font = new Font("Calibri", 9, FontStyle.Bold);
                    pnl.Controls.Add(lblfavori);
                    lblfavori.Tag = (drTop[0]).ToString();
                    lblfavori.Click += new EventHandler(this.label_bul);

                    Label lblimdb = new Label();
                    lblimdb.Location = new Point(60, 25);
                    lblimdb.Text = "  İMDB = "+ ((drTop[1]).ToString().ToUpper()) + " PUAN";
                    lblimdb.AutoSize = true;
                    lblimdb.ForeColor = Color.Green;
                    lblimdb.Font = new Font("Calibri", 9, FontStyle.Bold);
                    pnl.Controls.Add(lblimdb);
                    


                    Button btn = new Button();
                    btn.Tag = (drTop["fid"]).ToString();
                    btn.Location = new Point(0, 0);
                    btn.Size = new Size(60, 70);
                    btn.BackColor = Color.Green;
                    btn.Click += new EventHandler(this.buton_bul);
                    pnl.Controls.Add(btn);



                    int flimId = int.Parse((drTop[0]).ToString());
                    SqlConnection foto = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");
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
                        Bitmap bmp = new Bitmap(icon, new Size(60, 70));
                        btn.Image = bmp;
                    }
                    drFoto.Close();
                    foto.Close();



                    yKonum += 73;
                    panel1.Controls.Add(pnl);
                }
                drTop.Close();


                baglanti1.Close();

            }


            catch (Exception hata)
            {
                // baglanti1.Close();
                MessageBox.Show("Hata Oluştu !\n" + hata.Message);
                // throw;
            }
        }
    }
}
