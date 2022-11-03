using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Lucky_Draw
{
    public partial class LD_DataManage : Form
    {
        DataAccess DA = new DataAccess();
        StringBuilder sbSQL = new StringBuilder();

        /// <summary>
        /// 声明委托用于刷新主抽奖页面
        /// </summary>
        public delegate void ReturnListid();
        public event ReturnListid userl;

        public LD_DataManage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LD_DataManage_Load(object sender, EventArgs e)
        {
            Bind_DG();
        }

        /// <summary>
        /// 自定义事件——为DataGrid绑定数据
        /// </summary>
        private void Bind_DG()
        {
            try
            {
                string strSQL = "select stuID as 员工编号,stuName as 姓名,deptName as 部门编码,excludeWhere as 排除条件,IsCanDraw as 能否抽奖 from StuInfo";
                DataTable DT = DA.GetDataTable(strSQL);
                DG.DataSource = DT;
                DG.Refresh();
                if (DG.Rows.Count < 1)
                {
                    btnModify.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDeleteAll.Enabled = false;
                }
                else
                {
                    btnModify.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 单选按钮事件——打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (ofdExcel.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = ofdExcel.FileName;
            }
        }

        /// <summary>
        /// 单选按钮事件——导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLendIN_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DT = DA.LendInDT(txtFile.Text.Trim());
                int j = 0;
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    sbSQL = new StringBuilder("insert into StuInfo(stuID,stuName,deptName,porder,IsCanDraw,excludeWhere) values(");
                    sbSQL.Append(" '").Append(DT.Rows[i]["员工编号"].ToString()).Append( "',");
                    sbSQL.Append(" '").Append(DT.Rows[i]["姓名"].ToString()).Append("',");
                    sbSQL.Append(" '").Append(DT.Rows[i]["部门代码"].ToString()).Append("',");
                    sbSQL.Append(" '").Append(DT.Rows[i]["No"].ToString()).Append("',");
                    sbSQL.Append(" '").Append(DT.Rows[i]["能否抽奖"].ToString()).Append("',");
                    sbSQL.Append(" '").Append(DT.Rows[i]["排除条件"].ToString()).Append("')");
                    if (DA.ExecuteSQL(sbSQL.ToString()))
                    {
                        j++;
                    }
                }
                MessageBox.Show("导入数据成功，共导入" + j.ToString() + "条记录！","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                DataAccess.DataIsChange = true;
                Bind_DG();
                userl();
            }
            catch(Exception ex)
            {
                MessageBox.Show("导入数据失败，请确认你要导入的excel表格式正确！"+ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// DataGrid事件——单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_Click(object sender, EventArgs e)
        {
            try
            {
                if (DG.Rows.Count >= 1)
                {
                    DataGridViewRow DGrow = DG.CurrentRow;
                    txtNumber.Text = DGrow.Cells[0].Value.ToString();
                    txtNumber.Enabled = false;
                    txtName.Text = DGrow.Cells[1].Value.ToString();
                    txtDept.Text = DGrow.Cells[2].Value.ToString();
                    txtWhere.Text = DGrow.Cells[3].Value.ToString();
                    txtIsCan.Text = DGrow.Cells[4].Value.ToString();
                }
            }
            catch
            { }
        }

       /// <summary>
        /// 响应单击按钮事件——清空
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtNumber.Enabled = true;
            txtNumber.Text = "";
            txtName.Text = "";
            txtDept.Text = "";
            txtWhere.Text = "";
            txtIsCan.Text = "";
        }

        /// <summary>
        /// 响应单击按钮事件——添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool isRepeat = false;
                if (string.IsNullOrEmpty(txtNumber.Text.Trim()))
                {
                    MessageBox.Show("员工编号必填！","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    using (IDataReader dr = DA.ExecuteReader("SELECT stuID FROM StuInfo WHERE stuID='"+txtNumber.Text.Trim()+"'"))
                    {
                        while (dr.Read())
                        {
                            if (!string.IsNullOrEmpty(dr["stuID"].ToString()))
                            {
                                isRepeat = true;
                                break;
                            }
                        }
                        dr.Close();
                        dr.Dispose();
                    }
                }
                if (!isRepeat)
                {
                    sbSQL = new StringBuilder("insert into StuInfo(stuID,stuName,deptName,IsCanDraw,excludeWhere) values(");
                    sbSQL.Append(" '").Append(txtNumber.Text.ToString().Trim()).Append("',");
                    sbSQL.Append(" '").Append(txtName.Text.ToString().Trim()).Append("',");
                    sbSQL.Append(" '").Append(txtDept.Text.ToString().Trim()).Append("',");
                    sbSQL.Append(" '").Append(txtIsCan.Text.ToString().Trim()).Append("',");
                    sbSQL.Append(" '").Append(txtWhere.Text.ToString().Trim()).Append("')");
                    if (DA.ExecuteSQL(sbSQL.ToString()))
                    {
                        lblMessage.Text = "添加数据成功！";
                        DataAccess.DataIsChange = true;
                        Bind_DG();
                        userl();
                    }
                    else
                    {
                        lblMessage.Text = "添加数据失败，请重试！";
                    } 
                }
                else
                {
                    MessageBox.Show("员工编号重复，请重新填写！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 响应单击按钮事件——修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                //string sql = "update StuInfo set stuID='"+txtNumber.Text.Trim()+"', "+
                   string sql = "update StuInfo set "+
                                  " stuName='" + txtName.Text.Trim() + "',deptName='"+txtDept.Text.Trim()+"', "+
                                  " IsCanDraw='" + txtIsCan.Text.Trim() + "',excludeWhere='" + txtWhere.Text.Trim() + "' " +
                                  " where stuId='" +DG.CurrentRow.Cells[0].Value.ToString()+"'";
                if (DA.ExecuteSQL(sql))
                {
                    lblMessage.Text = "修改数据成功！";
                    DataAccess.DataIsChange = true;
                    Bind_DG();
                    userl();
                }
                else
                {
                    lblMessage.Text = "修改数据失败，请重试！";
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 响应单击按钮事件——删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定删除当前数据吗？", "确认删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    sbSQL = new StringBuilder("delete from StuInfo where stuID='");
                    sbSQL.Append(DG.CurrentRow.Cells[0].Value.ToString()).Append("'");
                    if (DA.ExecuteSQL(sbSQL.ToString()))
                    {
                        lblMessage.Text = "删除数据成功！";
                        DataAccess.DataIsChange = true;
                        Bind_DG();
                        userl();
                    }
                    else
                    {
                        lblMessage.Text = "删除数据失败，请重试！";
                    } 
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 响应单击按钮事件——删除所有数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定删除所有数据吗？", "确认删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    sbSQL = new StringBuilder("delete from StuInfo");
                    if (DA.ExecuteSQL(sbSQL.ToString()))
                    {
                        lblMessage.Text = "删除所有数据成功！";
                        DataAccess.DataIsChange = true;
                        Bind_DG();
                        userl();
                    }
                    else
                    {
                        lblMessage.Text = "删除所有数据失败，请重试！";
                    }
                }
            }
            catch
            { }
        }


    }
}