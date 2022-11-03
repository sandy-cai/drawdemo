using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;

namespace Lucky_Draw
{

    public partial class LD_Main : Form
    {
        DataAccess DA = new DataAccess();//实例化数据库操作类
        private DataTable DT_stu;//员工表                      
        private int stuCount;//员工总数
        private string strGrade;//奖项等级
        private string strGood;//奖品
        private Random RanNum;//随机数
        private int awaCount = 0; //中奖栏里的中奖个数
        private int iJXCount = 0; //存放当前选中奖项的抽奖名额
        private int onesNums = 1;//一次抽取的人数，默认为1个
        private bool isCheck = false; //标记是否选择了奖项
        private string jxId = string.Empty; //当前要抽的奖项在表中的Id

        public LD_Main()
        {
            InitializeComponent();
        }

        /// <summary>窗体加载事件
        ///     <remarks></remarks>
        /// </summary>
        private void LD_Main_Load(object sender, EventArgs e)
        {
            try
            {
                InitPage();
            }
            catch
            { }
        }

        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitPage()
        {
            #region 清空全局变量
            try
            {
                if (null != DT_stu && DT_stu.Rows.Count>0)
                {
                    DT_stu.Clear();//员工表 
                }               
                stuCount = 0;//员工总数
                strGrade = string.Empty;//奖项等级
                strGood = string.Empty;//奖品

                awaCount = 0; //中奖栏里的中奖个数
                iJXCount = 0; //存放当前选中奖项的抽奖名额
                onesNums = 1;//一次抽取的人数，默认为1个
                isCheck = false; //标记是否选择了奖项
                jxId = string.Empty; //当前要抽的奖项在表中的Id
            }
            catch 
            {
            }
            #endregion

            //查询系统表
            string strSQL = "select sys_Title,sys_Title2 from SystemInfo";
            DataTable DT_sys = DA.GetDataTable(strSQL);
            lblTitle.Text = DT_sys.Rows[0]["sys_Title"].ToString();
            lblTitle2.Text = DT_sys.Rows[0]["sys_Title2"].ToString();

            this.lblID.Text = "开　奖　区";
            this.lblID.ForeColor = System.Drawing.Color.White;
            lblGrade.Text = "恭喜发财";
            strGrade = "获奖区";
            Set_Prize();
            Set_StuCount();
            Awa_View();
            btnOpen.Enabled = false;

            try
            {
                //添加音乐
                System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(System.Environment.CurrentDirectory + @"\data\喜洋洋.wav");
                //System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(System.Environment.CurrentDirectory + @"\data\choujiang.wav");
                sndPlayer.PlayLooping();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 初始化奖项信息
        /// </summary>
        private void Set_Prize()
        {
            //先清空pannel里的所有radiobutton
            panelJX.Controls.Clear();
            //奖项表 
            string strSQL = "select  prize_id as 序号,prizeName as 奖项名称,prizeNumber as 抽奖个数,prizeGood as 奖品,prizeOrder as 奖项顺序 from PrizeInfo order by prizeOrder";
           DataTable DT_jx=DA.GetDataTable(strSQL);
            int height =5;
            int x_aris = 10;
            int y_aris = 10;
            for (int i = 0; i < DT_jx.Rows.Count; i++)
            {
                RadioButton radio = new RadioButton();
                radio.Name = DT_jx.Rows[i]["序号"].ToString();
                radio.Text = DT_jx.Rows[i]["奖项名称"].ToString() + " " + DT_jx.Rows[i]["抽奖个数"].ToString() + "名 " + DT_jx.Rows[i]["奖品"].ToString();               
                radio.Size = new System.Drawing.Size(500, 30);
                radio.Size = new System.Drawing.Size(500, 30);
                radio.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                radio.ForeColor =System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));             

                //put radio button into the panel
                panelJX.Controls.Add(radio);
                radio.Location = new Point(x_aris, y_aris + height);
                height += radio.Height;

                //Add click event to radio button
                radio.Click += new EventHandler(radio_Click);
            }
        }

        /// <summary>
        /// 单选按钮选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radio_Click(object sender, EventArgs e)
        {
            string strItem = ((RadioButton)sender).Text;
            jxId = ((RadioButton)sender).Name;
            isCheck = true;
            this.lblGrade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
            lblGrade.Height = 180;
            lblGrade.Text = strItem.Replace(" ", "\r\n");
            strGrade = strItem.Substring(0,strItem.IndexOf(" "));
            strGood = strItem.Substring(strItem.LastIndexOf(" ")+1);
           
            string mingye = strItem.Substring(strItem.IndexOf(" ") + 1, strItem.IndexOf("名") - strItem.IndexOf(" ")-1);
            //得到当前奖项的抽取名额
            iJXCount=Convert.ToInt16(string.IsNullOrEmpty(mingye)?"0":mingye);
            Awa_View();            
            Set_StuCount();//点击每个奖项时重新加载抽奖人数
            btnBegin.Focus();
        }

        /// <summary>
        /// 自定义事件——提取可以参加此项抽奖的员工人数
        /// </summary>
        private void Set_StuCount()
        {
            string strWhere = string.Empty;
            string strSQL = string.Empty;
            if (!string.IsNullOrEmpty(jxId))
            {
                strSQL = "SELECT prizeWhere FROM PrizeInfo where prize_id="+jxId;
                using (IDataReader dr=DA.ExecuteReader(strSQL))
                {
                    while (dr.Read())
                    {
                        if (!string.IsNullOrEmpty(dr["prizeWhere"].ToString()))
                        {
                            strWhere = " and (instr('" + dr["prizeWhere"].ToString() + "',excludeWhere)=0 or  excludeWhere is null) ";
                            break;  
                        } 
                    }
                    dr.Close();
                    dr.Dispose();
                }
            }
            strSQL = " select A.stuID,A.stuName,A.deptName,A.porder,B.stuID as ZJID from StuInfo A" +
                          " left join AwardsInfo B on B.stuID=A.stuID" +
                          " where IsCanDraw='T' and (B.stuID='' or B.stuID is null)";

            if (!string.IsNullOrEmpty(strWhere))
            {
                strSQL += strWhere;
            }
            DT_stu = DA.GetDataTable(strSQL);
            stuCount = DT_stu.Rows.Count;
            DataAccess.DataIsChange = false;
        }
      
        /// <summary>
        /// 自定义事件——浏览获奖情况
        /// </summary>
        private void Awa_View()
        {
            try
            {
                lsbAwaList.Items.Clear();
                lblPrize.Text = strGrade;
               // string strSQL = "select * from AwardsInfo where awaGrade='" + strGrade + "'"; 
                //获奖表 
                string strSQL = "select * from AwardsInfo where prize_id='" + jxId + "'"; 
                DataTable DT_awa=DA.GetDataTable(strSQL);
                for (int i = 0; i < DT_awa.Rows.Count; i++)
                {
                    lsbAwaList.Items.Add(" " + DT_awa.Rows[i]["stuID"].ToString() + "  " + DT_awa.Rows[i]["stuName"].ToString());
                }
                awaCount = lsbAwaList.Items.Count;
                if (lsbAwaList.Items.Count >= iJXCount)
                {
                   // MessageBox.Show(strGrade + "已经抽取完毕，请抽取其它奖项!！!");
                    btnBegin.Enabled = false;
                }
                else
                {
                    btnBegin.Enabled = true;
                }
                DT_awa = null;
            }
            catch
            { }
        }

        /// <summary>
        /// 自定义事件——检查此人是否已中奖
        /// </summary>
        /// <returns>false:说明此人已抽奖  true:说明此人未抽中奖可以继续抽奖</returns>
        private bool Awa_Chk()
        {
            try
            {
                string strSQL = "select stuID from AwardsInfo";
                DataTable DT_check = DA.GetDataTable(strSQL);
                for (int i = 0; i < DT_check.Rows.Count; i++)
                {
                    if (DT_check.Rows[i]["stuID"].ToString() == lblClass.Text.Trim())
                    {
                        return false;
                    }
                }
            }
            catch
            { }
            return true;
        }

        /// <summary>
        /// 自定义事件——添加中奖信息
        /// </summary>
        /// <param name="stuID">员工工号</param>
        /// <param name="stuName">员工姓名</param>
        /// <param name="awaGrade">几等奖</param>
        /// <param name="awaGood">奖品</param>
        private void Awa_Save(string stuID, string stuName, string awaGrade,string awaGood)
        {
            StringBuilder sbSQL = new StringBuilder("insert into AwardsInfo(stuID,stuName,awaGrade,awaGood,prize_id) values('");
            sbSQL.Append(stuID + "','" + stuName + "','" + awaGrade + "','"+awaGood+"','"+jxId+"')");
            DA.ExecuteSQL(sbSQL.ToString());
        }

       /// <summary>
        /// 响应顶部按钮事件——关闭窗体
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void tol_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 响应顶部按钮事件——最小化窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tol_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 响应顶部按钮事件——最大化/正常化窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tol_max_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// 计时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timLD_Tick(object sender, EventArgs e)
        {
           
                int randata;
                RanNum = new Random((int)DateTime.Now.Ticks);
                randata = RanNum.Next(stuCount);
                this.lblClass.Text = DT_stu.Rows[randata]["stuID"].ToString();
                lblName.Text = DT_stu.Rows[randata]["stuName"].ToString();
               // this.lblID.Text = lblClass.Text + "   " + lblName.Text;
                this.lblID.Text =  lblName.Text;
        }
      
        /// <summary>
        /// 响应单击按钮事件——摇奖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBegin_Click(object sender, EventArgs e)
        {            
             try
                {
                    if (isCheck)
                    {                       
                        //检查员工的数据有变化
                        if (DataAccess.DataIsChange)
                        {
                            Set_StuCount();
                        }
                        if (stuCount <= 0)
                        {
                            MessageBox.Show("对不起，还没有符合条件的员工信息，不能进行抽奖！", "没有记录", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        lblID.ForeColor = Color.White;

                        try
                        {
                            //添加音乐
                            //System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(System.Environment.CurrentDirectory + @"\data\喜洋洋.wav");
                            System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(System.Environment.CurrentDirectory + @"\data\choujiang.wav");
                            sndPlayer.PlayLooping();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        timLD.Start();
                        btnOpen.Enabled = true;
                        btnOpen.Focus(); 
                    }
                    else
                    {
                        MessageBox.Show("请先选择要抽取的奖项！", "没有记录", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch
                { }
            
        }

        /// <summary>
        /// 响应单击按钮事件——开奖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < onesNums; x++)
            { 
                chouJiang();
            }
            try
            {
                //System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(System.Environment.CurrentDirectory + @"\data\gongxi.wav");
                //sndPlayer.Play();
                //    System.Threading.Thread.Sleep(3800);
                 System.Media.SoundPlayer    sndPlayer = new System.Media.SoundPlayer(System.Environment.CurrentDirectory + @"\data\join.wav");
                    sndPlayer.PlayLooping();
                //添加音乐
                //System.Media.SoundPlayer sndPlayer2 = new System.Media.SoundPlayer(System.Environment.CurrentDirectory + @"\data\喜洋洋.wav");
              //  sndPlayer2.PlayLooping();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 抽奖方法
        /// </summary>
        private void chouJiang()
        {
            try
            {
                int randata;
                RanNum = new Random((int)DateTime.Now.Ticks);
                randata = RanNum.Next(stuCount);
                this.lblClass.Text = DT_stu.Rows[randata]["stuID"].ToString();
                lblName.Text = DT_stu.Rows[randata]["stuName"].ToString();
                // this.lblID.Text = lblClass.Text + "   " + lblName.Text;
                this.lblID.Text = lblName.Text;
    
                timLD.Stop();
                btnOpen.Enabled = false;
                awaCount = lsbAwaList.Items.Count;
                lblID.ForeColor = Color.Red;
                //isCheck=true;说明已经选中了要抽的奖项
                if (isCheck)
                {
                    if (awaCount >= iJXCount)
                    {
                        MessageBox.Show(strGrade+"已经抽取完毕，请抽取其它奖项!！!");
                    }
                    else
                    {
                        Awa_Save(lblClass.Text.Trim(), lblName.Text.Trim(), strGrade,strGood);
                    }
                }
                Set_StuCount();//点击每个奖项时重新加载抽奖人数
                Awa_View();
                btnBegin.Focus();
            }
            catch
            { }
        }

        /// <summary>
        /// 单选按钮选中事件——帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSysHelp_Click(object sender, EventArgs e)
        {
            Help hl = new Help();
            hl.ShowDialog();
        }

        /// <summary>
        /// 单选按钮选中事件——关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSysAbout_Click(object sender, EventArgs e)
        {
            Gy g = new Gy();
            g.ShowDialog();
        }

        /// <summary>
        /// 窗体事件——单击窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LD_Main_Click(object sender, EventArgs e)
        {
            btnBegin.Focus();
        }

        /// <summary>
        /// 窗体事件——键按下时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LD_Main_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                btnBegin.Focus();
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 重新开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("重新抽奖之前的抽奖记录将被清空，确定要重新抽奖吗？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                awaCount = 0;
                string strSQL = "delete from AwardsInfo";
                DA.ExecuteSQL(strSQL);         
                InitPage();
            }
        }

        /// <summary>
        /// 打开奖项设置页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 基本设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LD_Setting LDst = new LD_Setting();
            LDst.rl += new LD_Setting.ReturnListid(InitPage);
            LDst.ShowDialog();
        }

        /// <summary>
        /// 打开人员管理页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 数据管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LD_DataManage LDDM = new LD_DataManage();
            LDDM.userl += new LD_DataManage.ReturnListid(InitPage);
            LDDM.ShowDialog();
        }

        /// <summary>
        /// 打开关于页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gy g = new Gy();
            g.ShowDialog();
        }

        /// <summary>
        /// 打开关于页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            h.ShowDialog();
        }

        /// <summary>
        /// 打开帮助页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 帮助ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Help h = new Help();
            h.ShowDialog();
        }

        /// <summary>
        /// 导出中奖结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 导出结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql;
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Environment.CurrentDirectory + "\\data\\LD.mdb;Persist Security Info=False;Jet OLEDB:Database Password=shiyanexperiment;";
            System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(ConnectionString);
            System.Data.OleDb.OleDbCommand cmd;
            cn.Open();

            String fileName = "抽奖结果" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            sql = @"select stuID as 员工编号, stuName as 员工姓名,awaGrade as 获奖等级,awaGood as 奖品 into [Excel 8.0;database=" + System.Environment.CurrentDirectory + @"\" + fileName + "].[Sheet1] from AwardsInfo";
            cmd = new System.Data.OleDb.OleDbCommand(sql, cn);

            try
            {
                int result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    MessageBox.Show("没有中奖结果，您可能尚未开奖!");
                }
                else
                {
                    MessageBox.Show("中奖结果已经导入" + System.Environment.CurrentDirectory + "\\" + fileName + "中");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                cn.Close();
                cn.Dispose();
                cn = null;
            }          
        }

        /// <summary>
        /// 给中奖名单listBox添加右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsbAwaList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lsbAwaList.SelectedIndex = lsbAwaList.IndexFromPoint(e.X, e.Y);
                toolStripMenuItem.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        /// <summary>
        /// 中奖名单listBox右键的删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            string curUser = lsbAwaList.SelectedItem.ToString().Trim();
            string curId = curUser.Substring(0, curUser.IndexOf(" "));           
            if (MessageBox.Show("确定要删除此中奖记录？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                awaCount = awaCount-1;
                string strSQL = "delete from AwardsInfo where stuID='"+curId+"'";
                DA.ExecuteSQL(strSQL);
                Set_StuCount();//加载可以参加抽奖的人数
                Awa_View(); 
            }
        }
    }
}