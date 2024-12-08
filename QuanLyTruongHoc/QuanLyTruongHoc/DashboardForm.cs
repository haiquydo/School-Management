using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyTruongHoc
{
    public partial class DashboardForm : UserControl
    {
        private SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\ConsoleApp\QuanLyTruongHoc\QuanLyTruongHoc\QuanLyTruongHoc.mdf;Integrated Security=True");

        public DashboardForm()
        {
            InitializeComponent();
        }

        // Hiển thị tổng số sinh viên đã đăng ký
        public void displayTotalES()
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    // Cập nhật truy vấn SQL để sử dụng bảng "students"
                    string selectData = "SELECT COUNT(student_id) FROM students WHERE student_status = 'Enrolled' AND date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempES = 0;
                        if (reader.Read())
                        {
                            tempES = Convert.ToInt32(reader[0]);
                            total_ES.Text = tempES.ToString(); // Hiển thị tổng số sinh viên đã đăng ký
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to connect Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        // Hiển thị tổng số giáo viên đang hoạt động
        public void displayTotalTT()
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(teacher_id) FROM teachers WHERE teacher_status = 'Active' AND date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempTT = 0;
                        if (reader.Read())
                        {
                            tempTT = Convert.ToInt32(reader[0]);
                            total_TT.Text = tempTT.ToString(); // Hiển thị tổng số giáo viên
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to connect Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTotalGS()
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(student_id) FROM students WHERE student_status = 'Graduated' AND date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempGS = 0;
                        if (reader.Read())
                        {
                            tempGS = Convert.ToInt32(reader[0]);
                            total_GS.Text = tempGS.ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to connect Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayEnrolledStudentToday()
        {
            AddStudentData asData = new AddStudentData();
            dataGridView1.DataSource = asData.dashboardStudentData();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            displayTotalES();
            displayTotalTT();
            displayTotalGS();
            displayEnrolledStudentToday();
        }
    }
}
