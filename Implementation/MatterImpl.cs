using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Model;
namespace Implementation
{
    public class MatterImpl : MatterDao
    {
        public void Delete(Matter t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Matter t)
        {
            string query = @"INSERT INTO Matter (matterName,CategoryMatterId) VALUES
	        (@name,@id)";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);

                cmd.Parameters.AddWithValue("@id", t.CategoryId);
                cmd.Parameters.AddWithValue("@name", t.MatterName);

                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Select()
        {
            string query = @"SELECT m.matterid AS 'ID' ,m.matterName AS 'Name', m.CategoryMatterId AS 'Categoria'
FROM Matter m 
INNER JOIN CategoryMatter cm ON cm.CategoryMatterId = m.CategoryMatterId 
WHERE cm.ModalityId = (SELECT mo.ModalityId  FROM Modality mo 
INNER JOIN School s ON s.ModalityId=mo.ModalityId
WHERE s.SchoolId=@SchoolId)";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectByCategory(int idcat)
        {
            string query = @"SELECT co.id AS 'ID',co.name AS 'Name',co.CategoryId AS 'Categoria' from (SELECT m.matterid as 'id' ,m.matterName as 'name', m.CategoryMatterId AS 'CategoryId'
FROM Matter m 
INNER JOIN CategoryMatter cm ON cm.CategoryMatterId = m.CategoryMatterId 
WHERE cm.ModalityId = (SELECT mo.ModalityId  FROM Modality mo 
INNER JOIN School s ON s.ModalityId=mo.ModalityId
WHERE s.SchoolId=@SchoolId)) co
WHERE co.CategoryId=@CategoryId";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@CategoryId", idcat);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectLike(string like)
        {
            string query = @"SELECT m.matterid AS 'ID' ,m.matterName AS 'Name', m.CategoryMatterId AS 'Categoria'
FROM Matter m 
INNER JOIN CategoryMatter cm ON cm.CategoryMatterId = m.CategoryMatterId 
WHERE cm.ModalityId = (SELECT mo.ModalityId  FROM Modality mo 
INNER JOIN School s ON s.ModalityId=mo.ModalityId
WHERE s.SchoolId=@SchoolId) AND m.matterName LIKE  @like";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);

                cmd.Parameters.AddWithValue("@like", "%" + like + "%");
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectLikeByCategory(int idcat, string like)
        {
            string query = @"SELECT co.id AS 'ID',co.name AS 'Name',co.CategoryId AS 'Categoria' from (SELECT m.matterid as 'id' ,m.matterName as 'name', m.CategoryMatterId AS 'CategoryId'
FROM Matter m 
INNER JOIN CategoryMatter cm ON cm.CategoryMatterId = m.CategoryMatterId 
WHERE cm.ModalityId = (SELECT mo.ModalityId  FROM Modality mo 
INNER JOIN School s ON s.ModalityId=mo.ModalityId
WHERE s.SchoolId=@SchoolId)) co
WHERE co.CategoryId=@CategoryId and co.name
LIKE @like";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@CategoryId", idcat);
                cmd.Parameters.AddWithValue("@like", "%" + like + "%");
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(Matter t)
        {
            throw new NotImplementedException();
        }

        public void updateMatters(List<Matter> matters)
        {
            string queryCategory = @"UPDATE Matter SET matterName = @matterName WHERE matterid = @ID";
            try
            {

                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(matters.Count);

                for (int i = 0; i < matters.Count; i++)
                {
                    cmds[i].CommandText = queryCategory;
                    cmds[i].Parameters.AddWithValue("@ID", matters[i].MatterId);
                    cmds[i].Parameters.AddWithValue("@matterName", matters[i].MatterName);
                }
                DBImplementation.ExecuteNBasicCommand(cmds);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
