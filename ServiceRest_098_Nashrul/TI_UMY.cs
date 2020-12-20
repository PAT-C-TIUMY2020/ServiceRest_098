using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceRest_098_Nashrul
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TI_UMY : ITI_UMY
    {
        public string CreateMahasiswa(Mahasiswa mhs)
        {
            string msg = "GAGAL";
            SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-5QK0TPE\\NASHRUL;Initial Catalog=\"TI UMY\";Persist Security Info=True;User ID=sa;Password=12345");
            string query = string.Format("insert into dbo.Mahasiswa values ('{0}', '{1}','{2}','{3}')", mhs.nama, mhs.nim, mhs.prodi, mhs.angkatan);
            //NIM = '{0}'", nim)

            SqlCommand sqlcom = new SqlCommand(query, sqlcon);

            try
            {
                sqlcon.Open(); // membuka connection sql

                Console.WriteLine(query);

                sqlcom.ExecuteNonQuery(); // mengeksekusi untuk memasukan data

                sqlcon.Close();

                msg = "Sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";

            }
            return msg;
        }

        public List<Mahasiswa> GetAllMahasiswa()
        {
            List<Mahasiswa> mahas = new List<Mahasiswa>();

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-5QK0TPE\\NASHRUL;Initial Catalog=\"TI UMY\";Persist Security Info=True;User ID=sa;Password=12345");
            string query = "select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa";
            SqlCommand com = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader(); //mendapatkan data yang telah dieksekusi, dari select, hasil query ditaro di reader

                while (reader.Read())
                {
                    Mahasiswa mhs = new Mahasiswa();
                    mhs.nama = reader.GetString(0); // 0 itu array pertama // ini di ambil dari iservice
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);

                    mahas.Add(mhs);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mahas; //output
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Mahasiswa GetMahasiswaByNIM(string nim)
        {
            Mahasiswa mhs = new Mahasiswa();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-5QK0TPE\\NASHRUL;Initial Catalog=\"TI UMY\";Persist Security Info=True;User ID=sa;Password=12345");
            string query = string.Format("select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa where NIM = '{0}'", nim);
            SqlCommand com = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    mhs.nama = reader.GetString(0); // 0 itu array pertama // ini di ambil dari iservice
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mhs;

        }
    }
}
