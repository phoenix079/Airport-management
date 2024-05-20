using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
namespace Airport
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        DataTable dt;
        void loadData()
        {
            MySqlConnection conn;
            MySqlCommand cmd;
            MySqlDataReader reader;


            string path = "Data Source=localhost; Initial Catalog=airport; uid=root; password=Snap@2000;";
            string sql = "select * from airport.bookings";
           

            conn = new MySqlConnection(path);


            try
            {
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                reader = cmd.ExecuteReader();

                dt = new DataTable();
                dt.Load(reader);

                bookingview.DataSource = dt;

                reader.Close();
                cmd.Dispose();
                conn.Close();





            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

         
        }

        private void Form2_Load(object sender, EventArgs e)
        {


            loadData();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {



        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void book_Click(object sender, EventArgs e)
        {
            MySqlConnection conn;
            MySqlCommand cmd;



            string path = "Data Source=localhost; Initial Catalog=airport; uid=root; password=Snap@2000;";
            string sql = "INSERT INTO bookings(id, passenger_name, plane, timing, seatno, class, gender, age, source_location, destination_location) VALUES(@id, @passenger_name, @plane, @timing, @seatno, @class, @gender, @age, @source_location, @destination_location)";
            conn = new MySqlConnection(path);

            int iid = int.Parse(id.Text);
            string pn = pname.Text;
            string pl = plane.Text;
            string time = timing.Text;
            int seat = int.Parse(seatno.Text);
            string cl = seatclass.Text;
            string gen = gender.Text;
            int ag = int.Parse(age.Text);
            string src = slocation.Text;
            string dst = destlocation.Text;

            try
            {


                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", iid);
                cmd.Parameters.AddWithValue("@passenger_name", pn);
                cmd.Parameters.AddWithValue("@plane", pl);
                cmd.Parameters.AddWithValue("@timing", time);
                cmd.Parameters.AddWithValue("@seatno", seat);
                cmd.Parameters.AddWithValue("@class", cl);
                cmd.Parameters.AddWithValue("@gender", gen);
                cmd.Parameters.AddWithValue("@age", ag);
                cmd.Parameters.AddWithValue("@source_location", src);
                cmd.Parameters.AddWithValue("@destination_location", dst);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                conn.Close();

                loadData();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void bookingview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {

            MySqlConnection conn;
            MySqlCommand cmd;
            MySqlDataReader reader;


            string path = "Data Source=localhost; Initial Catalog=airport; uid=root; password=Snap@2000;";
            string sql = "delete from bookings where id=@id";

            conn = new MySqlConnection(path);


            try
            {
                conn.Open();
                cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", int.Parse(id.Text));

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();



                loadData();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void pname_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            DataView dataView = dt.DefaultView;
            dataView.RowFilter = string.Format("passenger_name LIKE '%{0}%' OR plane LIKE '%{0}%' OR source_location LIKE '%{0}%'", search.Text);


            DataTable dt2 = dataView.ToTable();
            bookingview.DataSource = dt2;
        }
    }
}