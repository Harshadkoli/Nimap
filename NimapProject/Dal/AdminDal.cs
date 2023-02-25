using NimapProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PagedList;

namespace NimapProject.Dal
{
    public class AdminDal
    {
        CommonDal Objcommon = new CommonDal();
        public CategoryModel Register(CategoryModel model)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("In_category", MyConn);
                cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = model.CategoryName;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                model.message = dt.Rows[0]["message"].ToString();
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<CategoryModel> GetAllCategory()
        {
            DataTable dt = new DataTable();
            List<CategoryModel> CatList = new List<CategoryModel>();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("GetAllCategory", MyConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                CatList = dt.AsEnumerable().ToList().ConvertAll(x => new CategoryModel
                {
                    CategoryId = x.Field<int>("CategoryId"),
                    CategoryName = x.Field<string>("CategoryName")
                });
                return CatList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ProductModel CreateProduct(ProductModel model)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("insert_Product", MyConn);
                cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = model.ProductName;
                cmd.Parameters.Add("@Product_CatId", SqlDbType.Int).Value = model.Product_CatId;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                if (Convert.ToString(dt.Rows[0]["message"]) == "Success")
                {
                    model.message = Convert.ToString(dt.Rows[0]["message"]);
                }
                else
                {
                    model.message = Convert.ToString(dt.Rows[0]["message"]);

                }

                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<CategoryModel> ShowData(List<CategoryModel> getlist)
        {
            CategoryModel model = new CategoryModel();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("GetCategory", MyConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                getlist = dt.AsEnumerable().ToList().ConvertAll(x => new CategoryModel
                {
                    CategoryId = x.Field<int>("CategoryId"),
                    CategoryName = x.Field<string>("CategoryName")
                });
                return getlist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public CategoryModel GetDatabyid(int id)
        {
            DataTable dt = new DataTable();
            CategoryModel model = new CategoryModel();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("Categorybyid", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                model.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public CategoryModel UpdateCategory(CategoryModel model)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("UpdateCategory", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = model.id;
                cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = model.CategoryName;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                if (Convert.ToString(dt.Rows[0]["message"]) == "Success")
                {
                    model.message = Convert.ToString(dt.Rows[0]["message"]);
                }
                else
                {
                    model.message = Convert.ToString(dt.Rows[0]["message"]);

                }
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string DeleteCategory(int id)
        {
            string message = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("DeleteCategory", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                message = Convert.ToString(dt.Rows[0]["message"]);
                return message;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //public List<ProductModel> ShowProData(List<ProductModel> getprolist,int page,int pagesize)
        //{
        //    CategoryModel model = new CategoryModel();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        SqlDataAdapter dap;
        //        SqlConnection MyConn = Objcommon.GetConnection();
        //        SqlCommand cmd = new SqlCommand("sp_GetAllproducts", MyConn);
        //        cmd.Parameters.Add("@page", SqlDbType.Int).Value = page;
        //        cmd.Parameters.Add("@size", SqlDbType.Int).Value = pagesize;
        //        cmd.Parameters.Add("@sort", SqlDbType.VarChar).Value = "createdon";
        //        cmd.Parameters.Add("@totalrow", SqlDbType.Int).Value = 0;
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 50000;
        //        dap = new SqlDataAdapter(cmd);
        //        dap.Fill(dt);

        //        getprolist = dt.AsEnumerable().ToList().ConvertAll(x => new ProductModel
        //        {
        //            ProductId = x.Field<int>("ProductId"),
        //            CategoryId = x.Field<int>("CategoryId"),
        //            ProductName = x.Field<string>("ProductName"),
        //            CategoryName=x.Field<string>("CategoryName")
        //        });
        //        return getprolist;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        public List<ProductModel> ShowProData(List<ProductModel> getprolist)
        {
            CategoryModel model = new CategoryModel();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("GetProduct", MyConn);
                //cmd.Parameters.Add("@page", SqlDbType.Int).Value = page;
                //cmd.Parameters.Add("@size", SqlDbType.Int).Value = pagesize;
                //cmd.Parameters.Add("@sort", SqlDbType.VarChar).Value = "createdon";
                //cmd.Parameters.Add("@totalrow", SqlDbType.Int).Value = 0;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                getprolist = dt.AsEnumerable().ToList().ConvertAll(x => new ProductModel
                {
                    ProductId = x.Field<int>("ProductId"),
                    CategoryId = x.Field<int>("CategoryId"),
                    ProductName = x.Field<string>("ProductName"),
                    CategoryName = x.Field<string>("CategoryName")
                });
                return getprolist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //public ProductModel GetAllProData(ProductModel model)
        //{ 
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        SqlDataAdapter dap;
        //        SqlConnection MyConn = Objcommon.GetConnection();
        //        SqlCommand cmd = new SqlCommand("GetProduct", MyConn);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 50000;
        //        dap = new SqlDataAdapter(cmd);
        //        dap.Fill(dt);

        //        model= dt.AsEnumerable().ToList().ConvertAll(x => new ProductModel
        //        {
        //            ProductId = x.Field<int>("ProductId"),
        //            CategoryId = x.Field<int>("CategoryId"),
        //            ProductName = x.Field<string>("ProductName"),
        //            CategoryName = x.Field<string>("CategoryName")
        //        }).ToList();

        //        return model;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        public ProductModel GetProDatabyid(int id)
        {
            DataTable dt = new DataTable();
            ProductModel model = new ProductModel();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("Productbyid", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                model.ProductId = Convert.ToInt32(dt.Rows[0]["ProductId"]);
                model.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);
                model.Product_CatId = Convert.ToInt32(dt.Rows[0]["Product_CatId"]);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ProductModel UpdateData(ProductModel model)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("UpdateData", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = model.id;
                cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = model.ProductName;
                cmd.Parameters.Add("@Product_CatId", SqlDbType.Int).Value = model.Product_CatId;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
                if (Convert.ToString(dt.Rows[0]["message"]) == "Success")
                {
                    model.message = Convert.ToString(dt.Rows[0]["message"]);
                }
                else
                {
                    model.message = Convert.ToString(dt.Rows[0]["message"]);

                }

                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string DeleteProcedure(int id)
        {
            string message = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dap;
                SqlConnection MyConn = Objcommon.GetConnection();
                SqlCommand cmd = new SqlCommand("DeleteProduct", MyConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

                message = Convert.ToString(dt.Rows[0]["message"]);
                return message;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
       
    }
}