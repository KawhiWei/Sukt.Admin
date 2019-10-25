using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Uwl.Attribute.ExcelAttribute;
using Uwl.Extends.Utility;

namespace Uwl.Common.Download
{
    /// <summary>
    /// Epplus导出Excel
    /// </summary>
    public class ExcelHelper<T> where T : class, new()
    {
        #region 导出Excel
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="data">泛型列表对象</param>
        /// <param name="FileName">保存的路径</param>
        /// <param name="OpenPassword">创建Excel打开密码</param>
        public static byte[] ToExcel (IList<T> data, ExcelVersion excelVersion = ExcelVersion.xlsx)
        {
            //获取泛型实体类的所有列头
            List<ExcelParameterVo> excelParameters = GetExcelParameters();
            return ToExcelbyByte(data, excelParameters);
            
        }
        /// <summary>
        /// 创建Excel;并返回文件流
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="data">导出的数据</param>
        /// <param name="excelParameters">通过反射得到的列头对象</param>
        /// <param name="FileName">文件存放路径</param>
        /// <param name="dataIndex">预留参数,暂未用到</param>
        /// <param name="excelVersion">导出的格式后缀</param>
        public static byte[] ToExcelbyByte(IList<T> data,IList<ExcelParameterVo> excelParameters,int dataIndex = 1, ExcelVersion excelVersion = ExcelVersion.xlsx)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                try
                {
                    if (data != null && data.Count > 0)
                    {
                        excelParameters = (from s in excelParameters orderby s.Sort select s).ToList();
                        object obj = null;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
                        //传入的列名 col用于得到list的下标，i用于写入Excel的某一列
                        for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                        {
                            ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                            worksheet.Cells[1, i].Value = excelParameterVo.ColumnName;//设置列名
                            worksheet.Column(i).Width = excelParameterVo.ColumnWidth;//设置列宽
                        }
                        //传入的列名 row用于得到data的下标，j用于写入Excel的某一行
                        for (int row = 0, j = 2; row < data.Count; row++, j++)
                        {
                            //传入的列名 col用于得到excelParameters的下标，i用于写入Excel的某一列
                            for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                            {
                                ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                                var item = data[row];
                                obj = excelParameterVo.Property.GetValue(item);//通过反射获取item
                                if (obj == null)//如果obj=null的话该列直接写空
                                {
                                    worksheet.Cells[j, i].Value = "";
                                }
                                else
                                {
                                    worksheet.Cells[j, i].Value = obj.ToString();
                                }
                            }
                        }
                        return package.GetAsByteArray();
                    }
                    return package.GetAsByteArray();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 创建Excel并保存到服务器
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="data">泛型列表对象</param>
        /// <param name="FileName">保存的路径</param>
        /// <param name="OpenPassword">创建Excel打开密码</param>
        public static void SaveExcel(IList<T> data, string FileName, ExcelVersion excelVersion = ExcelVersion.xlsx)
        {
            //获取泛型实体类的所有列头
            List<ExcelParameterVo> excelParameters = GetExcelParameters();
            SaveExcel(data,excelParameters, FileName);

        }
        /// <summary>
        /// 创建Excel
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="data">导出的数据</param>
        /// <param name="excelParameters">通过反射得到的列头对象</param>
        /// <param name="FileName">文件存放路径</param>
        /// <param name="dataIndex">预留参数,暂未用到</param>
        /// <param name="excelVersion">导出的格式后缀</param>
        public static void SaveExcel(IList<T> data, IList<ExcelParameterVo> excelParameters, string FileName, int dataIndex = 1, ExcelVersion excelVersion = ExcelVersion.xlsx)
        {
            FileInfo fileInfo = new FileInfo(FileName);
            if (fileInfo.Exists)
            {
                //删除原有文件
                fileInfo.Delete();
                fileInfo = new FileInfo(FileName);//创建新文件
            }
            try
            {
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    if (data != null && data.Count > 0)
                    {
                        excelParameters = (from s in excelParameters orderby s.Sort select s).ToList();
                        object obj = null;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("shett1");
                        //传入的列名 col用于得到list的下标，i用于写入Excel的某一列
                        for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                        {
                            ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                            worksheet.Cells[1, i].Value = excelParameterVo.ColumnName;//设置列名
                            worksheet.Column(i).Width = excelParameterVo.ColumnWidth;//设置列宽
                        }
                        //传入的列名 row用于得到data的下标，j用于写入Excel的某一行
                        for (int row = 0, j = 2; row < data.Count; row++, j++)
                        {
                            //传入的列名 col用于得到excelParameters的下标，i用于写入Excel的某一列
                            for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                            {
                                ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                                var item = data[row];
                                obj = excelParameterVo.Property.GetValue(item);//通过反射获取item
                                if (obj == null)//如果obj=null的话该列直接写空
                                {
                                    worksheet.Cells[j, i].Value = "";
                                }
                                else
                                {
                                    worksheet.Cells[j, i].Value = obj.ToString();
                                }
                            }
                        }
                    }
                    package.Save();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 通过反射和特性获取导出列;并生成列头对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<ExcelParameterVo> GetExcelParameters()
        {
            Type typeFromHandle = typeof(T);
            List<ExcelParameterVo> excelParameters = new List<ExcelParameterVo>();
            PropertyInfo[] properties = typeFromHandle.GetProperties();//通过反射获取到对象属性
            foreach (PropertyInfo  propertyInfo in properties)
            {
                //通过反射获取自定义特性
                ExcelColumnNameAttribute excelColumnNameAttribute = ((MemberInfo)propertyInfo).GetCustomAttribute<ExcelColumnNameAttribute>();
                if(excelColumnNameAttribute!=null)
                {
                    excelParameters.Add(new ExcelParameterVo
                    {
                        ColumnName = excelColumnNameAttribute.ColumnName,
                        ColumnWidth = excelColumnNameAttribute.ColumnWith,
                        Sort = excelColumnNameAttribute.Sort,
                        Property = propertyInfo,
                    });
                }
            }
            return excelParameters;
        }
        #endregion

        #region Excel导入
        /// <summary>
        /// 导入Excel文件
        /// </summary>
        /// <param name="excelfile"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="_hostingEnvironment"></param>
        /// <returns></returns>
        public static List<T> UpLoad(IFormFile file,int sheetIndex)
        {
            //FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            //using (FileStream fs = new FileStream(file.ToString(), FileMode.Create))
            //{
            //    excelfile.CopyTo(fs);
            //    fs.Flush();
            //}
            using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                List<T> list = worksheet.ConvertSheetToObjects<T>().ToList();
                return list;
            }
        }
        #endregion

    }
}
