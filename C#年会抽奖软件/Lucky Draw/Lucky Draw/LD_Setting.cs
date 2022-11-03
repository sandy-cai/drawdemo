
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Lucky_Draw
{
    public partial class LD_Setting : Form
    {
        DataAccess DA = new DataAccess();
        StringBuilder sbSQL = new StringBuilder();

        /// <summary>
        /// 声明委托用于刷新主抽奖页面
        /// </summary>
        public delegate void ReturnListid();
        public event ReturnListid rl;

        public LD_Setting()
        {
            InitializeComponent();
        }

        /// <summary>窗体加载事件
        ///     <remarks></remarks>
        /// </summary>
        private void LD_Setting_Load(object sender, EventArgs e)
        {
            Ini_Txt();
            Bind_DG();
        }

       /// <summary>
        /// 自定义事件——初始化文本框
       /// </summary>
        private void Ini_Txt()
        {
            string strSQL = "select sys_TopLeft,sys_Title,sys_Title2 from SystemInfo";
            DataTable DT_sys = DA.GetDataTable(strSQL);
            txtTopLeft.Text = DT_sys.Rows[0]["sys_TopLeft"].ToString();
            txtTitle.Text = DT_sys.Rows[0]["sys_Title"].ToString();
            txtTitle2.Text = DT_sys.Rows[0]["sys_Title2"].ToString();
            txtPrizeName.Text = "";
            txtPrizeNumber.Text = "";
            txtPrizeOrder.Text = "";
            txtJP.Text = "";
            txtWhere.Text = "";
        }

        /// <summary>
        /// 自定义事件——为DataGrid绑定数据
        /// </summary>
        private void Bind_DG()
        {
            try
            {
                string strSQL = "select prize_id as 序号,prizeName as 奖项名称,prizeNumber as 抽奖个数,prizeGood as 奖品,prizeOrder as 奖项顺序,prizeWhere as 排除条件 from PrizeInfo order by prizeOrder";
                DataTable DT = DA.GetDataTable(strSQL);
                DG.DataSource = DT;
                DG.Refresh();
                if (DG.Rows.Count < 1)
                {
                    btnModify.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnModify.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 响应单击按钮事件——重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIni_Click(object sender, EventArgs e)
        {
            Ini_Txt();
            Bind_DG();
        }

       /// <summary>
        /// 响应单击按钮事件——修改
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            StringBuilder sbSQL = new StringBuilder("update SystemInfo set sys_TopLeft='");
            sbSQL.Append(txtTopLeft.Text + "',sys_Title='" + txtTitle.Text + "',sys_Title2='" + txtTitle2.Text).Append("'");
            string sql = "update PrizeInfo set prizeName='" + txtPrizeName.Text.Trim() + "', "+
                              " prizeNumber=" + txtPrizeNumber.Text.Trim() + ",prizeGood='" + txtJP.Text.Trim() + "',"+
                              " prizeOrder=" + txtPrizeOrder.Text.Trim() + ",prizeWhere='"+txtWhere.Text.Trim()+"' " +
                              " where prize_id=" + int.Parse(DG.CurrentRow.Cells[0].Value.ToString());
            if (DA.ExecuteSQL(sbSQL.ToString()) && DA.ExecuteSQL(sql))
            {
                rl();
                MessageBox.Show("修改系统基本设置成功！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("修改系统基本设置失败！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            DataAccess.DataIsChange = true;
            Bind_DG();
        }

        /// <summary>
        /// datagrid的单击事件
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
                    txtPrizeName.Text = DGrow.Cells[1].Value.ToString();
                    txtPrizeNumber.Text = DGrow.Cells[2].Value.ToString();
                    txtJP.Text = DGrow.Cells[3].Value.ToString();
                    txtPrizeOrder.Text = DGrow.Cells[4].Value.ToString();
                    txtWhere.Text = DGrow.Cells[5].Value.ToString();
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定删除当前数据吗？", "确认删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    sbSQL = new StringBuilder("delete from PrizeInfo where prize_id=");
                    sbSQL.Append(int.Parse(DG.CurrentRow.Cells[0].Value.ToString()));
                    if (DA.ExecuteSQL(sbSQL.ToString()))
                    {
                        lblMessage.Text = "删除数据成功！";
                        DataAccess.DataIsChange = true;
                        Bind_DG();
                        rl();
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
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                sbSQL = new StringBuilder("insert into PrizeInfo(prizeName,prizeNumber,prizeGood,prizeOrder,prizeWhere) values(");
                sbSQL.Append(" '").Append(txtPrizeName.Text.ToString().Trim()).Append("',");
                sbSQL.Append(txtPrizeNumber.Text.ToString().Trim()).Append(",");
                sbSQL.Append(" '").Append(txtJP.Text.ToString().Trim()).Append("',");
                sbSQL.Append(txtPrizeOrder.Text.ToString().Trim()).Append(",");
                sbSQL.Append(" '").Append(txtWhere.Text.ToString().Trim()).Append("')");
                if (DA.ExecuteSQL(sbSQL.ToString()))
                {
                    lblMessage.Text = "添加数据成功！";
                    DataAccess.DataIsChange = true;
                    Bind_DG();
                    rl();
                }
                else
                {
                    lblMessage.Text = "添加数据失败，请重试！";
                }
            }
            catch
            { }
        }
     
    }
}