using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiBlogCM.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebApiBlogCM.Data
{
    public class DataAccess : IDataAccess
    {
        
   
        public string AddPost(Post oPost) 
        {
            string resp = "";
            SqlConnection sqlCon = new SqlConnection(Connection.conecString);
            SqlCommand sqlcmnd = new SqlCommand("sp_addpost", sqlCon);
            sqlcmnd.CommandType = CommandType.StoredProcedure;
            sqlcmnd.Parameters.AddWithValue("@writerid", SqlDbType.Int).Value = oPost.writerid;
            sqlcmnd.Parameters.AddWithValue("@editorid", SqlDbType.Int).Value = oPost.editorid;
            sqlcmnd.Parameters.AddWithValue("@title", SqlDbType.VarChar).Value = oPost.title;
            sqlcmnd.Parameters.AddWithValue("@description", SqlDbType.VarChar).Value = oPost.description;
            SqlDataReader reader;
            try
            {
                sqlCon.Open();
                reader = sqlcmnd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        resp = reader.GetValue(0).ToString();
                    
                    }

                }
                reader.Close();
                sqlcmnd.Dispose();
                sqlCon.Close();
                return resp;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            


        }

        public static string UpdatePost(Post oPost)
        {
            string resp = "";
            SqlConnection sqlcon = new SqlConnection(Connection.conecString);
            SqlCommand sqlcmd = new SqlCommand("sp_updatepost", sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@writerid", SqlDbType.Int).Value = oPost.writerid;
            sqlcmd.Parameters.AddWithValue("@postid", SqlDbType.Int).Value = oPost.id;
            sqlcmd.Parameters.AddWithValue("@title", SqlDbType.VarChar).Value = oPost.title;
            sqlcmd.Parameters.AddWithValue("@description", SqlDbType.VarChar).Value = oPost.description;
            SqlDataReader reader;
            try
            {

                sqlcon.Open();
                reader = sqlcmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resp = reader.GetValue(0).ToString();

                    }

                }

                reader.Close();
                sqlcmd.Dispose();
                sqlcon.Close();
                return resp;
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        public  IPostResponse ListAllPosts()
        {

            PostResponse oPostResponse = new PostResponse();
            List<Post> opostList = new List<Post>();
           
            SqlConnection sqlcon = new SqlConnection(Connection.conecString);
            SqlCommand sqlcmd = new SqlCommand("sp_listallposts", sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader;
            try
            {

                sqlcon.Open();
                reader = sqlcmd.ExecuteReader();
                if (reader.HasRows)
                {
                    oPostResponse.code = "00";
                    oPostResponse.description = "OK";
                    while (reader.Read())
                    {
                        Post oPost = new Post();
                        oPost.id = Convert.ToInt16(reader.GetValue(0));
                        oPost.writerid = Convert.ToInt16(reader.GetValue(1));
                        oPost.editorid = Convert.ToInt16(reader.GetValue(2));
                        oPost.title = reader.GetValue(3).ToString();
                        oPost.description = reader.GetValue(4).ToString();
                        oPost.status = reader.GetValue(5).ToString();
                        oPost.submitted = reader.GetValue(6).ToString();
                        oPost.creation_date = Convert.ToDateTime(reader.GetValue(7));
                        oPost.last_update_date = Convert.ToDateTime(reader.GetValue(8));
                        opostList.Add(oPost);
                          }
                     oPostResponse.postList = opostList;

                }
                else
                {
                    oPostResponse.code = "03";
                    oPostResponse.description = "No Data";
                }

                reader.Close();
                sqlcmd.Dispose();
                sqlcon.Close();
                return oPostResponse;
            }
            catch (Exception ex)
            {
                oPostResponse.code = "02";
                oPostResponse.description = ex.Message.ToString();
                return oPostResponse;
            }
        }

        public IPostResponse ListWriterPosts(string username)
        {
            PostResponse oPostResponse = new PostResponse();
            List<Post> opostList = new List<Post>();
            SqlConnection sqlcon = new SqlConnection(Connection.conecString);
            SqlCommand sqlcmd = new SqlCommand("sp_listownposts", sqlcon);
            sqlcmd.Parameters.AddWithValue("@writername", SqlDbType.VarChar).Value = username;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader;
            try
            {

                sqlcon.Open();
                reader = sqlcmd.ExecuteReader();
                if (reader.HasRows)
                {
                    oPostResponse.code = "00";
                    oPostResponse.description = "OK";
                    while (reader.Read())
                    {
                        Post oPost = new Post();
                        oPost.id = Convert.ToInt16(reader.GetValue(0));
                        oPost.writerid = Convert.ToInt16(reader.GetValue(1));
                        oPost.editorid = Convert.ToInt16(reader.GetValue(2));
                        oPost.title = reader.GetValue(3).ToString();
                        oPost.description = reader.GetValue(4).ToString();
                        oPost.status = reader.GetValue(5).ToString();
                        oPost.submitted = reader.GetValue(6).ToString();
                        oPost.creation_date = Convert.ToDateTime(reader.GetValue(7));
                        oPost.last_update_date = Convert.ToDateTime(reader.GetValue(8));
                        opostList.Add(oPost);

                    }
                    oPostResponse.postList = opostList;

                }
                else
                {
                    oPostResponse.code = "03";
                    oPostResponse.description = "No Data";
                }

                reader.Close();
                sqlcmd.Dispose();
                sqlcon.Close();
                return oPostResponse;
            }
            catch (Exception ex)
            {
                oPostResponse.code = "02";
                oPostResponse.description = ex.Message.ToString();
                return oPostResponse;
            }
        }

        public  IPostResponse ListEditorPosts(string username)
        {
            PostResponse oPostResponse = new PostResponse();
            List<Post> opostList = new List<Post>();
            SqlConnection sqlcon = new SqlConnection(Connection.conecString);
            SqlCommand sqlcmd = new SqlCommand("sp_listeditorposts", sqlcon);
            sqlcmd.Parameters.AddWithValue("@editorname", SqlDbType.VarChar).Value = username;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader;
            try
            {

                sqlcon.Open();
                reader = sqlcmd.ExecuteReader();
                if (reader.HasRows)
                {
                    oPostResponse.code = "00";
                    oPostResponse.description = "OK";
                    while (reader.Read())
                    {
                        Post oPost = new Post();
                        oPost.id = Convert.ToInt16(reader.GetValue(0));
                        oPost.writerid = Convert.ToInt16(reader.GetValue(1));
                        oPost.editorid = Convert.ToInt16(reader.GetValue(2));
                        oPost.title = reader.GetValue(3).ToString();
                        oPost.description = reader.GetValue(4).ToString();
                        oPost.status = reader.GetValue(5).ToString();
                        oPost.submitted = reader.GetValue(6).ToString();
                        oPost.creation_date = Convert.ToDateTime(reader.GetValue(7));
                        oPost.last_update_date = Convert.ToDateTime(reader.GetValue(8));
                        opostList.Add(oPost);

                    }
                    oPostResponse.postList = opostList;
                }
                else
                {
                    oPostResponse.code = "03";
                    oPostResponse.description = "No Data";
                }

                reader.Close();
                sqlcmd.Dispose();
                sqlcon.Close();
                return oPostResponse;
            }
            catch (Exception ex)
            {
                oPostResponse.code = "02";
                oPostResponse.description = ex.Message.ToString();
                return oPostResponse;
            }
        }

        public static string SubmitPost(Post oPost)
        {
            string resp = "";
            SqlConnection sqlcon = new SqlConnection(Connection.conecString);
            SqlCommand sqlcmd = new SqlCommand("sp_submitpost", sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@postid", SqlDbType.Int).Value = oPost.id;
            SqlDataReader reader;
            try
            {

                sqlcon.Open();
                reader = sqlcmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resp = reader.GetValue(0).ToString();

                    }

                }

                reader.Close();
                sqlcmd.Dispose();
                sqlcon.Close();
                return resp;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        public static string RejectPost(Post oPost)
        {
            string resp = "";
            SqlConnection sqlcon = new SqlConnection(Connection.conecString);
            SqlCommand sqlcmd = new SqlCommand("sp_rejectpost", sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@postid", SqlDbType.Int).Value = oPost.id;
            try
            {

                sqlcon.Open();
                sqlcmd.ExecuteNonQuery();
                resp = "00";
                sqlcmd.Dispose();
                sqlcon.Close();
                return resp;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        public static string PublishPost(Post oPost)
        {
            string resp = "";
            SqlConnection sqlcon = new SqlConnection(Connection.conecString);
            SqlCommand sqlcmd = new SqlCommand("sp_publishpost", sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@postid", SqlDbType.Int).Value = oPost.id;
            try
            {

                sqlcon.Open();
                sqlcmd.ExecuteNonQuery();
                resp = "00";
                sqlcmd.Dispose();
                sqlcon.Close();
                return resp;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        public static string AddComment(Comment oComment)
        {
            string resp = "";
            SqlConnection sqlCon = new SqlConnection(Connection.conecString);
            SqlCommand sqlcmnd = new SqlCommand("sp_addcomment", sqlCon);
            sqlcmnd.CommandType = CommandType.StoredProcedure;
            sqlcmnd.Parameters.AddWithValue("@userid", SqlDbType.Int).Value = oComment.userid;
            sqlcmnd.Parameters.AddWithValue("@postid", SqlDbType.Int).Value = oComment.postid;
            sqlcmnd.Parameters.AddWithValue("@description", SqlDbType.VarChar).Value = oComment.description;
            sqlcmnd.Parameters.AddWithValue("@ispublic", SqlDbType.VarChar).Value = oComment.ispublic;

            try
            {
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                resp = "00";
                sqlcmnd.Dispose();
                sqlCon.Close();
                return resp;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }



        }


    }
}